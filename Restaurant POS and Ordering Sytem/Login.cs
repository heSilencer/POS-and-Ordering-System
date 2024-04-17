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
using System.Security.Cryptography;
using Restaurant_POS_and_Ordering_Sytem.Models;
using Restaurant_POS_and_Ordering_Sytem.ForgotPassword;

namespace Restaurant_POS_and_Ordering_Sytem
{
    public partial class frmlogin : Form
    {
        string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";


        private string username;
        private int mainID;
        public frmlogin()
        {
            InitializeComponent();
            txtUsername.KeyPress += new KeyPressEventHandler(txtUsername_KeyPress);
            txtPassword.KeyPress += new KeyPressEventHandler(txtPassword_KeyPress);

        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            login();
        }
     
        private void OpenFormBasedOnRole(string enteredUsername, string userRole, string username, int userID, int mainID)
        {
            if (userRole == "Admin")
            {
                MainForm mainForm = new MainForm(username, userID); // Pass the UserID to MainForm
                mainForm.Show();
                this.Hide();
            }
            else if (userRole == "Cashier")
            {
                Subform subForm = new Subform(username, userID,mainID); // Pass the UserID to Subform
                subForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Unknown role: " + userRole);
            }
        }


        private string HashString(string passwordString)
        {
            // Hash the password using BCrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(passwordString, BCrypt.Net.BCrypt.GenerateSalt());
            return hashedPassword;
        }

        private bool VerifyPassword(string enteredPassword, string hashedPassword)
        {
            // Verify the entered password against the hashed password using BCrypt
            return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
        }


       
        private void login()
        {
            string enteredUsername = txtUsername.Text;
            string enteredPassword = txtPassword.Text;

            if (string.IsNullOrEmpty(enteredUsername) || string.IsNullOrEmpty(enteredPassword))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            // Clear any previous error messages

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string loginQuery = "SELECT userID, uname, userpass, role FROM users WHERE BINARY username = @username";

                    using (MySqlCommand cmd = new MySqlCommand(loginQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", enteredUsername);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int userID = Convert.ToInt32(reader["userID"]); // Retrieve the UserID
                                string storedPasswordHash = reader["userpass"].ToString();
                                if (VerifyPassword(enteredPassword, storedPasswordHash))
                                {
                                    string username = reader["uname"].ToString();
                                    string userRole = reader["role"].ToString();
                                    OpenFormBasedOnRole(enteredUsername, userRole, username, userID, mainID); // Pass the UserID
                                }
                                else
                                {
                                    guna2MessageDialog1.Show("Invalid Password.");
                                    // Clear password field
                                    txtPassword.Clear();
                                    txtUsername.Clear();


                                }
                            }
                            else
                            {
                                guna2MessageDialog1.Show("User not found.");
                                // Clear both fields
                                txtUsername.Clear();
                                txtPassword.Clear();
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




        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void linkforgotPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmCheckUser CheckUser = new frmCheckUser();
            CheckUser.Show();
            this.Hide();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '*')
            {
                btnShow.Visible = false;
                pictureBox3.Visible = true;
                txtPassword.PasswordChar = '\0';
                txtPassword.Font = new Font(txtPassword.Font.FontFamily, 14);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '\0')
            {
                pictureBox3.Visible = false;
                btnShow.Visible = true;
                txtPassword.PasswordChar = '*';
                txtPassword.Font = new Font(txtPassword.Font.FontFamily, 25);
            }
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                // Move focus to the password textbox
                txtPassword.Focus();
                // Consume the Enter key press event to prevent it from being entered into the password textbox
                e.Handled = true;
            }
            // Preventing user from entering non-alphanumeric characters for username
            else if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
       

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                login();
            }
        }
    }
}
