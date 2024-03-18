namespace Restaurant_POS_and_Ordering_Sytem
{
    partial class frmStaff
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
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnWaiter = new Guna.UI2.WinForms.Guna2Button();
            this.btnCashier = new Guna.UI2.WinForms.Guna2Button();
            this.ControlsPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.btnWaiter);
            this.guna2Panel1.Controls.Add(this.btnCashier);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1294, 65);
            this.guna2Panel1.TabIndex = 0;
            // 
            // btnWaiter
            // 
            this.btnWaiter.AutoRoundedCorners = true;
            this.btnWaiter.BorderRadius = 27;
            this.btnWaiter.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnWaiter.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnWaiter.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnWaiter.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnWaiter.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnWaiter.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.btnWaiter.ForeColor = System.Drawing.Color.White;
            this.btnWaiter.Location = new System.Drawing.Point(12, 3);
            this.btnWaiter.Name = "btnWaiter";
            this.btnWaiter.Size = new System.Drawing.Size(208, 57);
            this.btnWaiter.TabIndex = 1;
            this.btnWaiter.Text = "Staff Category";
            this.btnWaiter.Click += new System.EventHandler(this.btnWaiter_Click);
            // 
            // btnCashier
            // 
            this.btnCashier.AutoRoundedCorners = true;
            this.btnCashier.BorderRadius = 27;
            this.btnCashier.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCashier.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCashier.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCashier.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCashier.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnCashier.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.btnCashier.ForeColor = System.Drawing.Color.White;
            this.btnCashier.Location = new System.Drawing.Point(226, 3);
            this.btnCashier.Name = "btnCashier";
            this.btnCashier.Size = new System.Drawing.Size(208, 56);
            this.btnCashier.TabIndex = 2;
            this.btnCashier.Text = "Staff ";
            this.btnCashier.Click += new System.EventHandler(this.btnCashier_Click);
            // 
            // ControlsPanel
            // 
            this.ControlsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ControlsPanel.Location = new System.Drawing.Point(0, 65);
            this.ControlsPanel.Name = "ControlsPanel";
            this.ControlsPanel.Size = new System.Drawing.Size(1294, 682);
            this.ControlsPanel.TabIndex = 0;
            // 
            // frmStaff
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1294, 747);
            this.Controls.Add(this.ControlsPanel);
            this.Controls.Add(this.guna2Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmStaff";
            this.Text = "frmWaiterCashier";
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Panel ControlsPanel;
        private Guna.UI2.WinForms.Guna2Button btnWaiter;
        private Guna.UI2.WinForms.Guna2Button btnCashier;
    }
}