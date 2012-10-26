using System;
using System.Windows.Forms;

namespace YgoUpdater
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrmLoad(object sender, EventArgs e)
        {
            Program.Update.Start();
        }

        public void SetText(string text)
        {
            Invoke(new Action<string>(InternalSetText), text);
        }

        private void InternalSetText(string text)
        {
            updateLabel.Text = text;
        }

        public void SetProgress(int percent)
        {
            Invoke(new Action<int>(InternalSetProgress), percent);
        }

        private void InternalSetProgress(int percent)
        {
            updateBar.Value = percent;
        }

        public void Exit()
        {
            Invoke(new Action(Close));
        }
    }
}
