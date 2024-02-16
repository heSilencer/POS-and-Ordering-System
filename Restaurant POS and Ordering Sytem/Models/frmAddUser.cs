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

namespace Restaurant_POS_and_Ordering_Sytem.Models
{
    public partial class frmAddUser : Form
    {
        string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";


        public frmAddUser()
        {
            InitializeComponent();
            LoadStaffNames();
        }
        private void LoadStaffNames()
        {
            cmbxName.Items.Clear();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Replace "tbl_staff" with the actual table name in your database
                    string query = "SELECT staffFname FROM tbl_staff";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if there are rows in the result set
                        if (reader.HasRows)
                        {
                            // Loop through each row
                            while (reader.Read())
                            {
                                // Assuming "staffFname" is a column in your table
                                string staffFname = reader["staffFname"].ToString();

                                // Add the staffFname to the ComboBox
                                cmbxName.Items.Add(staffFname);
                            }
                        }
                        else
                        {
                            // Handle the case when there are no rows in the result set
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // AddUser code
        // ...
        private void btnSave_Click(object sender, EventArgs e)
        {
            string staffFname = cmbxName.SelectedItem?.ToString();
            string username = txtuser.Text;
            string password = txtpass.Text; // Assume you're getting the password as plain text

            // Validate input fields
            if (string.IsNullOrEmpty(staffFname) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            // Hash the password
            string hashedPassword = HashPassword(password);

            // Insert into the database
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Replace "tbl_user" with the actual table name in your database
                    string insertQuery = "INSERT INTO users (uname, username, userpass, role) " +
                                        "VALUES (@uname, @username, @userpass, @role)";

                    using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@uname", staffFname); // Use staffFname as uname
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@userpass", hashedPassword);
                        command.Parameters.AddWithValue("@role", "Regular"); // Assuming a default role for registered users

                        command.ExecuteNonQuery();

                        MessageBox.Show("User added successfully!");
                        this.Close(); // Close the form after adding the user
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        // HashPassword method
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert the byte array to a string representation
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

    }
}

