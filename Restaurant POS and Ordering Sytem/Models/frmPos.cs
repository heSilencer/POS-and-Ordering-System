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
    }

}
