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
using System.Drawing.Imaging;

namespace Restaurant_POS_and_Ordering_Sytem.Models
{
    public partial class frmProductsAdd : Form
    {
        private int productId;
        public Action<object, EventArgs> ProductUpdated { get; internal set; }
        string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";

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
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string productName = txttablename.Text.Trim();
                    decimal price;

                    // Validate product name
                    if (string.IsNullOrEmpty(productName))
                    {
                        MessageBox.Show("Please input a product name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Validate price
                    if (!decimal.TryParse(txtlblprice.Text, out price))
                    {
                        MessageBox.Show("Invalid price. Please enter a valid numeric value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string category = categorycmbx.SelectedItem?.ToString();

                    // Validate category selection
                    if (string.IsNullOrEmpty(category))
                    {
                        MessageBox.Show("Please select a category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int categoryId = GetCategoryId(category);

                    // Validate category ID
                    if (categoryId == -1)
                    {
                        MessageBox.Show("The selected category does not exist. Please choose a valid category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    byte[] img = null;

                    // Check if an image is selected
                    if (productImage.Image != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            // Get all available image formats
                            ImageFormat[] formats = { ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Gif, ImageFormat.Bmp, ImageFormat.Tiff, ImageFormat.Icon };

                            // Iterate through each format and try to save the image
                            foreach (ImageFormat format in formats)
                            {
                                try
                                {
                                    // Save the image to the MemoryStream with the current format
                                    productImage.Image.Save(ms, format);
                                    img = ms.ToArray();
                                    break; // Exit the loop if the image is successfully saved
                                }
                                catch (Exception)
                                {
                                    // If saving fails, try the next format
                                    continue;
                                }
                            }
                        }
                    }

                    string query;

                    // Determine whether to insert or update the product
                    if (productId == 0)
                    {
                        // Insert a new product
                        query = "INSERT INTO tbl_products (prodName, prodPrice, catID, prodcategory, prodImage) " +
                                "VALUES (@prodName, @prodPrice, @catID, @prodcategory, @prodImage)";
                    }
                    else
                    {
                        // Update an existing product
                        query = "UPDATE tbl_products SET prodName = @prodName, prodPrice = @prodPrice, " +
                                "catID = @catID, prodcategory = @prodcategory";

                        // Add image update part conditionally
                        if (img != null)
                        {
                            query += ", prodImage = @prodImage";
                        }

                        query += " WHERE prodID = @prodID";
                    }

                    // Execute the SQL command
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@prodID", productId);
                        command.Parameters.AddWithValue("@prodName", productName);
                        command.Parameters.AddWithValue("@prodPrice", price);
                        command.Parameters.AddWithValue("@catID", categoryId);
                        command.Parameters.AddWithValue("@prodcategory", category);

                        // Add image parameter only if an image is selected or for update operation
                        if (img != null)
                        {
                            command.Parameters.AddWithValue("@prodImage", img);
                        }

                        int rowsAffected = command.ExecuteNonQuery();

                        // Show success message
                        MessageBox.Show(productId == 0 ? "Product added successfully!" : "Product updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Invoke the ProductUpdated event
                        ProductUpdated?.Invoke(this, EventArgs.Empty);

                        // Clear input fields
                        txttablename.Text = "";
                        txtlblprice.Text = "";
                        categorycmbx.SelectedItem = null;

                        // Dispose of the image properly
                        if (productImage != null && productImage.Image != null)
                        {
                            productImage.Image.Dispose();
                            productImage.Image = null;
                        }

                        // Close the form if an update was successful
                        if (rowsAffected > 0 && productId != 0)
                        {
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors
                MessageBox.Show($"An unexpected error occurred: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

