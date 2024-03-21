namespace Restaurant_POS_and_Ordering_Sytem.Models
{
    partial class frmStaffAdd
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblfname = new Guna.UI2.WinForms.Guna2TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblLname = new Guna.UI2.WinForms.Guna2TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblAddress = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblPhone = new Guna.UI2.WinForms.Guna2TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbxcat = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnBrowse = new Guna.UI2.WinForms.Guna2Button();
            this.label8 = new System.Windows.Forms.Label();
            this.StaffImage = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StaffImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.guna2PictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(660, 108);
            this.panel1.TabIndex = 7;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(129, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 37);
            this.label1.TabIndex = 2;
            this.label1.Text = "Staff Details";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 409);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(660, 72);
            this.panel2.TabIndex = 8;
            // 
            // btnClose
            // 
            this.btnClose.AutoRoundedCorners = true;
            this.btnClose.BorderRadius = 28;
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(521, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(127, 58);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoRoundedCorners = true;
            this.btnSave.BorderRadius = 26;
            this.btnSave.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSave.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSave.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(379, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(127, 55);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.label2.Location = new System.Drawing.Point(19, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 31);
            this.label2.TabIndex = 10;
            this.label2.Text = "First Name";
            // 
            // lblfname
            // 
            this.lblfname.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.lblfname.DefaultText = "";
            this.lblfname.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.lblfname.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.lblfname.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.lblfname.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.lblfname.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.lblfname.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.lblfname.ForeColor = System.Drawing.Color.Black;
            this.lblfname.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.lblfname.Location = new System.Drawing.Point(21, 159);
            this.lblfname.Margin = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.lblfname.Name = "lblfname";
            this.lblfname.PasswordChar = '\0';
            this.lblfname.PlaceholderForeColor = System.Drawing.Color.Black;
            this.lblfname.PlaceholderText = "";
            this.lblfname.SelectedText = "";
            this.lblfname.Size = new System.Drawing.Size(217, 43);
            this.lblfname.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.label3.Location = new System.Drawing.Point(19, 213);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 31);
            this.label3.TabIndex = 12;
            this.label3.Text = "Last Name";
            // 
            // lblLname
            // 
            this.lblLname.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.lblLname.DefaultText = "";
            this.lblLname.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.lblLname.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.lblLname.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.lblLname.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.lblLname.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.lblLname.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.lblLname.ForeColor = System.Drawing.Color.Black;
            this.lblLname.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.lblLname.Location = new System.Drawing.Point(21, 250);
            this.lblLname.Margin = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.lblLname.Name = "lblLname";
            this.lblLname.PasswordChar = '\0';
            this.lblLname.PlaceholderForeColor = System.Drawing.Color.Black;
            this.lblLname.PlaceholderText = "";
            this.lblLname.SelectedText = "";
            this.lblLname.Size = new System.Drawing.Size(217, 43);
            this.lblLname.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.label4.Location = new System.Drawing.Point(19, 304);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 31);
            this.label4.TabIndex = 14;
            this.label4.Text = "Address";
            // 
            // lblAddress
            // 
            this.lblAddress.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.lblAddress.DefaultText = "";
            this.lblAddress.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.lblAddress.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.lblAddress.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.lblAddress.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.lblAddress.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.lblAddress.ForeColor = System.Drawing.Color.Black;
            this.lblAddress.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.lblAddress.Location = new System.Drawing.Point(21, 341);
            this.lblAddress.Margin = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.PasswordChar = '\0';
            this.lblAddress.PlaceholderForeColor = System.Drawing.Color.Black;
            this.lblAddress.PlaceholderText = "";
            this.lblAddress.SelectedText = "";
            this.lblAddress.Size = new System.Drawing.Size(217, 43);
            this.lblAddress.TabIndex = 13;
            // 
            // lblEmail
            // 
            this.lblEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.lblEmail.DefaultText = "";
            this.lblEmail.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.lblEmail.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.lblEmail.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.lblEmail.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.lblEmail.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.lblEmail.ForeColor = System.Drawing.Color.Black;
            this.lblEmail.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.lblEmail.Location = new System.Drawing.Point(262, 250);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.PasswordChar = '\0';
            this.lblEmail.PlaceholderForeColor = System.Drawing.Color.Black;
            this.lblEmail.PlaceholderText = "";
            this.lblEmail.SelectedText = "";
            this.lblEmail.Size = new System.Drawing.Size(217, 43);
            this.lblEmail.TabIndex = 16;
            // 
            // lblPhone
            // 
            this.lblPhone.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.lblPhone.DefaultText = "";
            this.lblPhone.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.lblPhone.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.lblPhone.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.lblPhone.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.lblPhone.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.lblPhone.ForeColor = System.Drawing.Color.Black;
            this.lblPhone.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.lblPhone.Location = new System.Drawing.Point(262, 159);
            this.lblPhone.Margin = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.PasswordChar = '\0';
            this.lblPhone.PlaceholderForeColor = System.Drawing.Color.Black;
            this.lblPhone.PlaceholderText = "";
            this.lblPhone.SelectedText = "";
            this.lblPhone.Size = new System.Drawing.Size(217, 43);
            this.lblPhone.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.label5.Location = new System.Drawing.Point(267, 213);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 31);
            this.label5.TabIndex = 18;
            this.label5.Text = "Email";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.label6.Location = new System.Drawing.Point(267, 123);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 31);
            this.label6.TabIndex = 17;
            this.label6.Text = "Phone";
            // 
            // cmbxcat
            // 
            this.cmbxcat.BackColor = System.Drawing.Color.Transparent;
            this.cmbxcat.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbxcat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxcat.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbxcat.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbxcat.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbxcat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbxcat.ItemHeight = 30;
            this.cmbxcat.Location = new System.Drawing.Point(262, 341);
            this.cmbxcat.Name = "cmbxcat";
            this.cmbxcat.Size = new System.Drawing.Size(217, 36);
            this.cmbxcat.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.label7.Location = new System.Drawing.Point(267, 304);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 31);
            this.label7.TabIndex = 20;
            this.label7.Text = "Category";
            // 
            // btnBrowse
            // 
            this.btnBrowse.AutoRoundedCorners = true;
            this.btnBrowse.BorderRadius = 26;
            this.btnBrowse.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBrowse.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBrowse.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBrowse.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBrowse.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnBrowse.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBrowse.ForeColor = System.Drawing.Color.White;
            this.btnBrowse.Location = new System.Drawing.Point(500, 305);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(139, 55);
            this.btnBrowse.TabIndex = 21;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.label8.Location = new System.Drawing.Point(532, 123);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 31);
            this.label8.TabIndex = 23;
            this.label8.Text = "Profile";
            // 
            // StaffImage
            // 
            this.StaffImage.BackColor = System.Drawing.Color.Silver;
            this.StaffImage.Image = global::Restaurant_POS_and_Ordering_Sytem.Properties.Resources.icons8_username_303;
            this.StaffImage.ImageRotate = 0F;
            this.StaffImage.Location = new System.Drawing.Point(494, 157);
            this.StaffImage.Name = "StaffImage";
            this.StaffImage.Size = new System.Drawing.Size(148, 140);
            this.StaffImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.StaffImage.TabIndex = 24;
            this.StaffImage.TabStop = false;
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox1.Image = global::Restaurant_POS_and_Ordering_Sytem.Properties.Resources.icons8_staff_502;
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(30, 12);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(84, 80);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 1;
            this.guna2PictureBox1.TabStop = false;
            this.guna2PictureBox1.UseTransparentBackground = true;
            // 
            // frmStaffAdd
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(660, 481);
            this.Controls.Add(this.StaffImage);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbxcat);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblLname);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblfname);
            this.Font = new System.Drawing.Font("Segoe Script", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmStaffAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmWaiterAdd";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.StaffImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private System.Windows.Forms.Panel panel2;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2TextBox lblfname;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2TextBox lblLname;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2TextBox lblAddress;
        private Guna.UI2.WinForms.Guna2TextBox lblEmail;
        private Guna.UI2.WinForms.Guna2TextBox lblPhone;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private Guna.UI2.WinForms.Guna2ComboBox cmbxcat;
        private System.Windows.Forms.Label label7;
        private Guna.UI2.WinForms.Guna2Button btnBrowse;
        private System.Windows.Forms.Label label8;
        private Guna.UI2.WinForms.Guna2PictureBox StaffImage;
    }
}