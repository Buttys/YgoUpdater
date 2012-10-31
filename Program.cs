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

            if (args.Length != 2)
            {
                MessageBox.Show("This updater cannot be manually started.", "YGOPro - Update", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Update = new Updater(args[0], args[1]);

            Application.Run(Frm = new MainFrm());
        }
    }
}
