using Restaurant_POS_and_Ordering_Sytem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_POS_and_Ordering_Sytem
{
    public partial class Subform : Form
    {
        public Subform()
        {
            InitializeComponent();
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            frmPos POS = new frmPos();

            // Assuming you have a property to store the user role in SubForm
            POS.UserRole = "cashier";

            POS.Show();
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

                // Alternatively, show a login form and hide the current form
                // LoginForm loginForm = new LoginForm();
                // loginForm.Show();
                // this.Hide();
            }
            else
            {
                // User canceled logout, you can handle this if needed
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimized_Click(object sender, EventArgs e)
        {
           
        }
    }
}
