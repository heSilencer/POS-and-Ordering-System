using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Restaurant_POS_and_Ordering_Sytem.ForgotPassword
{
    public partial class frmCheckUser : Form
    {
        string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";
        private frmNewPassword newPasswordForm;
        public frmCheckUser()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            frmlogin login = new frmlogin();
            login.Show();
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string enteredUsername = txtUsername.Text;

            if (string.IsNullOrEmpty(enteredUsername))
            {
                MessageBox.Show("Please enter a username.");
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string checkUserQuery = "SELECT * FROM users WHERE username = @username";

                    using (MySqlCommand cmd = new MySqlCommand(checkUserQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", enteredUsername);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Username exists, open the form to reset password
                                frmNewPassword newPasswordForm = new frmNewPassword(enteredUsername, this);
                                newPasswordForm.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Username not found.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        public void PasswordChangedSuccessfully()
        {
            MessageBox.Show("Password changed successfully.");
            newPasswordForm = null; // Reset the reference
            frmlogin loginForm = new frmlogin();
            loginForm.Show();

            // Close the frmCheckUser form
            this.Hide();
        }
    }
}
