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

namespace Restaurant_POS_and_Ordering_Sytem.Models
{
    public partial class frmStaffcategoryAdd : Form
    {
        private int categoryId;
        public event EventHandler CategoryUpdated;

        public frmStaffcategoryAdd()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void SetCategoryInfo(int id, string categoryName)
        {
            categoryId = id;
            lblcat.Text = categoryName;
            if (categoryId > 0)
            {
                btnSave.Text = "Update";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string categoryName = lblcat.Text;

                    if (string.IsNullOrEmpty(categoryName))
                    {
                        guna2MessageDialog2.Show("Please Input a Staff Category name.");
                        return;
                    }
                    if (categoryId == 0)
                    {
                        // Insert a new category
                        string insertQuery = "INSERT INTO tbl_staffcategory (catName) VALUES (@catName)";
                        using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@catName", categoryName);
                            insertCommand.ExecuteNonQuery();

                            // Clear the textbox after adding a new category
                            lblcat.Text = "";

                            // Directly refresh the DataGridView

                            // Close the current form
                            DialogResult result = MessageBox.Show("Successfully added");
                            OnCategoryUpdated();

                        }
                    }
                    else
                    {
                        // Update an existing category
                        string updateQuery = "UPDATE tbl_staffcategory SET catName = @catName WHERE staffcatID = @staffcatID";
                        using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@staffcatID", categoryId);
                            updateCommand.Parameters.AddWithValue("@catName", categoryName);
                            updateCommand.ExecuteNonQuery();

                            // Clear the textbox after updating an existing category
                            lblcat.Text = "";

                            DialogResult result = MessageBox.Show("Successfully updated");
                            OnCategoryUpdated();
                            this.Close();


                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }
        protected virtual void OnCategoryUpdated()
        {
            CategoryUpdated?.Invoke(this, EventArgs.Empty);
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
