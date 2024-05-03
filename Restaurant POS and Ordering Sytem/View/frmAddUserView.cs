using MySql.Data.MySqlClient;
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
            InitializeDataGridView();
            LoadDataFromDatabase();

        }
        private void InitializeDataGridView()
        {
            guna2datagrid.Columns.Add("srNumber", "Sr#");
            guna2datagrid.Columns.Add("userId", "User ID");
            guna2datagrid.Columns.Add("uname", "Full Name");
            guna2datagrid.Columns.Add("username", "UserName");
           // guna2datagrid.Columns.Add("userpass", "Password");
            guna2datagrid.Columns.Add("role", "Role");


            // Set visibility of columns
            guna2datagrid.Columns["userId"].Visible = false; // Hide User ID column

            // Set width of columns
            guna2datagrid.Columns["srNumber"].Width = 50;
            guna2datagrid.Columns["uname"].Width = 100;
            guna2datagrid.Columns["username"].Width = 150;
           // guna2datagrid.Columns["userpass"].Width = 100;
            guna2datagrid.Columns["role"].Width = 200;

            guna2datagrid.DefaultCellStyle.Font = new Font("Segue", 18);
            guna2datagrid.RowTemplate.Height = 40;

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
            guna2datagrid.Columns.Add(deleteColumn);

            // Load data from the database into the DataGridView
           // guna2datagrid.CellClick += guna2datagrid_CellContentClick;
        }
        private void FrmAddUser_UserAddedOrUpdated(object sender, EventArgs e)
        {
            // Refresh data from the database
            guna2datagrid.Rows.Clear(); // Clear the existing rows before loading new data

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
            foreach (DataGridViewRow row in guna2datagrid.Rows)
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
            guna2datagrid.Rows.Clear(); // Clear the existing rows before loading new data

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
                            string role = reader["role"].ToString();

                            // Add row to DataGridView without the password column
                            guna2datagrid.Rows.Add(srNumber, userId, uname, username, role);
                            srNumber++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
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
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void frmAddUserView_Load(object sender, EventArgs e)
        {
            LoadDataFromDatabase();
        }

        private void guna2datagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && guna2datagrid.Columns[e.ColumnIndex].Name == "Delete")
            {
                int userId = Convert.ToInt32(guna2datagrid.Rows[e.RowIndex].Cells["userId"].Value);

                if (guna2datagrid.Columns[e.ColumnIndex].Name == "Delete")
                {
                    string role = guna2datagrid.Rows[e.RowIndex].Cells["role"].Value.ToString();

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

                            // Perform the deletion operation
                            DeleteUser(userId);
                            guna2datagrid.Rows.Clear();

                            LoadDataFromDatabase();
                        }

                    }
                }
            }
        }
    }
}