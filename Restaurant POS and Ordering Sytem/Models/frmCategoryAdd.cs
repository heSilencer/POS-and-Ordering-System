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
using Restaurant_POS_and_Ordering_Sytem.View;

namespace Restaurant_POS_and_Ordering_Sytem.Models
{
    public partial class frmCategoryAdd : Form


    {
        public event EventHandler ProductUpdated;
        private int categoryId;
        private Action<object, EventArgs> frmCategoryAdd_CategoryUpdated;

        public frmCategoryAdd()
        {
            InitializeComponent();
        }

     

        public void SetCategoryInfo(int id, string categoryName)
        {
           
            categoryId = id;
            lblcat.Text = categoryName;

            // Check if it's an update, then change the button text
            if (categoryId > 0)
            {
                btnSave.Text = "Update";
            }
        }
        private void frmCategoryAdd_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
                        guna2MessageDialog1.Show("Please Input a Category name.");
                        return;
                    }

                    // Check if category already exists
                    bool categoryExists = CheckIfCategoryExists(categoryName, connection);
                    if (categoryExists)
                    {
                        guna2MessageDialog1.Show("This Category already exists.");
                        return;
                    }

                    if (categoryId == 0)
                    {
                        // Insert a new category
                        string insertQuery = "INSERT INTO tbl_category (catName) VALUES (@catName)";
                        using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@catName", categoryName);
                            insertCommand.ExecuteNonQuery();

                            // Clear the textbox after adding a new category
                            lblcat.Text = "";

                            // Directly refresh the DataGridView
                            ProductUpdated?.Invoke(this, EventArgs.Empty);
                            DialogResult result = guna2MessageDialog1.Show("Successfully added");
                        }
                    }
                    else
                    {
                        // Update an existing category
                        string updateQuery = "UPDATE tbl_category SET catName = @catName WHERE catId = @catId";
                        using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@catId", categoryId);
                            updateCommand.Parameters.AddWithValue("@catName", categoryName);
                            updateCommand.ExecuteNonQuery();

                            // Clear the textbox after updating an existing category
                            lblcat.Text = "";

                            DialogResult result = guna2MessageDialog1.Show("Successfully updated");
                            ProductUpdated?.Invoke(this, EventArgs.Empty);
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    guna2MessageDialog2.Show("Error: " + ex.Message);
                }
            }
        }

        private bool CheckIfCategoryExists(string categoryName, MySqlConnection connection)
        {
            string query = "SELECT COUNT(*) FROM tbl_category WHERE catName = @catName";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@catName", categoryName);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }
        protected virtual void OnCategoryUpdated()
        {
            ProductUpdated?.Invoke(this, EventArgs.Empty);
        }
        public void UpdateCategoryFormText()
        {
            btnSave.Text = "Update";
        }

        private void lblcat_TextChanged(object sender, EventArgs e)
        {

            // Check if the TextBox is null or empty
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
