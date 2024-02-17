using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_POS_and_Ordering_Sytem.ForgotPassword
{
    public partial class frmNewPassword : Form
    {
        string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";

        private string username;
        private frmCheckUser checkUserForm;
        public frmNewPassword(string username, frmCheckUser checkUserForm)
        {
            InitializeComponent();
            this.username = username;
        }

        private void frmNewPassword_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewpass.Text;
            string confirmPassword = txtConfirmPass.Text;

            // Validate that new password and confirm password match
            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirm password do not match.");
                return;
            }

            // Save the new password to the database
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string updatePasswordQuery = "UPDATE users SET userpass = @newPassword WHERE username = @username";

                    using (MySqlCommand cmd = new MySqlCommand(updatePasswordQuery, connection))
                    {
                        // Use appropriate hashing method to hash the new password before saving
                        string hashedPassword = HashString(newPassword);

                        cmd.Parameters.AddWithValue("@newPassword", hashedPassword);
                        cmd.Parameters.AddWithValue("@username", username);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Password changed successfully.");

                            // Inform the frmCheckUser that the password is changed successfully
                            if (checkUserForm != null)
                            {
                                checkUserForm.PasswordChangedSuccessfully();
                            }

                            // Open the login form
                            frmlogin loginForm = new frmlogin();
                            loginForm.Show();

                            // Close the current form (frmNewPassword)
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to change password.");
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
             private string HashString(string passwordString)
        {
            // Implement your hashing logic (e.g., BCrypt, SHA256, etc.)
            // Example using BCrypt:
            // return BCrypt.Net.BCrypt.HashPassword(passwordString, BCrypt.Net.BCrypt.GenerateSalt());
            return BCrypt.Net.BCrypt.HashPassword(passwordString, BCrypt.Net.BCrypt.GenerateSalt());
        }
    }
 }

