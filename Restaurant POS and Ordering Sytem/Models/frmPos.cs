using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_POS_and_Ordering_Sytem.Models
{
    public partial class frmPos : Form
    {
        string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";


        public string UserRole { get; set; }
        private string username;
        public frmPos(string username)
        {
            InitializeComponent();
            this.username = username;

            guna2DataGridView1.Columns.Add("Sr#", "Sr#");
            guna2DataGridView1.Columns.Add("Name", "Name");
            guna2DataGridView1.Columns.Add("Qty", "Qty");
            guna2DataGridView1.Columns.Add("Price", "Price");
            guna2DataGridView1.Columns.Add("Amount", "Amount");

            // Adjust the font size
            guna2DataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 13);

            // Set the height of the header
            guna2DataGridView1.ColumnHeadersHeight = 40; // Adjust the height as needed

            // Set column widths
            guna2DataGridView1.Columns["Sr#"].Width = 50;
            guna2DataGridView1.Columns["Name"].Width = 200; // Adjust the width as needed
            guna2DataGridView1.Columns["Qty"].Width = 50;
            guna2DataGridView1.Columns["Price"].Width = 80;
            guna2DataGridView1.Columns["Amount"].Width = 100;

            // Add "Delete" column with delete icon
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
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

            if (UserRole == "admin")
            {
                // Admin logout behavior (go to MainForm)
                MainForm mf = new MainForm(username);
                mf.Show();
            }
            else if (UserRole == "cashier")
            {
                // Cashier logout behavior (go to SubForm)
                Subform sf = new Subform(username);
                sf.Show();
            }

            this.Close();
        }

        private void frmPos_Load(object sender, EventArgs e)
        {
            AddCategory();
            ShowProducts("All Categories");

        }
        private void AddCategory()
        {
            string qry = "Select * from tbl_category";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(qry, connection))
                {
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();

                        connection.Open(); // Open the connection before executing the command
                        da.Fill(dt); // Corrected line

                        CategoryPanel.Controls.Clear();

                        // Add a button for "All Categories"
                        Guna.UI2.WinForms.Guna2Button allCategoriesButton = new Guna.UI2.WinForms.Guna2Button();
                        allCategoriesButton.FillColor = Color.FromArgb(64, 64, 64);
                        allCategoriesButton.Size = new Size(249, 45);
                        allCategoriesButton.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
                        allCategoriesButton.Text = "All Categories";
                        allCategoriesButton.Font = new Font("Segoe UI", 14, FontStyle.Bold); // Set the font size
                        allCategoriesButton.Click += BtnAllCategory_Click; // Attach the event handler
                        CategoryPanel.Controls.Add(allCategoriesButton);

                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                                b.FillColor = Color.FromArgb(64, 64, 64);
                                b.Size = new Size(249, 45);
                                b.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
                                b.Text = row["catName"].ToString();
                                b.Font = new Font("Segoe UI", 14, FontStyle.Bold); // Set the font size

                                b.Click += CategoryButton_Click;
                                CategoryPanel.Controls.Add(b);
                            }
                        }
                    }
                }
            }
        }

        private void BtnAllCategory_Click(object sender, EventArgs e)
        {
            // Handle the "All Categories" button click
            // You can implement the logic to show all categories here
            ShowProducts("All Categories"); 
        }
        private void ShowProducts(string category)
        {
            string qry;

            if (category == "All Categories")
            {
                qry = "SELECT * FROM tbl_products";
            }
            else
            {
                qry = "SELECT * FROM tbl_products WHERE prodcategory = @category";
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(qry, connection))
                {
                    if (category != "All Categories")
                    {
                        cmd.Parameters.AddWithValue("@category", category);
                    }

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();

                        connection.Open();
                        da.Fill(dt);

                        ProductPanel.Controls.Clear();

                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                ucProduct productControl = new ucProduct();
                                productControl.Pname = row["prodName"].ToString();
                                productControl.PImage = ConvertByteArrayToImage((byte[])row["prodImage"]);
                               
                                productControl.id = Convert.ToInt32(row["prodID"]);
                                productControl.PPrice = row["prodPrice"].ToString();
                                productControl.PCategory = row["prodcategory"].ToString();

                                // Add an event handler to handle the product selection
                                productControl.onSelect += ProductControl_onSelect;

                                ProductPanel.Controls.Add(productControl);
                            }
                        }
                        else
                        {
                            // Display a message or handle the case when no products are found.
                        }
                    }
                }
            }
        }

        private Image ConvertByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                return null;

            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }


        private void CategoryButton_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button clickedButton = (Guna.UI2.WinForms.Guna2Button)sender;
            string categoryName = clickedButton.Text;
            FilterProductsByCategory(categoryName);
        }


        private void ProductControl_onSelect(object sender, EventArgs e)
        {
            // Your event handler code here
            ucProduct selectedProduct = (ucProduct)sender;

            // Add the selected product to the DataGridView
            AddProductToDataGridView(selectedProduct);
        }

        private void ProductPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        private void FilterProductsByCategory(string category)
        {
            // Call the ShowProducts method with the selected category
            ShowProducts(category);

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text.ToLower(); // Convert the search query to lowercase for case-insensitive search

            foreach (Control control in ProductPanel.Controls)
            {
                if (control is ucProduct)
                {
                    ucProduct productControl = (ucProduct)control; // Declare productControl within the loop

                    // Check if the search query is empty or the product name contains the search query
                    bool isVisible = string.IsNullOrEmpty(searchQuery) || productControl.Pname.ToLower().Contains(searchQuery);

                    // If the search query is empty or the product name contains the search query, make the control visible; otherwise, hide it
                    productControl.Visible = isVisible;
                }
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == guna2DataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                guna2DataGridView1.Rows.RemoveAt(e.RowIndex);
                UpdateSerialNumbers(); // Update Sr# after deleting a row
            }
        }
        private void UpdateSerialNumbers()
        {
            // Update Sr# for each row after deletion
            for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
            {
                guna2DataGridView1.Rows[i].Cells["Sr#"].Value = (i + 1).ToString();
            }
        }
        private void AddProductToDataGridView(ucProduct product)
        {
            DataGridViewRow existingRow = null;

            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (row.Cells["Name"].Value != null && row.Cells["Name"].Value.ToString() == product.Pname)
                {
                    existingRow = row;
                    break;
                }
            }

            if (existingRow != null)
            {
                // If the product is already in the DataGridView, increment the quantity
                int currentQty = Convert.ToInt32(existingRow.Cells["Qty"].Value);
                existingRow.Cells["Qty"].Value = (currentQty + 1).ToString();

                // Update the amount based on the new quantity
                double qty = Convert.ToDouble(existingRow.Cells["Qty"].Value);
                double price = Convert.ToDouble(existingRow.Cells["Price"].Value);
                existingRow.Cells["Amount"].Value = (qty * price).ToString();
            }
            else
            {
                // If the product is not in the DataGridView, add a new row
                int rowIndex = guna2DataGridView1.Rows.Add();

                // Set values in the DataGridView for the selected product
                guna2DataGridView1.Rows[rowIndex].Cells["Sr#"].Value = (rowIndex + 1).ToString();
                guna2DataGridView1.Rows[rowIndex].Cells["Name"].Value = product.Pname;
                guna2DataGridView1.Rows[rowIndex].Cells["Qty"].Value = "1"; // Default quantity is 1, you can modify this based on your logic
                guna2DataGridView1.Rows[rowIndex].Cells["Price"].Value = product.PPrice;

                // Calculate and set the amount (Qty * Price)
                double qty = Convert.ToDouble(guna2DataGridView1.Rows[rowIndex].Cells["Qty"].Value);
                double price = Convert.ToDouble(guna2DataGridView1.Rows[rowIndex].Cells["Price"].Value);
                guna2DataGridView1.Rows[rowIndex].Cells["Amount"].Value = (qty * price).ToString();
            }
        }
    }

}
