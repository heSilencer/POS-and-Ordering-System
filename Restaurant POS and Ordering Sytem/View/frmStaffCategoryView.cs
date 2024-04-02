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
    public partial class frmStaffCategoryView : SampleView
    {
        string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";

        public frmStaffCategoryView()
        {
            InitializeComponent();

            // Assuming you have a GunaDataGridView named gunaDataGridView1 on your form
            // Add columns manually or in the designer
            guna2DataGridView1.Columns.Add("srNumber", "Sr#");
            guna2DataGridView1.Columns.Add("staffcatID", "Category ID"); // Set catID column to be invisible
            guna2DataGridView1.Columns.Add("catName", "Category Name");

            guna2DataGridView1.Columns["staffcatID"].Visible = false;
            guna2DataGridView1.Columns["srNumber"].Width = 150;
            //cat_datagridview.Columns["catName"].Width = 100;

            // Load data from the database into GunaDataGridView
            LoadDataFromDatabase();

            DataGridViewImageColumn updateColumn = new DataGridViewImageColumn();
            updateColumn.Image = Properties.Resources.Updateicon; // Replace with your actual update icon
            updateColumn.Name = "Update";
            updateColumn.HeaderText = ""; // Set the header text to an empty string
            updateColumn.HeaderCell.Style.NullValue = "";
            updateColumn.Width = 50;
            updateColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            updateColumn.FillWeight = 50;
            updateColumn.MinimumWidth = 50;
            updateColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;

            guna2DataGridView1.Columns.Add(updateColumn);

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
            guna2DataGridView1.Columns.Add(deleteColumn);

            guna2DataGridView1.DefaultCellStyle.Font = new Font("Segue", 14);
            guna2DataGridView1.RowTemplate.Height = 40;

            // Handle the CellClick event to perform actions when the buttons are clicked
            guna2DataGridView1.CellClick += GunaDataGridView2_CellClick;
        }
        public override void btnAdd_Click(object sender, EventArgs e)
        {
            var addCategoryForm = new frmStaffcategoryAdd();
            addCategoryForm.CategoryUpdated += FrmCategoryAdd_CategoryUpdated; // Subscribe to the event
            MainClass.BlurbackGround(addCategoryForm);

        }
        public override void txtSearch_TextChanged(object sender, EventArgs e)
        {

            string searchText = txtSearch.Text.Trim();

            // Filter the rows based on the search text
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (row.Cells["catName"].Value.ToString().ToLower().Contains(searchText.ToLower()))
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
            }
        }
        private void FrmCategoryAdd_CategoryUpdated(object sender, EventArgs e)
        {
            guna2DataGridView1.Rows.Clear();
            LoadDataFromDatabase(); // You should implement this method to reload data
        }
        private async void GunaDataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && (guna2DataGridView1.Columns[e.ColumnIndex].Name == "Delete" || guna2DataGridView1.Columns[e.ColumnIndex].Name == "Update"))
            {
                int catId = Convert.ToInt32(guna2DataGridView1.Rows[e.RowIndex].Cells["staffcatID"].Value);
                string catName = guna2DataGridView1.Rows[e.RowIndex].Cells["catName"].Value.ToString().ToLower();

                if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "Delete")
                {
                    // Check if the category name is "Admin"
                    if (catName == "admin")
                    {
                        MessageBox.Show("Category with name 'Admin' cannot be deleted.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // Exit the method without further processing
                    }

                    // Confirm deletion with the user
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this category?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Show warning if staff will be deleted
                        DialogResult warningResult = MessageBox.Show("Deleting this category will also delete the staff Category  associated with it. Are you sure you want to proceed?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (warningResult == DialogResult.Yes)
                        {
                            await DeleteCategory(catId);

                            // Reload the data from the database after the action
                            guna2DataGridView1.Rows.Clear();
                            LoadDataFromDatabase();
                        }
                    }
                }
                else if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "Update")
                {
                    // Handle the update action asynchronously
                    UpdateCategory(catId);
                }
            }
        }
        private async Task DeleteCategory(int catId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Update the staff category in tbl_staff to 'N/A' where staffcatID matches
                        string updateStaffQuery = "UPDATE tbl_staff SET `Staff Category` = 'N/A' WHERE `staffcatID` = @staffcatID";

                        // Delete the category with the specified catId from tbl_staffcategory
                        string deleteCategoryQuery = "DELETE FROM tbl_staffcategory WHERE `staffcatID` = @staffcatID";

                        using (MySqlCommand updateStaffCommand = new MySqlCommand(updateStaffQuery, connection, transaction))
                        using (MySqlCommand deleteCategoryCommand = new MySqlCommand(deleteCategoryQuery, connection, transaction))
                        {
                            updateStaffCommand.Parameters.AddWithValue("@staffcatID", catId);
                            deleteCategoryCommand.Parameters.AddWithValue("@staffcatID", catId);

                            await updateStaffCommand.ExecuteNonQueryAsync();
                            await deleteCategoryCommand.ExecuteNonQueryAsync();
                        }

                        // Commit the transaction if all commands succeeded
                        transaction.Commit();

                        MessageBox.Show("Category deleted successfully");

                        // Reload the data from the database after the action
                        guna2DataGridView1.Rows.Clear();
                        LoadDataFromDatabase();
                    }
                    catch (Exception ex)
                    {
                        // Rollback the transaction if an error occurred
                        transaction.Rollback();
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }

        }

        private void UpdateCategory(int catId)
        {
            // Implement your update logic here
            var editForm = new frmStaffcategoryAdd();
            editForm.SetCategoryInfo(catId, GetCategoryName(catId));
            editForm.CategoryUpdated += FrmCategoryAdd_CategoryUpdated;
            MainClass.BlurbackGround(editForm);
            // Show the form as a dialog
      
        }


        private string GetCategoryName(int catId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Retrieve the category name for the given catId
                    string selectQuery = "SELECT catName FROM tbl_staffcategory WHERE staffcatID = @staffcatID";

                    using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@staffcatID", catId);

                        // Execute the query and retrieve the category name
                        object result = selectCommand.ExecuteScalar();

                        if (result != null)
                        {
                            return result.ToString();
                        }
                        else
                        {
                            return "Category Name"; // Default or placeholder value
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return "Category Name"; // Default or placeholder value
                }
            }
        }
        private void LoadDataFromDatabase()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT staffcatID, catName FROM tbl_staffcategory"; // Replace YourTableName with your actual table name

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        int srNumber = 1;

                        while (reader.Read())
                        {
                            string staffcatID = reader["staffcatID"].ToString();
                            string catName = reader["catName"].ToString();

                            guna2DataGridView1.Rows.Add(srNumber, staffcatID, catName);
                            srNumber++;
                        }

                        // Display the count in a MessageBox

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        private void frmCashierView_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            
        }
    }
}
