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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length < 2)
            {
                MessageBox.Show("This updater cannot be manually started.", "YGOPro - Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string dir = string.Empty;

            for (int i = 1; i < args.Length; i++)
            {
                if (string.IsNullOrEmpty(dir))
                    dir += args[i];
                else
                    dir += " " + args[i];
            }

            Update = new Updater(args[0], dir);

            Application.Run(Frm = new MainFrm());
        }
    }
}
