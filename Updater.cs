﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace YgoUpdater
{
    public class Updater
    {
        private IList<string> m_downloads;
        private string m_processName;
        private string m_location;

        private WebClient m_client;
        private bool m_downloaded;
        private bool m_error;

        public Updater(string updates, string processName)
        {
            m_downloads = new List<string>(updates.Split(','));
            m_processName = Path.GetFileNameWithoutExtension(processName);
            m_location = Path.GetDirectoryName(processName) + "\\";
        }

        public void Start()
        {
            Thread updateThread = new Thread(InternalUpdate) { IsBackground = true };
            updateThread.Start();
        }

        private void InternalUpdate()
        {
            try
            {
                CloseDevPro();
                DownloadUpdates();
                StartDevPro();
            }
            catch (Exception)
            {
                Program.Frm.SetText("Error when updating.");
                Thread.Sleep(2000);
            }

            Program.Frm.Exit();
        }

        private void CloseDevPro()
        {
            Process[] processes;

            string dots = "";
            for (int i = 1; i <= 3; i++)
            {
                processes = Process.GetProcessesByName(m_processName);
                if (processes.Length == 0)
                    return;

                dots += ".";
                Program.Frm.SetText("Waiting for DevPro" + dots);
                Thread.Sleep(1000);
            }

            processes = Process.GetProcessesByName(m_processName);
            if (processes.Length == 0)
                return;

            foreach (Process process in processes)
                process.Kill();
        }

        private void DownloadUpdates()
        {
            m_client = new WebClient { Proxy = null };
            m_client.DownloadProgressChanged += ClientDownloadProgressChanged;
            m_client.DownloadFileCompleted += ClientDownloadFileCompleted;
            for (int i = 0; i < m_downloads.Count; i++)
            {
                Program.Frm.SetText("Downloading update " + (i + 1) + " / " + m_downloads.Count + "...");
                DownloadUpdate(m_downloads[i]);
                InstallUpdate(0);
            }
        }

        private void DownloadUpdate(string download)
        {
            m_downloaded = false;
            m_client.DownloadFileAsync(new Uri(download), Path.Combine(Application.StartupPath, "update.zip"));
            while (!m_downloaded)
                Thread.Sleep(10);
            if (m_error)
                throw new Exception("Error when downloading.");
        }

        private void ClientDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Program.Frm.SetProgress(e.ProgressPercentage);
        }

        private void ClientDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            m_downloaded = true;
            if (e.Cancelled)
                m_error = true;
            Program.Frm.SetProgress(100);
        }

        private void InstallUpdate(int attempt)
        {
            ZipFile zipfile = null; 
            try
            {
                zipfile = new ZipFile(File.OpenRead(Path.Combine(Application.StartupPath, "update.zip")));
            }
            catch
            {
                if (attempt == 3)
                {
                    if (MessageBox.Show("Error trying to extract update, do you want to retry?", "Error Installing", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        InstallUpdate(0);
                        return;
                    }
                    else
                    {
                        return;
                    }
                }

                Thread.Sleep(500);
                InstallUpdate(attempt + 1);
                return;
            }
            
            for (int i = 0; i < zipfile.Count; i++)
            {
                ZipEntry entry = zipfile[i];
                if (!entry.IsFile) continue;

                double percent = (double) i/zipfile.Count;
                int percentInt = (int)(percent*100);
                if (percentInt > 100) percentInt = 100;
                if (percentInt < 0) percentInt = 0;
                Program.Frm.SetText("Installing " + entry.Name);
                Program.Frm.SetProgress(percentInt);

                string filename = Path.Combine(m_location, entry.Name == "DevPro.exe" ? m_processName + ".exe" : entry.Name);
                string directory = Path.GetDirectoryName(filename);
                if (directory != null && !Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                byte[] buffer = new byte[4096];
                Stream zipStream = zipfile.GetInputStream(entry);
                using (FileStream streamWriter = new FileStream(filename, FileMode.Create, FileAccess.Write))
                    StreamUtils.Copy(zipStream, streamWriter, buffer);
            }
            zipfile.Close();

            File.Delete(Path.Combine(Application.StartupPath, "update.zip"));
        }

        private void StartDevPro()
        {
            string location = Path.Combine(m_location, m_processName + ".exe");
            Process.Start(location);
        }
    }
}
