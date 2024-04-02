namespace Restaurant_POS_and_Ordering_Sytem.View
{
    partial class frmImageViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ZoomProfilePic = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomProfilePic)).BeginInit();
            this.SuspendLayout();
            // 
            // ZoomProfilePic
            // 
            this.ZoomProfilePic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ZoomProfilePic.Location = new System.Drawing.Point(0, 0);
            this.ZoomProfilePic.Name = "ZoomProfilePic";
            this.ZoomProfilePic.Size = new System.Drawing.Size(312, 298);
            this.ZoomProfilePic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ZoomProfilePic.TabIndex = 0;
            this.ZoomProfilePic.TabStop = false;
            this.ZoomProfilePic.Click += new System.EventHandler(this.ZoomProfilePic_Click);
            // 
            // frmImageViewer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(312, 298);
            this.Controls.Add(this.ZoomProfilePic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmImageViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmImageViewer";
            ((System.ComponentModel.ISupportInitialize)(this.ZoomProfilePic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ZoomProfilePic;
    }
}