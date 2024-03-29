﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Restaurant_POS_and_Ordering_Sytem.View;



namespace Restaurant_POS_and_Ordering_Sytem
{
    public partial class MainForm : Form
    {
        private static MainForm _obj;
        private string username;

        private int userID;
        private int mainID;

        public static MainForm Instance
        {
            get
            {
                if (_obj == null)
                {
                    _obj = new MainForm("DefaultUsername");
                }
                return _obj;
            }
        }

        public MainForm(string username)
        {
            InitializeComponent();
            this.username = username;
            this.userID = userID;
        }

        public MainForm(string username, int userID) : this(username)
        {
            this.userID = userID;
        }

        public void AddControls(Form f)
        {
            ControlsPanel.Controls.Clear();
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            ControlsPanel.Controls.Add(f);
            f.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
           AddControls(new frmCategoryView());
        }

        private void btndashboard_Click(object sender, EventArgs e)
        {
            AddControls(new frmdashboardView());
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            AddControls(new frmProductvView());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Display a confirmation message using guna2MessageDialog
            guna2MessageDialog1.Caption = "Logout Confirmation";
            guna2MessageDialog1.Text = "Are you sure you want to logout?";
           

            DialogResult dialogResult = guna2MessageDialog1.Show();

            // Check user's choice
            if (dialogResult == DialogResult.Yes)
            {
                frmlogin l = new frmlogin();
                l.Show();               
                    this.Close();

               
            }
            else
            {
            }
        }

        private void btnTables_Click(object sender, EventArgs e)
        {
            AddControls(new frmTableView());

        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            Models.frmPos frmpos = new Models.frmPos(username,userID, mainID);

            // Assuming you have a property to store the user role in MainForm
            frmpos.userRole = "admin";

            frmpos.Show();
            this.Hide();
        }

        private void ControlsPanel_Paint(object sender, PaintEventArgs e)
        {
            // Create an instance of frmDashboard
            frmdashboardView dashboardForm = new frmdashboardView();

            // Set TopLevel property to false
            dashboardForm.TopLevel = false;

            // Clear existing controls in the ControlsPanel
            ControlsPanel.Controls.Clear();

            // Add frmDashboard to ControlsPanel
            ControlsPanel.Controls.Add(dashboardForm);

            // Set the Dock property to fill the ControlsPanel
            dashboardForm.Dock = DockStyle.Fill;

            // Show the frmDashboard
            dashboardForm.Show();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            AddControls(new frmStaff());

        }

      
        

        private void btnReports_Click(object sender, EventArgs e)
        {

        }

        private void btnMinimized_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnkitchen_Click(object sender, EventArgs e)
        {
            AddControls(new frmKitchenView());
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AddControls(new frmAddUserView());

        }

        private void lbluser_Click(object sender, EventArgs e)
        {

        }
      

        private void MainForm_Load_1(object sender, EventArgs e)
        {
            lbluser.Text = $" {username}!";
            _obj = this;
        }
    }
}
