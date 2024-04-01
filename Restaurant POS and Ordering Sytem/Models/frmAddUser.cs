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
using BCrypt.Net;
namespace Restaurant_POS_and_Ordering_Sytem.Models
{
    public partial class frmAddUser : Form
    {
        string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";
        public event EventHandler UserAddedOrUpdated;


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

                    // Modify the query to select only staff names with category "Cashier"
                    string query = "SELECT staffID, staffFname FROM tbl_staff WHERE staffcatID IN (SELECT staffcatID FROM tbl_staffcategory WHERE catName IN ('Cashier', 'Admin', 'Manager'))";


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
                                string staffID = reader["staffID"].ToString();

                                // Store staffID as the Tag of each item
                                cmbxName.Items.Add(staffFname);
                                cmbxName.Tag = staffID;
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
                    guna2MessageDialog1.Show("Error: " + ex.Message);
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
            if (string.IsNullOrEmpty(staffFname))
            {
                guna2MessageDialog1.Show("Please select a cashier.");
                return;
            }

            // Retrieve staffID from the ComboBox's Tag
            string staffID = cmbxName.Tag.ToString();

            string username = txtuser.Text;
            string password = txtpass.Text;
            string staffrole = cmbxrole.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                guna2MessageDialog1.Show("Please fill in all required fields.");
                return;
            }

            string hashedPassword = HashString(password);

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO users (uname, username, userpass, role, staffID) " +
                                        "VALUES (@uname, @username, @userpass, @role, @staffID)";

                    using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@uname", staffFname);
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@userpass", hashedPassword);
                        command.Parameters.AddWithValue("@role", staffrole);
                        command.Parameters.AddWithValue("@staffID", staffID);

                        command.ExecuteNonQuery();

                        guna2MessageDialog1.Show("User added successfully!");

                        // Raise the UserAddedOrUpdated event
                        UserAddedOrUpdated?.Invoke(this, e);


                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    guna2MessageDialog2.Show("Error: " + ex.Message);
                }
            }
        }

        private string HashString(string passwordString)
        {
            // Hash the password using BCrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(passwordString, BCrypt.Net.BCrypt.GenerateSalt());
            return hashedPassword;
        }
     

        private void frmAddUser_Load(object sender, EventArgs e)
        {

            cmbxrole.Items.Add("Admin");
            cmbxrole.Items.Add("Cashier");
        }

        private void cmbxName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
    }
}

