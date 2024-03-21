namespace Restaurant_POS_and_Ordering_Sytem.Models
{
    partial class frmCheckOut
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCheckOut = new Guna.UI2.WinForms.Guna2Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExit = new Guna.UI2.WinForms.Guna2ControlBox();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.lvlpRecieve = new System.Windows.Forms.Label();
            this.txtpRecieve = new Guna.UI2.WinForms.Guna2TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBillAmount = new Guna.UI2.WinForms.Guna2TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtChange = new Guna.UI2.WinForms.Guna2TextBox();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.btnCheckOut);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 340);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(500, 72);
            this.panel2.TabIndex = 26;
            // 
            // btnCheckOut
            // 
            this.btnCheckOut.AutoRoundedCorners = true;
            this.btnCheckOut.BorderRadius = 26;
            this.btnCheckOut.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCheckOut.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCheckOut.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCheckOut.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCheckOut.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnCheckOut.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckOut.ForeColor = System.Drawing.Color.White;
            this.btnCheckOut.Location = new System.Drawing.Point(341, 5);
            this.btnCheckOut.Name = "btnCheckOut";
            this.btnCheckOut.Size = new System.Drawing.Size(127, 55);
            this.btnCheckOut.TabIndex = 0;
            this.btnCheckOut.Text = "Check Out";
            this.btnCheckOut.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.guna2PictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 108);
            this.panel1.TabIndex = 25;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(152)))), ((int)(((byte)(166)))));
            this.btnExit.IconColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(450, 36);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(33, 24);
            this.btnExit.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(129, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 37);
            this.label1.TabIndex = 2;
            this.label1.Text = "Check Out";
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox1.Image = global::Restaurant_POS_and_Ordering_Sytem.Properties.Resources.icons8_check_out_66;
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(30, 12);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(84, 80);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 1;
            this.guna2PictureBox1.TabStop = false;
            this.guna2PictureBox1.UseTransparentBackground = true;
            // 
            // lvlpRecieve
            // 
            this.lvlpRecieve.AutoSize = true;
            this.lvlpRecieve.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.lvlpRecieve.Location = new System.Drawing.Point(260, 123);
            this.lvlpRecieve.Name = "lvlpRecieve";
            this.lvlpRecieve.Size = new System.Drawing.Size(185, 31);
            this.lvlpRecieve.TabIndex = 28;
            this.lvlpRecieve.Text = "Payment Recieve";
            // 
            // txtpRecieve
            // 
            this.txtpRecieve.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtpRecieve.DefaultText = "";
            this.txtpRecieve.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtpRecieve.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtpRecieve.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtpRecieve.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtpRecieve.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtpRecieve.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.txtpRecieve.ForeColor = System.Drawing.Color.Black;
            this.txtpRecieve.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtpRecieve.Location = new System.Drawing.Point(266, 159);
            this.txtpRecieve.Margin = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.txtpRecieve.Name = "txtpRecieve";
            this.txtpRecieve.PasswordChar = '\0';
            this.txtpRecieve.PlaceholderForeColor = System.Drawing.Color.Black;
            this.txtpRecieve.PlaceholderText = "";
            this.txtpRecieve.SelectedText = "";
            this.txtpRecieve.Size = new System.Drawing.Size(217, 43);
            this.txtpRecieve.TabIndex = 27;
            this.txtpRecieve.TextChanged += new System.EventHandler(this.txtpRecieve_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.label3.Location = new System.Drawing.Point(24, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 31);
            this.label3.TabIndex = 30;
            this.label3.Text = "Bill Amount";
            // 
            // txtBillAmount
            // 
            this.txtBillAmount.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBillAmount.DefaultText = "";
            this.txtBillAmount.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtBillAmount.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtBillAmount.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtBillAmount.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtBillAmount.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtBillAmount.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.txtBillAmount.ForeColor = System.Drawing.Color.Black;
            this.txtBillAmount.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtBillAmount.Location = new System.Drawing.Point(30, 159);
            this.txtBillAmount.Margin = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.txtBillAmount.Name = "txtBillAmount";
            this.txtBillAmount.PasswordChar = '\0';
            this.txtBillAmount.PlaceholderForeColor = System.Drawing.Color.Black;
            this.txtBillAmount.PlaceholderText = "";
            this.txtBillAmount.SelectedText = "";
            this.txtBillAmount.Size = new System.Drawing.Size(217, 43);
            this.txtBillAmount.TabIndex = 29;
            this.txtBillAmount.TextChanged += new System.EventHandler(this.txtBillAmount_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.label4.Location = new System.Drawing.Point(24, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 31);
            this.label4.TabIndex = 32;
            this.label4.Text = "Change";
            // 
            // txtChange
            // 
            this.txtChange.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtChange.DefaultText = "";
            this.txtChange.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtChange.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtChange.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtChange.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtChange.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtChange.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.txtChange.ForeColor = System.Drawing.Color.Black;
            this.txtChange.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtChange.Location = new System.Drawing.Point(30, 249);
            this.txtChange.Margin = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.txtChange.Name = "txtChange";
            this.txtChange.PasswordChar = '\0';
            this.txtChange.PlaceholderForeColor = System.Drawing.Color.Black;
            this.txtChange.PlaceholderText = "";
            this.txtChange.SelectedText = "";
            this.txtChange.Size = new System.Drawing.Size(217, 43);
            this.txtChange.TabIndex = 31;
            this.txtChange.TextChanged += new System.EventHandler(this.txtChange_TextChanged);
            // 
            // frmCheckOut
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(500, 412);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtChange);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBillAmount);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lvlpRecieve);
            this.Controls.Add(this.txtpRecieve);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmCheckOut";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCheckOut";
            this.Load += new System.EventHandler(this.frmCheckOut_Load);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private Guna.UI2.WinForms.Guna2Button btnCheckOut;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private System.Windows.Forms.Label lvlpRecieve;
        private Guna.UI2.WinForms.Guna2TextBox txtpRecieve;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2TextBox txtBillAmount;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2TextBox txtChange;
        private Guna.UI2.WinForms.Guna2ControlBox btnExit;
    }
}