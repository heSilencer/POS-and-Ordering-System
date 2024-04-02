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
    public partial class frmChangePassword : Form
    {
        private int userID;
        private string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";

        public frmChangePassword(int userID)
        {
            InitializeComponent();
            this.userID = userID;

        }

     

        private bool CheckCurrentPassword(string currentPassword)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT userpass FROM users WHERE userID = @UserID";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userID);
                        string storedHashedPassword = command.ExecuteScalar()?.ToString();

                        // Verify the entered password against the hashed password
                        return BCrypt.Net.BCrypt.Verify(currentPassword, storedHashedPassword);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking current password: " + ex.Message);
                return false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string currentPassword = txtCurrentpass.Text;
            string newPassword = txtNewpass.Text;
            string confirmNewPassword = txtConfirmPass.Text;

            // Check if the current password is correct
            if (!CheckCurrentPassword(currentPassword))
            {
                MessageBox.Show("Current password is incorrect.");
                return;
            }

            // Check if new password and confirm password match
            if (newPassword != confirmNewPassword)
            {
                MessageBox.Show("New password and confirm password do not match.");
                return;
            }

            // Hash the new password
            string hashedNewPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);

            // Update the password in the database
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string updateQuery = "UPDATE users SET userpass = @NewPassword WHERE userID = @UserID";
                    using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@NewPassword", hashedNewPassword);
                        command.Parameters.AddWithValue("@UserID", userID);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Password updated successfully.");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update password.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating password: " + ex.Message);
            }
        }
        private void btnhide1_Click_1(object sender, EventArgs e)
        {
            if (txtNewpass.PasswordChar == '\0')
            {
                btnhide1.Visible = false;
                btnShow1.Visible = true;
                txtNewpass.PasswordChar = '*';
                txtNewpass.Font = new Font(txtNewpass.Font.FontFamily, 25);
            }
        }

        private void btnhide2_Click_1(object sender, EventArgs e)
        {
            if (txtConfirmPass.PasswordChar == '\0')
            {
                btnhide2.Visible = false;
                btnShow2.Visible = true;
                txtConfirmPass.PasswordChar = '*';
                txtConfirmPass.Font = new Font(txtConfirmPass.Font.FontFamily, 25);
            }
        }

        private void btnShow1_Click_1(object sender, EventArgs e)
        {
            if (txtNewpass.PasswordChar == '*')
            {
                btnShow1.Visible = false;
                btnhide1.Visible = true;
                txtNewpass.PasswordChar = '\0';
                txtNewpass.Font = new Font(txtNewpass.Font.FontFamily, 14);
            }
        }

        private void btnShow2_Click_1(object sender, EventArgs e)
        {
            if (txtConfirmPass.PasswordChar == '*')
            {
                btnShow2.Visible = false;
                btnhide2.Visible = true;
                txtConfirmPass.PasswordChar = '\0';
                txtConfirmPass.Font = new Font(txtConfirmPass.Font.FontFamily, 14);
            }
        }

      

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
