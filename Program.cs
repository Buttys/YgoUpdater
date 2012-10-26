using System;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace YgoUpdater
{
    public static class Program
    {
        public static MainFrm Frm { get; private set; }
        public static Updater Update { get; private set; }

        [STAThread]
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length != 2)
            {
                MessageBox.Show("This updater cannot be manually started.", "TDOANE - Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Update = new Updater(args[0], args[1]);

            Application.Run(Frm = new MainFrm());
        }
        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            Stream stream = myAssembly.GetManifestResourceStream("TdoaneUpdater.ICSharpCode_SharpZipLib.dll");
            byte[] raw = new byte[stream.Length];
            stream.Read(raw, 0, raw.Length);
            return Assembly.Load(raw);
        }
    }
}
