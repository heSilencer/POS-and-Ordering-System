﻿using MySql.Data.MySqlClient;
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

namespace Restaurant_POS_and_Ordering_Sytem.View
{
    public partial class frmAddUserView : SampleView
    {
        string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";
        public frmAddUserView()
        {
            InitializeComponent();
            Add_UserView.Columns.Add("srNumber", "Sr#");
            Add_UserView.Columns.Add("userId", "User ID");
            Add_UserView.Columns.Add("uname", "Full Name");
            Add_UserView.Columns.Add("username", "UserName");
            Add_UserView.Columns.Add("userpass", "Password");
            Add_UserView.Columns.Add("role", "Role");
            Add_UserView.Columns["Role"].DefaultCellStyle.Font = new Font("Segoe UI", 14);


            // Set visibility of columns
            Add_UserView.Columns["userId"].Visible = false; // Hide User ID column

            // Set width of columns
            Add_UserView.Columns["srNumber"].Width = 50;
            Add_UserView.Columns["uname"].Width = 100;
            Add_UserView.Columns["username"].Width = 150;
            Add_UserView.Columns["userpass"].Width = 100;
            Add_UserView.Columns["role"].Width = 200;

            Add_UserView.DefaultCellStyle.Font = new Font("Segue", 14);
            Add_UserView.RowTemplate.Height = 40;

            DataGridViewImageColumn deleteColumn = new DataGridViewImageColumn();
            deleteColumn.Image = Properties.Resources.deleteicon; // Replace with your actual delete icon
            deleteColumn.Name = "Delete";
            deleteColumn.HeaderText = ""; // Set the header text to an empty string
            deleteColumn.HeaderCell.Style.NullValue = "";
            deleteColumn.Width = 50;
            deleteColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            deleteColumn.FillWeight = 50;
            deleteColumn.MinimumWidth = 50;
            deleteColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            Add_UserView.Columns.Add(deleteColumn);

            // Load data from the database into the DataGridView
            Add_UserView.CellClick += Add_UserView_CellClick;

        }
        private void FrmAddUser_UserAddedOrUpdated(object sender, EventArgs e)
        {
            // Refresh data from the database
            Add_UserView.Rows.Clear(); // Clear the existing rows before loading new data

            LoadDataFromDatabase();
        }
        public override void btnAdd_Click(object sender, EventArgs e)
        {
            var addUserForm = new frmAddUser();
            addUserForm.UserAddedOrUpdated += FrmAddUser_UserAddedOrUpdated; // Subscribe to the event
            MainClass.BlurbackGround(addUserForm);


        }
        public override void txtSearch_TextChanged(object sender, EventArgs e)
        {

            string searchText = txtSearch.Text.Trim().ToLower();

            // Filter the rows based on the search text
            foreach (DataGridViewRow row in Add_UserView.Rows)
            {
                bool matchFound = false;

                // Check if the search text matches the value in any of the specified columns
                foreach (string columnName in new[] { "uname", "username", "role" })
                {
                    if (row.Cells[columnName].Value.ToString().ToLower().Contains(searchText))
                    {
                        matchFound = true;
                        break;
                    }
                }

                row.Visible = matchFound;
            }
        }


        private void btnAdd_Click_1(object sender, EventArgs e)
        {

        }
        private void LoadDataFromDatabase()
        {
            Add_UserView.Rows.Clear(); // Clear the existing rows before loading new data

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM users"; // Replace 'your_table_name' with the actual table name

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        int srNumber = 1;

                        while (reader.Read())
                        {
                            string userId = reader["userId"].ToString();
                            string uname = reader["uname"].ToString();
                            string username = reader["username"].ToString();
                            string userpass = reader["userpass"].ToString();
                            string role = reader["role"].ToString();

                            Add_UserView.Rows.Add(srNumber, userId, uname, username, userpass, role);
                            srNumber++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    guna2MessageDialog2.Show("Error: " + ex.Message);
                }
            }
        }
        private void Add_UserView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && Add_UserView.Columns[e.ColumnIndex].Name == "Delete")
    {
                // Get the role of the user to be deleted
                string role = Add_UserView.Rows[e.RowIndex].Cells["role"].Value.ToString();

                if (role.ToLower() == "admin")
                {
                    // Display a message that admin users cannot be deleted
                    MessageBox.Show("Admin users cannot be deleted.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the method without further processing
                }

                // Confirm deletion with the user
                DialogResult result = MessageBox.Show("Are you sure you want to delete this user?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    DialogResult result1 = MessageBox.Show("This User will be Permanently Deleted. Are you sure you want to delete this?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result1 == DialogResult.Yes)
                    {
                        int userId = Convert.ToInt32(Add_UserView.Rows[e.RowIndex].Cells["userId"].Value);

                        // Perform the deletion operation
                        DeleteUser(userId);
                    }
                    else
                    {
                        // User clicked "No" in the second confirmation dialog
                        // Do nothing, deletion operation canceled
                    }
                }
            }
        }

        private void DeleteUser(int userId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Delete user with the specified ID
                    string query = "DELETE FROM users WHERE userId = @userId"; //

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Remove the row from the DataGridView
                            foreach (DataGridViewRow row in Add_UserView.Rows)
                            {
                                if (Convert.ToInt32(row.Cells["userId"].Value) == userId)
                                {
                                    Add_UserView.Rows.Remove(row);
                                    break;
                                }
                            }
                            LoadDataFromDatabase();

                            guna2MessageDialog1.Show("User deleted successfully");
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete user", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    guna2MessageDialog2.Show("Error: " + ex.Message, "Error");
                }
            }
        }

        private void frmAddUserView_Load(object sender, EventArgs e)
        {
            LoadDataFromDatabase();
        }
    }
}