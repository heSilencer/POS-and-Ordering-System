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

        public frmCategoryAdd(Action<object, EventArgs> frmCategoryAdd_CategoryUpdated)
        {
            this.frmCategoryAdd_CategoryUpdated = frmCategoryAdd_CategoryUpdated;
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

                    string categoryName = lblcat.Text;

                    if (string.IsNullOrEmpty(categoryName))
                    {
                        MessageBox.Show("Please Input a Category name.");
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
                            // Close the current form
                            DialogResult result = MessageBox.Show("Successfully added");
                            OnCategoryUpdated();

                           
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
            ProductUpdated?.Invoke(this, EventArgs.Empty);
        }
        public void UpdateCategoryFormText()
        {
            btnSave.Text = "Update";
        }

    }
}
