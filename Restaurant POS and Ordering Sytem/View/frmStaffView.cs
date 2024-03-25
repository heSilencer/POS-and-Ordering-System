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
using MySql.Data.MySqlClient;
using System.IO;

namespace Restaurant_POS_and_Ordering_Sytem.View
{
    public partial class frmStaffView : SampleView
    {
        string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";

        public frmStaffView()
        {
            InitializeComponent();
            guna2DataGridView1.Columns.Add("srNumber", "Sr#");
            guna2DataGridView1.Columns["srNumber"].DefaultCellStyle.Font = new Font("Segoe UI", 14);

            guna2DataGridView1.Columns.Add("staffID", "Staff ID");
            guna2DataGridView1.Columns["staffID"].Visible = false;

            guna2DataGridView1.Columns.Add("staffFname", "First Name");
            guna2DataGridView1.Columns["staffFname"].DefaultCellStyle.Font = new Font("Segoe UI", 14);

            guna2DataGridView1.Columns.Add("staffLname", "Last Name");
            guna2DataGridView1.Columns["staffLname"].DefaultCellStyle.Font = new Font("Segoe UI", 14);

            guna2DataGridView1.Columns.Add("staffAddress", "Address");
            guna2DataGridView1.Columns["staffAddress"].DefaultCellStyle.Font = new Font("Segoe UI", 14);

            guna2DataGridView1.Columns.Add("staffPhone", "Phone");
            guna2DataGridView1.Columns["staffPhone"].DefaultCellStyle.Font = new Font("Segoe UI", 14);

            guna2DataGridView1.Columns.Add("staffEmail", "Email");
            guna2DataGridView1.Columns["staffEmail"].DefaultCellStyle.Font = new Font("Segoe UI", 14);

            guna2DataGridView1.Columns.Add("staffcatID", "Category ID");
            guna2DataGridView1.Columns["staffcatID"].Visible = false;

            guna2DataGridView1.Columns.Add("Staff Category", "Staff Category");
            guna2DataGridView1.Columns["Staff Category"].DefaultCellStyle.Font = new Font("Segoe UI", 14);

            // Add staff image column
            DataGridViewImageColumn staffImageColumn = new DataGridViewImageColumn();
            staffImageColumn.Name = "staffImage";
            staffImageColumn.HeaderText = "Staff Image";
            staffImageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
            staffImageColumn.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            guna2DataGridView1.Columns.Add(staffImageColumn);

            // Add update column
            DataGridViewImageColumn updateColumn = new DataGridViewImageColumn();
            updateColumn.Image = Properties.Resources.Updateicon; // Replace with your actual update icon
            updateColumn.Name = "Update";
            updateColumn.HeaderText = "";
            updateColumn.HeaderCell.Style.NullValue = "";
            updateColumn.Width = 30;
            updateColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            updateColumn.FillWeight = 30;
            updateColumn.MinimumWidth = 30;
            updateColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            guna2DataGridView1.Columns.Add(updateColumn);

            // Add delete column
            DataGridViewImageColumn deleteColumn = new DataGridViewImageColumn();
            deleteColumn.Image = Properties.Resources.deleteicon; // Replace with your actual delete icon
            deleteColumn.Name = "Delete";
            deleteColumn.HeaderText = "";
            deleteColumn.HeaderCell.Style.NullValue = "";
            deleteColumn.Width = 30;
            deleteColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            deleteColumn.FillWeight = 30;
            deleteColumn.MinimumWidth = 30;
            deleteColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            guna2DataGridView1.Columns.Add(deleteColumn);

            // Set the default font for the entire DataGridView
            guna2DataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 12);

            // Adjust row height
            int customRowHeight = 130; // Adjust this value as needed
            guna2DataGridView1.RowTemplate.Height = customRowHeight;

            // Load data and attach event handler
            LoadStaffDataFromDatabase();
            guna2DataGridView1.CellClick += GunaDataGridView1_CellClick;

 
        }
        private void FrmStaffAdd_staffUpdated(object sender, EventArgs e)
        {
            guna2DataGridView1.Rows.Clear();
            LoadStaffDataFromDatabase(); // You should implement this method to reload data
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            
        }
        public override void btnAdd_Click(object sender, EventArgs e)
        {
            var addSatffForm = new frmStaffAdd();
          
            addSatffForm.StaffUpdated += FrmStaffAdd_staffUpdated; // Subscribe to the event
            MainClass.BlurbackGround(addSatffForm);

        }
        public override void txtSearch_TextChanged(object sender, EventArgs e)
        {

            string searchText = txtSearch.Text.Trim().ToLower();

            // Filter the rows based on the search text
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                string fname = row.Cells["staffFname"].Value.ToString().ToLower();
                string lname = row.Cells["staffLname"].Value.ToString().ToLower();
                string staffCategory = row.Cells["Staff Category"].Value.ToString().ToLower();

                if (fname.Contains(searchText) || lname.Contains(searchText) || staffCategory.Contains(searchText))
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
            }
        }
        private async void GunaDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && (guna2DataGridView1.Columns[e.ColumnIndex].Name == "Delete" || guna2DataGridView1.Columns[e.ColumnIndex].Name == "Update"))
            {
                int staffId = Convert.ToInt32(guna2DataGridView1.Rows[e.RowIndex].Cells["staffID"].Value);

                if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "Delete")
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this staff?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        await DeleteStaff(staffId);
                        MessageBox.Show("Staff deleted successfully");

                        guna2DataGridView1.Rows.Clear();
                        LoadStaffDataFromDatabase();
                    }
                }
                else if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "Update")
                {
                     await UpdateStaff(staffId);
                }
            }
        }

        private async Task DeleteStaff(int staffId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    string deleteQuery = "DELETE FROM tbl_staff WHERE staffID = @staffId";

                    using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@staffId", staffId);
                        await deleteCommand.ExecuteNonQueryAsync();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        private void LoadStaffDataFromDatabase()
        {
            guna2DataGridView1.Rows.Clear();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT staffID, staffFname, staffLname, staffAddress, staffPhone, staffEmail, staffcatID, `Staff Category`, staffImage FROM tbl_staff";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        int srNumber = 1;

                        while (reader.Read())
                        {
                            string staffID = reader["staffID"].ToString();
                            string staffFname = reader["staffFname"].ToString();
                            string staffLname = reader["staffLname"].ToString();
                            string staffAddress = reader["staffAddress"].ToString();
                            string staffPhone = reader["staffPhone"].ToString();
                            string staffEmail = reader["staffEmail"].ToString();
                            string staffcatID = reader["staffcatID"].ToString();
                            string staffCategory = reader["Staff Category"].ToString();

                            // Retrieve image data as byte array from database
                            byte[] imageData = (byte[])reader["staffImage"];

                            // Convert byte array to Image
                            Image image = null;
                            if (imageData != null && imageData.Length > 0)
                            {
                                using (MemoryStream ms = new MemoryStream(imageData))
                                {
                                    image = Image.FromStream(ms);
                                }
                            }

                            // Add staff data to DataGridView
                            guna2DataGridView1.Rows.Add(srNumber, staffID, staffFname, staffLname, staffAddress, staffPhone, staffEmail, staffcatID, staffCategory, image);
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

        private async Task UpdateStaff(int staffId)
        {
            var editForm = new frmStaffAdd();

            // Retrieve staff information using GetStaffInfo method
            StaffInfo staffInfo = GetStaffInfo(staffId);

            // Set staff information in the frmStaffAdd form using SetStaffInfo method
            editForm.SetStaffInfo(staffId, staffInfo.Fname, staffInfo.Lname, staffInfo.Address, staffInfo.Phone, staffInfo.Email, staffInfo.Category, staffInfo.ImageData);

            // Subscribe to the StaffUpdated event
            editForm.StaffUpdated += FrmStaffAdd_staffUpdated;

            // Show the form
            MainClass.BlurbackGround(editForm);

        }

        // Method to retrieve staff information from the database
        private StaffInfo GetStaffInfo(int staffId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string selectQuery = "SELECT staffFname, staffLname, staffEmail, staffPhone, staffAddress, `Staff Category`, staffImage FROM tbl_staff WHERE staffID = @staffId";

                    using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@staffId", staffId);

                        using (MySqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                StaffInfo staffInfo = new StaffInfo
                                {
                                    Fname = reader["staffFname"].ToString(),
                                    Lname = reader["staffLname"].ToString(),
                                    Email = reader["staffEmail"].ToString(),
                                    Phone = reader["staffPhone"].ToString(),
                                    Address = reader["staffAddress"].ToString(),
                                    Category = reader["Staff Category"].ToString(),
                                    ImageData = reader["staffImage"] as byte[]
                                };

                                return staffInfo;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

                return null;
            }
        }
        public class StaffInfo
        {
            public int StaffId { get; set; }
            public string Fname { get; set; }
            public string Lname { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Category { get; set; }
            public byte[] ImageData { get; set; }
        }

    }
}