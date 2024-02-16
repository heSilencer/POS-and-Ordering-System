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
    public partial class frmTableView : SampleView
    {
        string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";

        public frmTableView()
        {
            InitializeComponent();
            // Assuming you have a GunaDataGridView named gunaDataGridView1 on your form
            // Add columns manually or in the designer
            guna2DataGridView1.Columns.Add("srNumber", "Sr#");
            guna2DataGridView1.Columns.Add("tblID", "Table ID"); // Set catID column to be invisible
            guna2DataGridView1.Columns.Add("tblName", "Table Name");

            guna2DataGridView1.Columns["tblID"].Visible = false;
            guna2DataGridView1.Columns["srNumber"].Width = 150;
            //cat_datagridview.Columns["catName"].Width = 100;

            // Load data from the database into GunaDataGridView
            LoadTableDataFromDatabase();

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



            // Handle the CellClick event to perform actions when the buttons are clicked
            guna2DataGridView1.CellClick += GunaDataGridView2_CellClick;
        }

      
        public override void btnAdd_Click(object sender, EventArgs e)
        {
            frmTableAdd addTableForm = new frmTableAdd();
            addTableForm.TableUpdated += FrmTableAdd_TableUpdated; // Subscribe to the event
            addTableForm.ShowDialog();

        }
        public override void txtSearch_TextChanged(object sender, EventArgs e)
        {

            string searchText = txtSearch.Text.Trim();

            // Filter the rows based on the search text
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (row.Cells["tblName"].Value.ToString().ToLower().Contains(searchText.ToLower()))
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
            }
        }
        private void FrmTableAdd_TableUpdated(object sender, EventArgs e)
        {
            guna2DataGridView1.Rows.Clear();
            LoadTableDataFromDatabase(); // You should implement this method to reload data
        }

        private void GunaDataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is in the Delete or Update column
            if (e.RowIndex >= 0 && (guna2DataGridView1.Columns[e.ColumnIndex].Name == "Delete" || guna2DataGridView1.Columns[e.ColumnIndex].Name == "Update"))
            {
                int tableId = Convert.ToInt32(guna2DataGridView1.Rows[e.RowIndex].Cells["tblID"].Value);

                if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "Delete")
                {
                    // Confirm deletion with Yes/No dialog
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this table?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Handle the delete action
                        DeleteTable(tableId);

                        // Reload the data from the database after the deletion
                        guna2DataGridView1.Rows.Clear();
                        LoadTableDataFromDatabase();
                    }
                    // If the user clicks No, nothing happens, and the data is not reloaded
                }
                else if (guna2DataGridView1.Columns[e.ColumnIndex].Name == "Update")
                {
                    // Handle the update action
                    frmTableAdd editForm = new frmTableAdd();
                    editForm.Show();
                    UpdateTable(tableId);
                }
            }
        }

        private void DeleteTable(int tblId)
        {
            // Implement your delete logic here
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Delete the table with the specified tableId
                    string deleteQuery = "DELETE FROM tbl_Table WHERE tblID = @tblID";

                    using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@tblID", tblId);
                        deleteCommand.ExecuteNonQuery();

                        MessageBox.Show("Table deleted successfully");

                        // Raise the TableUpdated event
                        //OnTableUpdated();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        private void UpdateTable(int tableId)
        {
            // Implement your update logic here
            frmTableAdd editForm = new frmTableAdd();
            editForm.SetTableInfo(tableId, GetTableName(tableId));
            editForm.TableUpdated += FrmTableAdd_TableUpdated;
            editForm.ShowDialog();
        }
        private string GetTableName(int tableId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Retrieve the table name for the given tableId
                    string selectQuery = "SELECT tblName FROM tbl_Table WHERE tblID = @tblID";

                    using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@tblID", tableId);

                        // Execute the query and retrieve the table name
                        object result = selectCommand.ExecuteScalar();

                        if (result != null)
                        {
                            return result.ToString();
                        }
                        else
                        {
                            return "Table Name"; // Default or placeholder value
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return "Table Name"; // Default or placeholder value
                }
            }
        }


    private void LoadTableDataFromDatabase()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT tblID, tblName FROM tbl_Table";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        int srNumber = 1;

                        while (reader.Read())
                        {
                            string tblID = reader["tblID"].ToString();
                            string tblName = reader["tblName"].ToString();

                            guna2DataGridView1.Rows.Add(srNumber, tblID, tblName);
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
        private void FrmTableView_TableUpdated(object sender, EventArgs e)
        {
            guna2DataGridView1.Rows.Clear();
            LoadTableDataFromDatabase();
        }

        private void frmTableView_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
           
        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
