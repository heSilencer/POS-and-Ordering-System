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

                    string categoryName = lblcat.Text.Trim();

                    if (string.IsNullOrEmpty(categoryName))
                    {
                        guna2MessageDialog1.Show("Please Input a Staff Category name.");
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
                            DialogResult result = guna2MessageDialog1.Show("Successfully added");
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

                            DialogResult result = guna2MessageDialog1.Show("Successfully updated");
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

        private void lblcat_TextChanged(object sender, EventArgs e)
        {
            if (lblcat == null || string.IsNullOrEmpty(lblcat.Text))
            {
                return;
            }

            // Capitalize the first character of the text
            lblcat.Text = char.ToUpper(lblcat.Text[0]) + lblcat.Text.Substring(1);

            // Set the caret position to the end of the text
            lblcat.SelectionStart = lblcat.Text.Length;
        }
    }
}
