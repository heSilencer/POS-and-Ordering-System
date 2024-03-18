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
namespace Restaurant_POS_and_Ordering_Sytem.View
{
    public partial class frmCategoryView : SampleView

    {
        string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";

        public frmCategoryView()
        {
            InitializeComponent();

            // Assuming you have a GunaDataGridView named gunaDataGridView1 on your form
            // Add columns manually or in the designer
            cat_datagridview.Columns.Add("srNumber", "Sr#");
            cat_datagridview.Columns.Add("catID", "Category ID"); // Set catID column to be invisible
            cat_datagridview.Columns.Add("catName", "Category Name");

            cat_datagridview.Columns["catID"].Visible = false;
            cat_datagridview.Columns["srNumber"].Width = 150;
            //cat_datagridview.Columns["catName"].Width = 100;

            cat_datagridview.DefaultCellStyle.Font = new Font("Segue", 14);
            cat_datagridview.RowTemplate.Height = 40;
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

            cat_datagridview.Columns.Add(updateColumn);

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
            cat_datagridview.Columns.Add(deleteColumn);



            // Handle the CellClick event to perform actions when the buttons are clicked
            cat_datagridview.CellClick += GunaDataGridView1_CellClick;
        }

        private void frmCategoryView_Load(object sender, EventArgs e)
        {
            //LoadData();

        }
        public override void btnAdd_Click(object sender, EventArgs e)
        {


            var frmAddCategory = new frmCategoryAdd();
            frmAddCategory.ProductUpdated += FrmCategoryAdd_CategoryUpdated;
            MainClass.BlurbackGround(frmAddCategory);



        }

        public override void txtSearch_TextChanged(object sender, EventArgs e)
        {

            string searchText = txtSearch.Text.Trim();

            // Filter the rows based on the search text
            foreach (DataGridViewRow row in cat_datagridview.Rows)
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

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            
        }
        private void FrmCategoryAdd_CategoryUpdated(object sender, EventArgs e)
        {
            cat_datagridview.Rows.Clear();
            LoadDataFromDatabase(); // You should implement this method to reload data
        }

        private async void GunaDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && (cat_datagridview.Columns[e.ColumnIndex].Name == "Delete" || cat_datagridview.Columns[e.ColumnIndex].Name == "Update"))
            {
                int catId = Convert.ToInt32(cat_datagridview.Rows[e.RowIndex].Cells["catID"].Value);

                if (cat_datagridview.Columns[e.ColumnIndex].Name == "Delete")
                {
                    // Confirm deletion with the user
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this category?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                         await DeleteCategory(catId);
                       // MessageBox.Show("Category deleted successfully");

                        // Reload the data from the database after the action
                        cat_datagridview.Rows.Clear();
                        LoadDataFromDatabase();
                    }
                }
                else if (cat_datagridview.Columns[e.ColumnIndex].Name == "Update")
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
                try
                {
                    await connection.OpenAsync();

                    // Delete the category with the specified catId
                    string deleteQuery = "DELETE FROM tbl_category WHERE catId = @catId";

                    using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@catId", catId);
                        await deleteCommand.ExecuteNonQueryAsync();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void UpdateCategory(int catId)
        {
            // Implement your update logic here
            var editForm = new frmCategoryAdd();
            editForm.SetCategoryInfo(catId, GetCategoryName(catId));
            editForm.ProductUpdated += FrmCategoryAdd_CategoryUpdated;
            MainClass.BlurbackGround(editForm);


        }

        private string GetCategoryName(int catId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Retrieve the category name for the given catId
                    string selectQuery = "SELECT catName FROM tbl_category WHERE catId = @catId";

                    using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@catId", catId);

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

                    string query = "SELECT catID, catName FROM tbl_category"; // Replace YourTableName with your actual table name

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        int srNumber = 1;

                        while (reader.Read())
                        {
                            string catID = reader["catID"].ToString();
                            string catName = reader["catName"].ToString();

                            cat_datagridview.Rows.Add(srNumber, catID, catName);
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
    }


}


