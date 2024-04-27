namespace Restaurant_POS_and_Ordering_Sytem.View
{
    partial class frmBillListView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            this.RadioButtonCheckOut = new Guna.UI2.WinForms.Guna2RadioButton();
            this.btnShowAll = new Guna.UI2.WinForms.Guna2Button();
            this.radiobuttonTakeOut = new Guna.UI2.WinForms.Guna2RadioButton();
            this.radiobuttonDineIn = new Guna.UI2.WinForms.Guna2RadioButton();
            this.txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.dgvBillList = new Guna.UI2.WinForms.Guna2DataGridView();
            this.guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            this.label1 = new System.Windows.Forms.Label();
            this.radiobuttonHold = new Guna.UI2.WinForms.Guna2RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillList)).BeginInit();
            this.SuspendLayout();
            // 
            // RadioButtonCheckOut
            // 
            this.RadioButtonCheckOut.CheckedState.BorderColor = System.Drawing.Color.Green;
            this.RadioButtonCheckOut.CheckedState.BorderThickness = 0;
            this.RadioButtonCheckOut.CheckedState.FillColor = System.Drawing.Color.Green;
            this.RadioButtonCheckOut.CheckedState.InnerColor = System.Drawing.Color.White;
            this.RadioButtonCheckOut.CheckedState.InnerOffset = -4;
            this.RadioButtonCheckOut.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RadioButtonCheckOut.Location = new System.Drawing.Point(720, 64);
            this.RadioButtonCheckOut.Name = "RadioButtonCheckOut";
            this.RadioButtonCheckOut.Size = new System.Drawing.Size(173, 43);
            this.RadioButtonCheckOut.TabIndex = 38;
            this.RadioButtonCheckOut.Text = "Check Out";
            this.RadioButtonCheckOut.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.RadioButtonCheckOut.UncheckedState.BorderThickness = 2;
            this.RadioButtonCheckOut.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.RadioButtonCheckOut.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.RadioButtonCheckOut.Visible = false;
            this.RadioButtonCheckOut.CheckedChanged += new System.EventHandler(this.RadioButtonCheckOut_CheckedChanged);
            // 
            // btnShowAll
            // 
            this.btnShowAll.AutoRoundedCorners = true;
            this.btnShowAll.BorderRadius = 20;
            this.btnShowAll.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnShowAll.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnShowAll.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnShowAll.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnShowAll.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnShowAll.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnShowAll.ForeColor = System.Drawing.Color.White;
            this.btnShowAll.Location = new System.Drawing.Point(1057, 67);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(171, 43);
            this.btnShowAll.TabIndex = 37;
            this.btnShowAll.Text = "Show All";
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // radiobuttonTakeOut
            // 
            this.radiobuttonTakeOut.CheckedState.BorderColor = System.Drawing.Color.Green;
            this.radiobuttonTakeOut.CheckedState.BorderThickness = 0;
            this.radiobuttonTakeOut.CheckedState.FillColor = System.Drawing.Color.Green;
            this.radiobuttonTakeOut.CheckedState.InnerColor = System.Drawing.Color.White;
            this.radiobuttonTakeOut.CheckedState.InnerOffset = -4;
            this.radiobuttonTakeOut.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radiobuttonTakeOut.Location = new System.Drawing.Point(577, 64);
            this.radiobuttonTakeOut.Name = "radiobuttonTakeOut";
            this.radiobuttonTakeOut.Size = new System.Drawing.Size(137, 43);
            this.radiobuttonTakeOut.TabIndex = 36;
            this.radiobuttonTakeOut.Text = "Take Out";
            this.radiobuttonTakeOut.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.radiobuttonTakeOut.UncheckedState.BorderThickness = 2;
            this.radiobuttonTakeOut.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.radiobuttonTakeOut.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.radiobuttonTakeOut.CheckedChanged += new System.EventHandler(this.radiobuttonTakeOut_CheckedChanged);
            // 
            // radiobuttonDineIn
            // 
            this.radiobuttonDineIn.CheckedState.BorderColor = System.Drawing.Color.Green;
            this.radiobuttonDineIn.CheckedState.BorderThickness = 0;
            this.radiobuttonDineIn.CheckedState.FillColor = System.Drawing.Color.Green;
            this.radiobuttonDineIn.CheckedState.InnerColor = System.Drawing.Color.White;
            this.radiobuttonDineIn.CheckedState.InnerOffset = -4;
            this.radiobuttonDineIn.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radiobuttonDineIn.Location = new System.Drawing.Point(449, 64);
            this.radiobuttonDineIn.Name = "radiobuttonDineIn";
            this.radiobuttonDineIn.Size = new System.Drawing.Size(122, 43);
            this.radiobuttonDineIn.TabIndex = 35;
            this.radiobuttonDineIn.Text = "Dine In";
            this.radiobuttonDineIn.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.radiobuttonDineIn.UncheckedState.BorderThickness = 2;
            this.radiobuttonDineIn.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.radiobuttonDineIn.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.radiobuttonDineIn.CheckedChanged += new System.EventHandler(this.radiobuttonDineIn_CheckedChanged);
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
            this.txtSearch.Location = new System.Drawing.Point(15, 64);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PasswordChar = '\0';
            this.txtSearch.PlaceholderText = "Search Here";
            this.txtSearch.SelectedText = "";
            this.txtSearch.Size = new System.Drawing.Size(425, 43);
            this.txtSearch.TabIndex = 33;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // dgvBillList
            // 
            this.dgvBillList.AllowUserToAddRows = false;
            this.dgvBillList.AllowUserToDeleteRows = false;
            this.dgvBillList.AllowUserToResizeColumns = false;
            this.dgvBillList.AllowUserToResizeRows = false;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.White;
            this.dgvBillList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle19;
            this.dgvBillList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBillList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.dgvBillList.ColumnHeadersHeight = 4;
            this.dgvBillList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBillList.DefaultCellStyle = dataGridViewCellStyle21;
            this.dgvBillList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvBillList.Location = new System.Drawing.Point(12, 132);
            this.dgvBillList.Name = "dgvBillList";
            this.dgvBillList.ReadOnly = true;
            this.dgvBillList.RowHeadersVisible = false;
            this.dgvBillList.Size = new System.Drawing.Size(1339, 556);
            this.dgvBillList.TabIndex = 32;
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
            this.dgvBillList.ThemeStyle.ReadOnly = true;
            this.dgvBillList.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvBillList.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvBillList.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvBillList.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvBillList.ThemeStyle.RowsStyle.Height = 22;
            this.dgvBillList.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvBillList.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // guna2Separator1
            // 
            this.guna2Separator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2Separator1.FillThickness = 2;
            this.guna2Separator1.Location = new System.Drawing.Point(12, 116);
            this.guna2Separator1.Name = "guna2Separator1";
            this.guna2Separator1.Size = new System.Drawing.Size(1338, 10);
            this.guna2Separator1.TabIndex = 39;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 72);
            this.label1.TabIndex = 40;
            this.label1.Text = "Bill List";
            // 
            // radiobuttonHold
            // 
            this.radiobuttonHold.CheckedState.BorderColor = System.Drawing.Color.Green;
            this.radiobuttonHold.CheckedState.BorderThickness = 0;
            this.radiobuttonHold.CheckedState.FillColor = System.Drawing.Color.Green;
            this.radiobuttonHold.CheckedState.InnerColor = System.Drawing.Color.White;
            this.radiobuttonHold.CheckedState.InnerOffset = -4;
            this.radiobuttonHold.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radiobuttonHold.Location = new System.Drawing.Point(325, 64);
            this.radiobuttonHold.Name = "radiobuttonHold";
            this.radiobuttonHold.Size = new System.Drawing.Size(96, 43);
            this.radiobuttonHold.TabIndex = 34;
            this.radiobuttonHold.Text = "Hold";
            this.radiobuttonHold.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.radiobuttonHold.UncheckedState.BorderThickness = 2;
            this.radiobuttonHold.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.radiobuttonHold.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.radiobuttonHold.Visible = false;
            this.radiobuttonHold.CheckedChanged += new System.EventHandler(this.radiobuttonHold_CheckedChanged);
            // 
            // frmBillListView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1360, 700);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.guna2Separator1);
            this.Controls.Add(this.RadioButtonCheckOut);
            this.Controls.Add(this.btnShowAll);
            this.Controls.Add(this.radiobuttonTakeOut);
            this.Controls.Add(this.radiobuttonDineIn);
            this.Controls.Add(this.radiobuttonHold);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dgvBillList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmBillListView";
            this.Text = "frmBillListView";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2RadioButton RadioButtonCheckOut;
        private Guna.UI2.WinForms.Guna2Button btnShowAll;
        private Guna.UI2.WinForms.Guna2RadioButton radiobuttonTakeOut;
        private Guna.UI2.WinForms.Guna2RadioButton radiobuttonDineIn;
        public Guna.UI2.WinForms.Guna2TextBox txtSearch;
        private Guna.UI2.WinForms.Guna2DataGridView dgvBillList;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2RadioButton radiobuttonHold;
    }
}