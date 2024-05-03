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
    public partial class frmProductvView : SampleView
    {
        string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";
       
        public frmProductvView()
        {
            InitializeComponent();
            InitializeDataGridView();


        }
        private void InitializeDataGridView()
        {
            // Add columns manually or in the designer
            guna2DataGridViewProducts.Columns.Add("srNumber", "Sr#");
            guna2DataGridViewProducts.Columns["srNumber"].Width = 200; // Increase the width for "Sr#" column

            guna2DataGridViewProducts.Columns.Add("prodID", "Product ID"); // Set prodID column to be invisible
            guna2DataGridViewProducts.Columns.Add("prodName", "Product Name");
            guna2DataGridViewProducts.Columns["prodName"].Width = 400; // Increase the width for "Product Name" column

            guna2DataGridViewProducts.Columns.Add("prodPrice", "Product Price");
            guna2DataGridViewProducts.Columns["prodPrice"].Width = 150; // Increase the width for "Product Price" column

            guna2DataGridViewProducts.Columns.Add("prodCategory", "Product Category");
            guna2DataGridViewProducts.Columns["prodCategory"].Width = 200; // Set the width for "Product Category" column

            guna2DataGridViewProducts.DefaultCellStyle.Font = new Font("Segue", 18);


            DataGridViewImageColumn prodImageColumn = new DataGridViewImageColumn();
            prodImageColumn.Name = "prodImage";
            prodImageColumn.HeaderText = "Product Image";
            prodImageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;



            guna2DataGridViewProducts.Columns.Add(prodImageColumn);

            guna2DataGridViewProducts.Columns["prodID"].Visible = false;
            guna2DataGridViewProducts.Columns["srNumber"].Width = 150;
            guna2DataGridViewProducts.Columns["prodImage"].Visible = true;
            foreach (DataGridViewColumn column in guna2DataGridViewProducts.Columns)
            {
                if (column.Name == "prodPrice")
                {
                    column.DefaultCellStyle.Format = "C2";
                }
            }

            // Add other column setup code if needed

            DataGridViewImageColumn updateColumn = new DataGridViewImageColumn();
            updateColumn.Image = Properties.Resources.Updateicon; // Replace with your actual update icon
            updateColumn.Name = "Update";
            updateColumn.HeaderText = ""; // Set the header text to an empty string
            updateColumn.HeaderCell.Style.NullValue = "";
            updateColumn.Width = 30;
            updateColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            updateColumn.FillWeight = 30;
            updateColumn.MinimumWidth = 30;
            updateColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;

            guna2DataGridViewProducts.Columns.Add(updateColumn);

            DataGridViewImageColumn deleteColumn = new DataGridViewImageColumn();
            deleteColumn.Image = Properties.Resources.deleteicon; // Replace with your actual delete icon
            deleteColumn.Name = "Delete";
            deleteColumn.HeaderText = ""; // Set the header text to an empty string
            deleteColumn.HeaderCell.Style.NullValue = "";
            deleteColumn.Width = 30;
            deleteColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            deleteColumn.FillWeight = 30;
            deleteColumn.MinimumWidth = 30;
            deleteColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            guna2DataGridViewProducts.Columns.Add(deleteColumn);

            // Handle the CellClick event to perform actions when the buttons are clicked
            guna2DataGridViewProducts.CellClick += GunaDataGridViewProducts_CellClick;

            // Handle the CellFormatting event to adjust row height for "prodImage" column
            guna2DataGridViewProducts.CellFormatting += GunaDataGridViewProducts_CellFormatting;

            // Load data from the database into GunaDataGridView
            LoadProductDataFromDatabase();

        }

        public override void btnAdd_Click(object sender, EventArgs e)
        {
            //frmProductsAdd addProductForm = new frmProductsAdd();
            //addProductForm.ProductUpdated += FrmProductsAdd_ProductUpdated; // Subscribe to the event
            // addProductForm.ShowDialog();


            var frmAddCategory = new frmProductsAdd();
            frmAddCategory.ProductUpdated += FrmProductsAdd_ProductUpdated;
            MainClass.BlurbackGround(frmAddCategory);
        }

        public override void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();

            // Filter the rows based on the search text
            foreach (DataGridViewRow row in guna2DataGridViewProducts.Rows)
            {
                string prodName = row.Cells["prodName"].Value.ToString().ToLower();
                string prodCategory = row.Cells["prodCategory"].Value.ToString().ToLower();

                if (prodName.Contains(searchText) || prodCategory.Contains(searchText))
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
            }
        }

        private void FrmProductsAdd_ProductUpdated(object sender, EventArgs e)
        {
            guna2DataGridViewProducts.Rows.Clear();
            LoadProductDataFromDatabase(); // You should implement this method to reload data
        }

        private void GunaDataGridViewProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is in the Delete or Update column
            if (e.RowIndex >= 0 && (guna2DataGridViewProducts.Columns[e.ColumnIndex].Name == "Delete" || guna2DataGridViewProducts.Columns[e.ColumnIndex].Name == "Update"))
            {
                int productId = Convert.ToInt32(guna2DataGridViewProducts.Rows[e.RowIndex].Cells["prodID"].Value);

                if (guna2DataGridViewProducts.Columns[e.ColumnIndex].Name == "Delete")
                {
                    // Confirm deletion with Yes/No dialog
                    DialogResult result = MessageBox.Show("Are you sure you want to delete this product?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Show warning message
                        DialogResult warningResult = MessageBox.Show("Deleting this product will remove it from the POS and will be deleted permanently. Are you sure you want to proceed?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (warningResult == DialogResult.Yes)
                        {
                            // Handle the delete action
                            DeleteProduct(productId);

                            // Reload the data from the database after the deletion
                            guna2DataGridViewProducts.Rows.Clear();
                            LoadProductDataFromDatabase();
                        }
                        // If the user clicks No, nothing happens, and the data is not reloaded
                    }
                }
                else if (guna2DataGridViewProducts.Columns[e.ColumnIndex].Name == "Update")
                {
                    // Handle the update action
                    UpdateProduct(productId);
                }
            }
        }

        private void DeleteProduct(int prodId)
        {
            // Implement your delete logic here
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Delete the product with the specified prodId
                    string deleteQuery = "DELETE FROM tbl_products WHERE prodID = @prodID";

                    using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@prodID", prodId);
                        deleteCommand.ExecuteNonQuery();

                        MessageBox.Show("Product deleted successfully");

                        // Raise the ProductUpdated event
                        //OnProductUpdated();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void UpdateProduct(int productId)
        {
            // Implement your update logic here
            string prodCategory = GetProductCategory(productId); // Retrieve the product category

            frmProductsAdd editForm = Application.OpenForms.OfType<frmProductsAdd>().FirstOrDefault();

            if (editForm == null)
            {
                // If the form is not open, create a new instance
                editForm = new frmProductsAdd();
                editForm.ProductUpdated += FrmProductsAdd_ProductUpdated;
            }

            // Set the product information in the form
            editForm.SetProductInfo(productId, GetProductName(productId), GetProductPrice(productId), GetProductImage(productId), prodCategory);

            // Apply the blur background effect and show the form
            MainClass.BlurbackGround(editForm);
        }
        private string GetProductCategory(int productId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Retrieve the product category for the given productId
                    string selectQuery = "SELECT prodCategory FROM tbl_products WHERE prodID = @prodID";

                    using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@prodID", productId);

                        // Execute the query and retrieve the product category
                        object result = selectCommand.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            return result.ToString();
                        }
                        else
                        {
                            return ""; // Default or placeholder value
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return ""; // Default or placeholder value
                }
            }
        }


        private string GetProductName(int productId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Retrieve the product name for the given productId
                    string selectQuery = "SELECT prodName FROM tbl_products WHERE prodID = @prodID";

                    using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@prodID", productId);

                        // Execute the query and retrieve the product name
                        object result = selectCommand.ExecuteScalar();

                        if (result != null)
                        {
                            return result.ToString();
                        }
                        else
                        {
                            return "Product Name"; // Default or placeholder value
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return "Product Name"; // Default or placeholder value
                }
            }
        }

        private decimal GetProductPrice(int productId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string selectQuery = "SELECT prodPrice FROM tbl_products WHERE prodID = @prodID";

                    using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@prodID", productId);

                        object result = selectCommand.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            return (decimal)result;
                        }
                        else
                        {
                            return 0.00m; // Default or placeholder value
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return 0.00m; // Default or placeholder value
                }
            }
        }




        private byte[] GetProductImage(int productId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Retrieve the product image for the given productId
                    string selectQuery = "SELECT prodImage FROM tbl_products WHERE prodID = @prodID";

                    using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@prodID", productId);

                        // Execute the query and retrieve the product image
                        object result = selectCommand.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            // Check if the data type of prodImage is Byte[]
                            if (result.GetType() == typeof(byte[]))
                            {
                                return (byte[])result;
                            }
                            else
                            {
                                MessageBox.Show("Invalid data type for prodImage column.");
                                return new byte[0]; // Default or placeholder value
                            }
                        }
                        else
                        {
                            return new byte[0]; // Default or placeholder value
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                    return new byte[0]; // Default or placeholder value
                }
            }
        }


        private void LoadProductDataFromDatabase()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT prodID, prodName, prodPrice, prodImage,prodCategory FROM tbl_products";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        int srNumber = 1;

                        while (reader.Read())
                        {
                            string prodID = reader["prodID"].ToString();
                            string prodName = reader["prodName"].ToString();
                            decimal prodPrice = reader.GetDecimal(reader.GetOrdinal("prodPrice"));
                            string prodCategory = reader["prodCategory"].ToString();


                            // Check if the prodImage column is not DBNull
                            if (!reader.IsDBNull(reader.GetOrdinal("prodImage")))
                            {
                                // Get the index of the prodImage column
                                int imageIndex = reader.GetOrdinal("prodImage");

                                // Check if the data type of prodImage is Byte[]
                                if (reader.GetFieldType(imageIndex) == typeof(byte[]))
                                {
                                    // Get the byte array from the database
                                    byte[] imageData = (byte[])reader["prodImage"];

                                    // Convert the byte array to an Image
                                    Image prodImage = byteArrayToImage(imageData);

                                    // Add the row to the DataGridView
                                    guna2DataGridViewProducts.Rows.Add(srNumber, prodID, prodName, prodPrice, prodCategory,prodImage);
                                }
                                else
                                {
                                    MessageBox.Show("Invalid data type for prodImage column.");
                                }
                            }
                            else
                            {
                                // Handle the case when the prodImage column is DBNull
                                guna2DataGridViewProducts.Rows.Add(srNumber, prodID, prodName, prodPrice, prodCategory, null);
                            }
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

        private Image byteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }
        }

        private void GunaDataGridViewProducts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Check if it's the "prodImage" column and the cell has a value
            if (guna2DataGridViewProducts.Columns[e.ColumnIndex].Name == "prodImage" && e.Value != null)
            {
                // Set the row height to accommodate the image (adjust as needed)
                e.CellStyle.Padding = new Padding(0, 5, 0, 5); // Padding for spacing around the image
                e.CellStyle.SelectionBackColor = Color.FromArgb(192, 192, 255); // Background color when selected
                e.CellStyle.Font = new Font(e.CellStyle.Font.FontFamily, 12); // Font size for the cell text
                e.CellStyle.ForeColor = Color.Black; // Text color

                e.CellStyle.Font = new Font(e.CellStyle.Font.FontFamily, 12); // Font size for the cell text
                e.CellStyle.ForeColor = Color.Black; // Text color

                // Adjust the row height (adjust as needed)
                int desiredRowHeight = 150;
                if (guna2DataGridViewProducts.Rows[e.RowIndex].Height != desiredRowHeight)
                {
                    guna2DataGridViewProducts.Rows[e.RowIndex].Height = desiredRowHeight;
                }
            }
        }

       



        private void frmProductvView_Load(object sender, EventArgs e)
        {
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridViewProducts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           
        }
    }
}
