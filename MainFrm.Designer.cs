namespace YgoUpdater
{
    partial class MainFrm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form

        private void InitializeComponent()
        {
            this.updateLabel = new System.Windows.Forms.Label();
            this.updateBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // updateLabel
            // 
            this.updateLabel.AutoSize = true;
            this.updateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateLabel.Location = new System.Drawing.Point(12, 9);
            this.updateLabel.Name = "updateLabel";
            this.updateLabel.Size = new System.Drawing.Size(86, 16);
            this.updateLabel.TabIndex = 0;
            this.updateLabel.Text = "Please wait...";
            // 
            // updateBar
            // 
            this.updateBar.Location = new System.Drawing.Point(12, 37);
            this.updateBar.Name = "updateBar";
            this.updateBar.Size = new System.Drawing.Size(265, 22);
            this.updateBar.TabIndex = 1;
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 71);
            this.Controls.Add(this.updateBar);
            this.Controls.Add(this.updateLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "MainFrm";
            this.Text = "YGOPro - Updater";
            this.Load += new System.EventHandler(this.MainFrmLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label updateLabel;
        private System.Windows.Forms.ProgressBar updateBar;
    }
}

