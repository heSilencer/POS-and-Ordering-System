namespace Restaurant_POS_and_Ordering_Sytem.Models
{
    partial class frmBillList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvBillList = new Guna.UI2.WinForms.Guna2DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExit = new Guna.UI2.WinForms.Guna2ControlBox();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2MessageDialog1 = new Guna.UI2.WinForms.Guna2MessageDialog();
            this.radiobuttonHold = new Guna.UI2.WinForms.Guna2RadioButton();
            this.radiobuttonDineIn = new Guna.UI2.WinForms.Guna2RadioButton();
            this.radiobuttonTakeOut = new Guna.UI2.WinForms.Guna2RadioButton();
            this.btnShowAll = new Guna.UI2.WinForms.Guna2Button();
            this.RadioButtonCheckOut = new Guna.UI2.WinForms.Guna2RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillList)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBillList
            // 
            this.dgvBillList.AllowUserToAddRows = false;
            this.dgvBillList.AllowUserToDeleteRows = false;
            this.dgvBillList.AllowUserToResizeColumns = false;
            this.dgvBillList.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.dgvBillList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvBillList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBillList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvBillList.ColumnHeadersHeight = 4;
            this.dgvBillList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBillList.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvBillList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvBillList.Location = new System.Drawing.Point(12, 160);
            this.dgvBillList.Name = "dgvBillList";
            this.dgvBillList.RowHeadersVisible = false;
            this.dgvBillList.Size = new System.Drawing.Size(825, 301);
            this.dgvBillList.TabIndex = 0;
            this.dgvBillList.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvBillList.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvBillList.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvBillList.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvBillList.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvBillList.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvBillList.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvBillList.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvBillList.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvBillList.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvBillList.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvBillList.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvBillList.ThemeStyle.HeaderStyle.Height = 4;
            this.dgvBillList.ThemeStyle.ReadOnly = false;
            this.dgvBillList.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvBillList.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvBillList.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvBillList.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvBillList.ThemeStyle.RowsStyle.Height = 22;
            this.dgvBillList.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvBillList.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
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
            this.panel1.Size = new System.Drawing.Size(849, 108);
            this.panel1.TabIndex = 24;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(152)))), ((int)(((byte)(166)))));
            this.btnExit.IconColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(793, 36);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(33, 24);
            this.btnExit.TabIndex = 3;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(129, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 37);
            this.label1.TabIndex = 2;
            this.label1.Text = "Bill List";
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox1.Image = global::Restaurant_POS_and_Ordering_Sytem.Properties.Resources.icons8_list_64__1_1;
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(30, 12);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(84, 80);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 1;
            this.guna2PictureBox1.TabStop = false;
            this.guna2PictureBox1.UseTransparentBackground = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Silver;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 467);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(849, 20);
            this.panel2.TabIndex = 25;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearch.DefaultText = "";
            this.txtSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSearch.IconRight = global::Restaurant_POS_and_Ordering_Sytem.Properties.Resources.icons8_search_48;
            this.txtSearch.Location = new System.Drawing.Point(15, 117);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(6);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PasswordChar = '\0';
            this.txtSearch.PlaceholderText = "Search Here";
            this.txtSearch.SelectedText = "";
            this.txtSearch.Size = new System.Drawing.Size(224, 34);
            this.txtSearch.TabIndex = 26;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // guna2MessageDialog1
            // 
            this.guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
            this.guna2MessageDialog1.Caption = "Warning";
            this.guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Warning;
            this.guna2MessageDialog1.Parent = this;
            this.guna2MessageDialog1.Style = Guna.UI2.WinForms.MessageDialogStyle.Light;
            this.guna2MessageDialog1.Text = "";
            // 
            // radiobuttonHold
            // 
            this.radiobuttonHold.CheckedState.BorderColor = System.Drawing.Color.Green;
            this.radiobuttonHold.CheckedState.BorderThickness = 0;
            this.radiobuttonHold.CheckedState.FillColor = System.Drawing.Color.Green;
            this.radiobuttonHold.CheckedState.InnerColor = System.Drawing.Color.White;
            this.radiobuttonHold.CheckedState.InnerOffset = -4;
            this.radiobuttonHold.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radiobuttonHold.Location = new System.Drawing.Point(259, 117);
            this.radiobuttonHold.Name = "radiobuttonHold";
            this.radiobuttonHold.Size = new System.Drawing.Size(73, 32);
            this.radiobuttonHold.TabIndex = 27;
            this.radiobuttonHold.Text = "Hold";
            this.radiobuttonHold.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.radiobuttonHold.UncheckedState.BorderThickness = 2;
            this.radiobuttonHold.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.radiobuttonHold.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.radiobuttonHold.CheckedChanged += new System.EventHandler(this.radiobuttonHold_CheckedChanged);
            // 
            // radiobuttonDineIn
            // 
            this.radiobuttonDineIn.CheckedState.BorderColor = System.Drawing.Color.Green;
            this.radiobuttonDineIn.CheckedState.BorderThickness = 0;
            this.radiobuttonDineIn.CheckedState.FillColor = System.Drawing.Color.Green;
            this.radiobuttonDineIn.CheckedState.InnerColor = System.Drawing.Color.White;
            this.radiobuttonDineIn.CheckedState.InnerOffset = -4;
            this.radiobuttonDineIn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radiobuttonDineIn.Location = new System.Drawing.Point(352, 117);
            this.radiobuttonDineIn.Name = "radiobuttonDineIn";
            this.radiobuttonDineIn.Size = new System.Drawing.Size(84, 32);
            this.radiobuttonDineIn.TabIndex = 28;
            this.radiobuttonDineIn.Text = "Dine In";
            this.radiobuttonDineIn.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.radiobuttonDineIn.UncheckedState.BorderThickness = 2;
            this.radiobuttonDineIn.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.radiobuttonDineIn.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.radiobuttonDineIn.CheckedChanged += new System.EventHandler(this.radiobuttonDineIn_CheckedChanged);
            // 
            // radiobuttonTakeOut
            // 
            this.radiobuttonTakeOut.CheckedState.BorderColor = System.Drawing.Color.Green;
            this.radiobuttonTakeOut.CheckedState.BorderThickness = 0;
            this.radiobuttonTakeOut.CheckedState.FillColor = System.Drawing.Color.Green;
            this.radiobuttonTakeOut.CheckedState.InnerColor = System.Drawing.Color.White;
            this.radiobuttonTakeOut.CheckedState.InnerOffset = -4;
            this.radiobuttonTakeOut.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radiobuttonTakeOut.Location = new System.Drawing.Point(452, 117);
            this.radiobuttonTakeOut.Name = "radiobuttonTakeOut";
            this.radiobuttonTakeOut.Size = new System.Drawing.Size(83, 32);
            this.radiobuttonTakeOut.TabIndex = 29;
            this.radiobuttonTakeOut.Text = "Take Out";
            this.radiobuttonTakeOut.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.radiobuttonTakeOut.UncheckedState.BorderThickness = 2;
            this.radiobuttonTakeOut.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.radiobuttonTakeOut.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.radiobuttonTakeOut.CheckedChanged += new System.EventHandler(this.radiobuttonTakeOut_CheckedChanged);
            // 
            // btnShowAll
            // 
            this.btnShowAll.AutoRoundedCorners = true;
            this.btnShowAll.BorderRadius = 17;
            this.btnShowAll.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnShowAll.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnShowAll.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnShowAll.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnShowAll.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnShowAll.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnShowAll.ForeColor = System.Drawing.Color.White;
            this.btnShowAll.Location = new System.Drawing.Point(717, 113);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(132, 36);
            this.btnShowAll.TabIndex = 30;
            this.btnShowAll.Text = "Show All";
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // RadioButtonCheckOut
            // 
            this.RadioButtonCheckOut.CheckedState.BorderColor = System.Drawing.Color.Green;
            this.RadioButtonCheckOut.CheckedState.BorderThickness = 0;
            this.RadioButtonCheckOut.CheckedState.FillColor = System.Drawing.Color.Green;
            this.RadioButtonCheckOut.CheckedState.InnerColor = System.Drawing.Color.White;
            this.RadioButtonCheckOut.CheckedState.InnerOffset = -4;
            this.RadioButtonCheckOut.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadioButtonCheckOut.Location = new System.Drawing.Point(552, 117);
            this.RadioButtonCheckOut.Name = "RadioButtonCheckOut";
            this.RadioButtonCheckOut.Size = new System.Drawing.Size(96, 32);
            this.RadioButtonCheckOut.TabIndex = 31;
            this.RadioButtonCheckOut.Text = "Check Out";
            this.RadioButtonCheckOut.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.RadioButtonCheckOut.UncheckedState.BorderThickness = 2;
            this.RadioButtonCheckOut.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.RadioButtonCheckOut.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.RadioButtonCheckOut.CheckedChanged += new System.EventHandler(this.RadioButtonCheckOut_CheckedChanged);
            // 
            // frmBillList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(849, 487);
            this.Controls.Add(this.RadioButtonCheckOut);
            this.Controls.Add(this.btnShowAll);
            this.Controls.Add(this.radiobuttonTakeOut);
            this.Controls.Add(this.radiobuttonDineIn);
            this.Controls.Add(this.radiobuttonHold);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvBillList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmBillList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmBillList";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2DataGridView dgvBillList;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2ControlBox btnExit;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private System.Windows.Forms.Panel panel2;
        public Guna.UI2.WinForms.Guna2TextBox txtSearch;
        private Guna.UI2.WinForms.Guna2MessageDialog guna2MessageDialog1;
        private Guna.UI2.WinForms.Guna2Button btnShowAll;
        private Guna.UI2.WinForms.Guna2RadioButton radiobuttonTakeOut;
        private Guna.UI2.WinForms.Guna2RadioButton radiobuttonDineIn;
        private Guna.UI2.WinForms.Guna2RadioButton radiobuttonHold;
        private Guna.UI2.WinForms.Guna2RadioButton RadioButtonCheckOut;
    }
}