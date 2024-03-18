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


        public string userRole { get; set; }
        private string username;
        private int userID;

        public frmPos(string username, int userID)
        {
            InitializeComponent();
            this.username = username;
            this.userID = userID;

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

        public frmPos(int userID, int mainId)
        {
            this.userID = userID;
            MainID = mainId;
        }

      

        public string OrderType;
        private int MainID;
        private string productName;
        private int quantity;
        private decimal amount;
        private decimal price;

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            OpenFormBasedOnRole(userRole, username, userID);

        }
        private void OpenFormBasedOnRole(string userRole, string username, int userID)
        {
            if (userRole == "admin")
            {
                MainForm mainForm = new MainForm(username, userID); // Pass the UserID to MainForm
                mainForm.Show();
                this.Hide();
            }
            else if (userRole == "cashier")
            {
                Subform subForm = new Subform(username, userID); // Pass the UserID to Subform
                subForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Unknown role: " + userRole);
            }
        }

        private void frmPos_Load(object sender, EventArgs e)
        {
            AddCategory();
            ShowProducts("All Categories");
            UpdateTotalAmount();

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

                                b.Click += new EventHandler(CategoryButton_Click);
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
                DataGridViewRow selectedRow = guna2DataGridView1.Rows[e.RowIndex];

                // Check if the quantity is greater than 1
                if (selectedRow.Cells["Qty"].Value != null && Convert.ToInt32(selectedRow.Cells["Qty"].Value) > 1)
                {
                    // Decrease the quantity by 1
                    int currentQty = Convert.ToInt32(selectedRow.Cells["Qty"].Value);
                    selectedRow.Cells["Qty"].Value = (currentQty - 1).ToString();

                    // Update the amount based on the new quantity
                    double qty = Convert.ToDouble(selectedRow.Cells["Qty"].Value);
                    double price = Convert.ToDouble(selectedRow.Cells["Price"].Value);
                    selectedRow.Cells["Amount"].Value = (qty * price).ToString();
                }
                else
                {
                    // If quantity is 1 or less, remove the entire row
                    guna2DataGridView1.Rows.RemoveAt(e.RowIndex);
                }

                UpdateSerialNumbers();
                UpdateTotalAmount();
            }
        }
        private void UpdateTotalAmount()
        {
            double totalAmount = 0; // Rename MainID to totalAmount

            // Sum all the amounts in the DataGridView
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (row.Cells["Amount"].Value != null)
                {
                    string amountString = row.Cells["Amount"].Value.ToString();
                    double amount;
                    // Validate and parse the amount
                    if (double.TryParse(amountString.Replace("$", ""), out amount))
                    {
                        totalAmount += amount;
                    }
                    else
                    {
                        // Handle invalid amount format gracefully
                        MessageBox.Show($"Invalid amount format: {amountString}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            lbltotal.Text = $"{totalAmount:C}"; // Assuming you want to display the amount as currency
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
            UpdateTotalAmount();
        }
        private bool CalculateTotalAmount(out double totalAmount)
        {
            totalAmount = 0;

            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (row.Cells["Amount"].Value != null)
                {
                    string amountString = row.Cells["Amount"].Value.ToString();
                    double amount;
                    // Validate and parse the amount
                    if (double.TryParse(amountString.Replace("$", ""), out amount))
                    {
                        totalAmount += amount;
                    }
                    else
                    {
                        // If any amount is invalid, return false
                        return false;
                    }
                }
            }

            return true;
        }

        private void lbltotal_Click(object sender, EventArgs e)
        {

        }

        private void BtnTakeAway_Click(object sender, EventArgs e)
        {
            lbltxtTable.Text = "";
            lbltxtWaiter.Text = "";
            lbltxtTable.Visible = false;
            lbltxtWaiter.Visible = false;
            OrderType = "Take Away";
        }

        private void btnAddnew_Click(object sender, EventArgs e)
        {
            lbltxtTable.Text = "";
            lbltxtWaiter.Text = "";
            OrderType = "";
            double totalAmount = 0;// Reset the total amount to zero
            lbltotal.Text = $"{MainID:C}";
            lbltxtTable.Visible = false;
            lbltxtWaiter.Visible = false;
            lbltotal.Visible = true;
            guna2DataGridView1.Rows.Clear();
        }
     
        private void btnHold_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            string currentTime = currentDate.ToString("HH:mm:ss");
            string tableName = lbltxtTable.Text;
            string waiterName = lbltxtWaiter.Text;
            double totalAmount;

            // Calculate the total amount and check if it's valid
            if (!CalculateTotalAmount(out totalAmount))
            {
                MessageBox.Show("One or more amounts are invalid. Please correct them before proceeding.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string orderType = OrderType;

            // Call InsertIntoMain with status parameter set to "Hold"
            int mainID = InsertIntoMain(currentDate, currentTime, tableName, waiterName, "Hold", orderType, totalAmount, userID);

            // Insert held products into tbldetails
            InsertIntoDetail(mainID);

            MessageBox.Show("Order held successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            guna2DataGridView1.Rows.Clear();

        }


        private int InsertIntoMain(DateTime currentDate, string currentTime, string tableName, string waiterName, string status, string orderType, double totalAmount, int userID)
        {
            int mainID = 0;
            string query = "INSERT INTO tblMain (aDate, aTime, TableName, WaiterName, Status, OrderType, Total, userID) " +
                           "VALUES (@aDate, @aTime, @TableName, @WaiterName, @Status, @OrderType, @Total, @UserID); " +
                           "SELECT LAST_INSERT_ID();"; // Get the last inserted ID

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.Parameters.AddWithValue("@aDate", currentDate);
                        cmd.Parameters.AddWithValue("@aTime", currentTime);
                        cmd.Parameters.AddWithValue("@TableName", tableName);
                        cmd.Parameters.AddWithValue("@WaiterName", waiterName);
                        cmd.Parameters.AddWithValue("@Status", status); // Pass the status parameter here
                        cmd.Parameters.AddWithValue("@OrderType", orderType);
                        cmd.Parameters.AddWithValue("@Total", totalAmount);
                        cmd.Parameters.AddWithValue("@UserID", userID); // Add userID parameter

                        mainID = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch (MySqlException ex)
                    {
                        // Handle database exception
                        MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return mainID;
        }


        private void BtnBillList_Click(object sender, EventArgs e)
        {
            frmBillList bl = new frmBillList();
            MainClass.BlurbackGround(bl);
        }



        private void BtnDineIn_Click(object sender, EventArgs e)
        {
            Table tbl = new Table();
            MainClass.BlurbackGround(tbl);
            if (tbl.TableName != "")
            {
                lbltxtTable.Text = tbl.TableName;
            }
            else
            {
                lbltxtTable.Text = "";
            }


            Waiter wtr = new Waiter();
            MainClass.BlurbackGround(wtr);
            if (wtr.WaiterName != "")
            {
                lbltxtWaiter.Text = wtr.WaiterName;
            }
            else
            {
                lbltxtWaiter.Text = "";
            }
            OrderType = "Dine In";
        }



        private void BtnAddOn_Click(object sender, EventArgs e)
        {


        }
        private void Btnkot_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No items in the order.");
                return;
            }

            // Get the current date and time
            DateTime currentDate = DateTime.Now;
            string currentTime = currentDate.ToString("HH:mm:ss");

            // Get table name and waiter name
            string tableName = lbltxtTable.Text;
            string waiterName = lbltxtWaiter.Text;
            double totalAmount;

            // Calculate the total amount and check if it's valid
            if (!CalculateTotalAmount(out totalAmount))
            {
                MessageBox.Show("One or more amounts are invalid. Please correct them before proceeding.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Set the status to pending
            string status = "Pending";

            // Use the previously set OrderType
            string orderType = OrderType;

            // Execute the query to insert into tblMain
            int mainID = InsertIntoMain(currentDate, currentTime, tableName, waiterName, status, orderType, totalAmount, userID);

            // Execute the query to insert into tblDetail for each item in the order
            InsertIntoDetail(mainID);

            // Optional: You can provide feedback to the user here if needed
            MessageBox.Show("Order placed successfully.");
            guna2DataGridView1.Rows.Clear();
            OrderType = "";

            // Optionally, you can also reset the total amount label
            lbltotal.Text = "$0.00";
        }
        private void InsertIntoDetail(int mainID)
        {
            string query = "INSERT INTO tbldetails (MainID, prodID, qty, price, amount) " +
                   "VALUES (@MainID, @ProductID, @Quantity, @Price, @Amount);";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                {
                    if (row.Cells["Name"].Value != null)
                    {
                        int productID = GetProductID(row.Cells["Name"].Value.ToString());
                        int quantity = Convert.ToInt32(row.Cells["Qty"].Value);
                        double price = Convert.ToDouble(row.Cells["Price"].Value);
                        double amount = quantity * price; // Calculate the amount

                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@MainID", mainID);
                            cmd.Parameters.AddWithValue("@ProductID", productID);
                            cmd.Parameters.AddWithValue("@Quantity", quantity);
                            cmd.Parameters.AddWithValue("@Price", price);
                            cmd.Parameters.AddWithValue("@Amount", amount); // Include the amount parameter
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        private int GetProductID(string productName)
        {
            int productID = 0;
            string query = "SELECT prodID FROM tbl_products WHERE prodName = @productName;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@productName", productName);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        productID = Convert.ToInt32(result);
                    }
                }
            }
            return productID;
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
