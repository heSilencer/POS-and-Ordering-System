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

namespace Restaurant_POS_and_Ordering_Sytem.Models
{
    public partial class frmProductsAdd : Form
    {
        private int productId;
        public Action<object, EventArgs> ProductUpdated { get; internal set; }

        public frmProductsAdd()
        {
            InitializeComponent();
            LoadCategoryNames();
        }
        

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Clear the image before closing the form
            productImage.Image = null;

            // Close the form
            this.Close();
        }

        private void categorycmbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Access the selected category name
            if (categorycmbx.SelectedItem != null)
            {
                // Access the selected category name
                string selectedCategory = categorycmbx.SelectedItem.ToString();

                // Your code using selectedCategory
            }
        }
        private void LoadCategoryNames()
        {
            // Replace the connection string with your actual MySQL database connection string
            string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Replace "tbl_category" with the actual table name in your database
                string query = "SELECT catName FROM tbl_category";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Clear existing items in the Guna ComboBox
                        categorycmbx.Items.Clear();

                        // Check if there are rows in the result set
                        if (reader.HasRows)
                        {
                            // Loop through each row
                            while (reader.Read())
                            {
                                // Assuming "catName" is a string column in your table
                                string categoryName = reader["catName"].ToString();

                                // Add the categoryName to the Guna ComboBox
                                categorycmbx.Items.Add(categoryName);
                            }
                        }
                        else
                        {
                            // Handle the case when there are no rows in the result set
                        }
                    }
                }
            }
        }

        private void frmProductsAdd_Load(object sender, EventArgs e)
        {
            LoadCategoryNames();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Choose Image(*.jpg;*.png;*.png;)|*.jpg;*.png;*.png;";
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                productImage.Image = Image.FromFile(openFileDialog.FileName);
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

                    string productName = txttablename.Text;
                    decimal price;
                    if (string.IsNullOrEmpty(productName))
                    {
                        MessageBox.Show("Please Input a Product name.");
                        return;
                    }
                    if (string.IsNullOrEmpty(txtlblprice.Text))
                    {
                        MessageBox.Show("Please input a price.");
                        return;
                    }

                    if (!decimal.TryParse(txtlblprice.Text, out price))
                    {
                        MessageBox.Show("Invalid price. Please enter a valid numeric value.");
                        return;
                    }

                    string category = categorycmbx.SelectedItem?.ToString();

                    if (string.IsNullOrEmpty(category))
                    {
                        MessageBox.Show("Please select a category.");
                        return;
                    }

                    if (productImage.Image == null)
                    {
                        DialogResult result = MessageBox.Show("You haven't selected an image. Do you want to continue without an image?", "Confirmation", MessageBoxButtons.YesNo);

                        if (result == DialogResult.No)
                        {
                            // User chose not to continue without an image
                            return;
                        }
                        // If the user chooses to continue without an image, your code can proceed without further checks.
                    }

                    int categoryId = GetCategoryId(category);

                    if (categoryId == -1)
                    {
                        MessageBox.Show("The selected category does not exist. Please choose a valid category.");
                        return;
                    }


                    byte[] img = null;

                    if (productImage.Image != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            // Save the image to the MemoryStream
                            productImage.Image.Save(ms, productImage.Image.RawFormat);
                            img = ms.ToArray();
                        }
                    }

                    string insertUpdateQuery;

                    if (productId == 0)
                    {
                        // Insert a new product
                        insertUpdateQuery = "INSERT INTO tbl_products (prodName, prodPrice, catID, prodcategory, prodImage) " +
                                            "VALUES (@prodName, @prodPrice, @catID, @prodcategory, @prodImage)";
                    }
                    else
                    {
                        // Update an existing product
                        insertUpdateQuery = "UPDATE tbl_products SET prodName = @prodName, prodPrice = @prodPrice, " +
                                            "catID = @catID, prodcategory = @prodcategory" +
                                            (img != null ? ", prodImage = @prodImage " : "") +
                                            "WHERE prodID = @prodID";
                    }

                    using (MySqlCommand command = new MySqlCommand(insertUpdateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@prodID", productId);
                        command.Parameters.AddWithValue("@prodName", productName);
                        command.Parameters.AddWithValue("@prodPrice", price);
                        command.Parameters.AddWithValue("@catID", categoryId);
                        command.Parameters.AddWithValue("@prodcategory", category);

                        // Add image parameter only if it's not null
                        if (img != null)
                            command.Parameters.AddWithValue("@prodImage", img);

                        command.ExecuteNonQuery();

                        MessageBox.Show(productId == 0 ? "Product added successfully!" : "Product updated successfully!");

                        ProductUpdated?.Invoke(this, EventArgs.Empty);

                        txttablename.Text = "";
                        txtlblprice.Text = "";
                        categorycmbx.SelectedItem = null;

                        // Dispose of the image properly
                        if (productImage != null && productImage.Image != null)
                        {
                            productImage.Image.Dispose();
                            productImage.Image = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }







        private int GetCategoryId(string categoryName)
        {
            // Replace the connection string with your actual MySQL database connection string
            string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Replace "tbl_category" with the actual table name in your database
                string query = "SELECT catID FROM tbl_category WHERE catName = @catName";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@catName", categoryName);

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }

            return -1; // Return -1 if category ID is not found
        }
            
        internal void SetProductInfo(int productId, string v1, decimal v2, byte[] v3)
        {
            this.productId = productId;
            txttablename.Text = v1;
            txtlblprice.Text = v2.ToString();

            // Use the byteArrayToImage method to convert byte array to Image
            if (productImage != null)
            {
                // Use the byteArrayToImage method to convert byte array to Image
                productImage.Image = byteArrayToImage(v3);
            }
            if (productId > 0)
            {
                btnSave.Text = "Update";
            }
        }
        private Image byteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }
        }
      
    }
}

