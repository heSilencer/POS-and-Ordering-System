using MySql.Data.MySqlClient;
using Restaurant_POS_and_Ordering_Sytem.View;
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
        private int mainID;
        private double totalAmount;

        public frmPos(string username, int userID, int mainID)
        {
            InitializeComponent();
            this.username = username;
            this.userID = userID;
            this.mainID = mainID;
            InitializeDataGridView();

        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void InitializeDataGridView()
        {
            // Add columns to the DataGridView
            guna2DataGridView1.Columns.Add("Sr#", "Sr#");
            guna2DataGridView1.Columns.Add("Name", "Name");
            guna2DataGridView1.Columns.Add("Qty", "Qty");
            guna2DataGridView1.Columns.Add("Price", "Price");
            guna2DataGridView1.Columns.Add("Amount", "Amount");

            // Adjust the font size
            guna2DataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 13);

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
        


        public string OrderType;
        private int MainID;


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
                Subform subForm = new Subform(username, userID, mainID); // Pass the UserID to Subform
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
            btnCheckOut.Visible = false;

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
                qry = "SELECT * FROM tbl_products WHERE Prodcategory != 'N/A'";
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
                        guna2MessageDialog2.Show($"Invalid amount format: {amountString}");
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
            OrderType = "Take Out";
        }

        private void btnAddnew_Click(object sender, EventArgs e)
        {
            btntk1.Visible = false;
            btndineIn1.Visible = false;
            btnhold1.Visible = false;
            btnhold2.Visible = false;
            btnCheckOut.Visible = false;
            BtnDineIn.Visible = true;
            btnHold.Visible = true;
            Btnkot.Visible = true;
            BtnTakeAway.Visible = true;
            lbltxtTable.Text = "";
            lbltxtWaiter.Text = "";
            OrderType = "";
            double totalAmount = 0;// Reset the total amount to zero
            lbltotal.Text = $"{totalAmount:C}";
            lbltxtTable.Visible = false;
            lbltxtWaiter.Visible = false;
            lbltotal.Visible = true;
            guna2DataGridView1.Rows.Clear();
        }

        private void btnHold_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count == 0)
            {
                guna2MessageDialog1.Show("No items in the order.");
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
                guna2MessageDialog2.Show("One or more amounts are invalid. Please correct them before proceeding.");
                return;
            }

            // Set the status to "Hold"
            string status = "Hold";

            // Get the selected order type
            string orderType = OrderType;

            // Validate if an order type is selected
            if (string.IsNullOrEmpty(orderType))
            {
                guna2MessageDialog2.Show("Please select an order type.");
                return;
            }

            // Execute the query to insert into tblMain for the new order
            int newMainID = InsertIntoMain(currentDate, currentTime, tableName, waiterName, status, orderType, totalAmount, userID);

            // Execute the query to insert into tblDetail for each item in the order
            InsertIntoDetail(newMainID);

            // Optional: Provide feedback to the user
            guna2MessageDialog1.Show("Order hold successfully.");
            guna2DataGridView1.Rows.Clear();
            OrderType = "";

            // Optionally, reset the total amount label
            lbltotal.Text = $"{0:C}";
            lbltxtTable.Text = "";
            lbltxtWaiter.Text = "";



        }


        private int InsertIntoMain(DateTime currentDate, string currentTime, string tableName, string waiterName, string status, string orderType, double totalAmount, int userID)
        {
            int mainID = 0;
            string tableStatus = "Not Ready";
            string query = "INSERT INTO tblMain (aDate, aTime, TableName, WaiterName, Status, OrderType, Total, userID, Table_Status) " +
                           "VALUES (@aDate, @aTime, @TableName, @WaiterName, @Status, @OrderType, @Total, @UserID, @TableStatus); " +
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
                        cmd.Parameters.AddWithValue("@TableStatus", tableStatus); // Add Table_Status parameter

                        mainID = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    catch (MySqlException ex)
                    {
                        // Handle database exception
                        guna2MessageDialog2.Show($"Database error: {ex.Message}");
                    }
                }
            }
            return mainID;
        }

        private void BtnBillList_Click(object sender, EventArgs e)
        {
            frmBillList bl = new frmBillList();
            bl.FormClosed += (s, ev) =>
            {
                if (bl.MainID != 0)
                {
                    LoadEntries(bl.MainID);
                  
                }
            };
            MainClass.BlurbackGround(bl);
           
        }
        public void LoadEntries(int mainID)
        {
            string query = @"SELECT d.prodID, p.prodName, d.qty, d.price, d.amount, m.tableName, m.waiterName, m.Status, m.OrderType
                     FROM tbldetails d 
                     INNER JOIN tbl_products p ON d.prodID = p.prodID 
                     INNER JOIN tblMain m ON d.MainID = m.MainID 
                     WHERE d.MainID = @mainID
                     AND m.Status IN ('Hold', 'Complete');";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@mainID", mainID);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string status = reader.GetString("Status");

                                string tableName = reader.GetString("tableName");
                                string waiterName = reader.GetString("waiterName");

                                // Display table name, waiter name, and order type in labels
                                lbltxtTable.Text = tableName;
                                lbltxtWaiter.Text = waiterName;

                                // Clear DataGridView before adding new entries
                                guna2DataGridView1.Rows.Clear();

                                // Read remaining rows and add data to the DataGridView
                                do
                                {
                                    int productID = reader.GetInt32("prodID");
                                    string productName = reader.GetString("prodName");
                                    int quantity = reader.GetInt32("qty");
                                    double price = reader.GetDouble("price");
                                    double amount = reader.GetDouble("amount");

                                    // Add the data to the DataGridView
                                    int rowIndex = guna2DataGridView1.Rows.Add();
                                    guna2DataGridView1.Rows[rowIndex].Cells["Sr#"].Value = (rowIndex + 1).ToString();
                                    guna2DataGridView1.Rows[rowIndex].Cells["Name"].Value = productName;
                                    guna2DataGridView1.Rows[rowIndex].Cells["Qty"].Value = quantity.ToString();
                                    guna2DataGridView1.Rows[rowIndex].Cells["Price"].Value = price.ToString();
                                    guna2DataGridView1.Rows[rowIndex].Cells["Amount"].Value = amount.ToString();
                                } while (reader.Read());

                                // Hide the delete icon column
                                guna2DataGridView1.Columns["Delete"].Visible = false;

                                // Clear buttons if status is Hold
                                if (status == "Hold")
                                {
                                    BtnDineIn.Visible = false;
                                    btnHold.Visible = false;
                                    Btnkot.Visible = false;
                                    btnHoldKot.Visible = true;
                                    btnKot2.Visible = false;

                                    btnhold1.Visible = true;
                                    btnhold2.Visible = false;
                                    BtnTakeAway.Visible = false;
                                    btnCheckOut.Visible = false;

                                    btntk1.Visible = true;
                                    btndineIn1.Visible = true;
                                    UpdateStatusToPending(mainID);
                                }
                                if (status == "Complete")
                                {
                                    BtnDineIn.Visible = false;
                                    btnHold.Visible = false;
                                    Btnkot.Visible = false;
                                    BtnTakeAway.Visible = false;
                                    btnCheckOut.Visible = true;
                                    btnCheckOut.Visible = true;
                                    btnKot2.Visible = true;

                                    btnHoldKot.Visible = false;
                                    btntk1.Visible = true;
                                    btndineIn1.Visible = true;
                                    btnhold1.Visible = false;
                                    btnhold2.Visible = true;
                                    // Delete entries if status is Hold
                                }

                                MainID = mainID;
                                UpdateTotalAmount();
                            }
                        }
                        else
                        {
                            // If no data found, clear labels and DataGridView
                            lbltxtTable.Text = "";
                            lbltxtWaiter.Text = "";
                            guna2DataGridView1.Rows.Clear();
                            double totalAmount1 = 0; // Reset the total amount to zero
                            lbltotal.Text = $"{totalAmount1:C}";
                        }
                    }
                }
            }
        }

        private void UpdateStatusToPending(int mainID)
        {
            string updateQuery = "UPDATE tblMain SET Status = 'Pending' WHERE MainID = @mainID AND Status = 'Hold'";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(updateQuery, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.Parameters.AddWithValue("@mainID", mainID);
                        cmd.ExecuteNonQuery();
                    }
                    catch (MySqlException ex)
                    {
                        // Handle database exception
                        guna2MessageDialog2.Show($"Database error: {ex.Message}");
                    }
                }
            }
        }




        private void BtnDineIn_Click(object sender, EventArgs e)
        {
            var tbl = new Table();
            MainClass.BlurbackGround(tbl);
            if (tbl.TableName != "")
            {
                lbltxtTable.Text = tbl.TableName;
            }
            else
            {
                lbltxtTable.Text = "";
            }


            var wtr = new Waiter();
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



  
        private void Btnkot_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count == 0)
            {
                guna2MessageDialog1.Show("No items in the order.");
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
                guna2MessageDialog2.Show("One or more amounts are invalid. Please correct them before proceeding.");
                return;
            }

            // Set the status to "Pending"
            string status = "Pending";

            // Use the previously set OrderType
            string orderType = OrderType;
            if (string.IsNullOrEmpty(orderType))
            {
                guna2MessageDialog2.Show("Please select an order type.");
                return;
            }


            // Execute the query to insert into tblMain for the new order
            int newMainID = InsertIntoMain(currentDate, currentTime, tableName, waiterName, status, orderType, totalAmount, userID);

            // Execute the query to insert into tblDetail for each item in the order
            InsertIntoDetail(newMainID);

            // Optional: Provide feedback to the user
            guna2MessageDialog1.Show("Order placed successfully.");
            guna2DataGridView1.Rows.Clear();
            OrderType = "";

            // Optionally, reset the total amount label
            lbltotal.Text = $"{0:C}";
            lbltxtTable.Text = "";
            lbltxtWaiter.Text = "";
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

        private double CalculateTotalAmount()
        {
            double totalAmount = 0;
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    double amount = Convert.ToDouble(row.Cells["Amount"].Value);
                    totalAmount += amount;
                }
            }
            return totalAmount;
        }
        private List<OrderDetail> GetOrderDetails()
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();

            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                OrderDetail detail = new OrderDetail
                {
                    ProductName = row.Cells["Name"].Value.ToString(),
                    Quantity = Convert.ToInt32(row.Cells["Qty"].Value),
                    Price = Convert.ToDouble(row.Cells["Price"].Value),
                    Amount = Convert.ToDouble(row.Cells["Amount"].Value)
                };
                orderDetails.Add(detail);
            }

            return orderDetails;
        }

       

        private void btnCheckOut_Click_1(object sender, EventArgs e)
        {
            double totalAmount = CalculateTotalAmount();
            List<OrderDetail> orderDetails = GetOrderDetails();

            // Load entries and retrieve the MainID
            LoadEntries(MainID);
            guna2DataGridView1.Rows.Clear();
            double totalAmount1 = 0;// Reset the total amount to zero
            lbltotal.Text = $"{totalAmount1:C}";
            lbltxtTable.Text = "";
            lbltxtWaiter.Text = "";
            
            btnCheckOut.Visible = true;
            btnHoldKot.Visible = true;
            BtnDineIn.Visible = true;
            btnHold.Visible = true;
            Btnkot.Visible = true;
            BtnTakeAway.Visible = true;

            btnCheckOut.Visible = false;
            btntk1.Visible = false;
            btndineIn1.Visible = false;
            btnhold1.Visible = false;
            btnhold2.Visible = false;
            btnKot2.Visible = false;
            btnHoldKot.Visible = false;
            // Open the checkout form and pass all required parameters
            frmCheckOut checkoutForm = new frmCheckOut(totalAmount, orderDetails, MainID);
            MainClass.BlurbackGround(checkoutForm);
        }

        private void btnHoldKot_Click(object sender, EventArgs e)
        {
            btntk1.Visible = false;
            btndineIn1.Visible = false;
            btnhold1.Visible = false;
            btnhold2.Visible = false;
            btnCheckOut.Visible = false;
            BtnDineIn.Visible = true;
            btnHold.Visible = true;
            Btnkot.Visible = true;
            BtnTakeAway.Visible = true;
            lbltxtTable.Text = "";
            lbltxtWaiter.Text = "";
            OrderType = "";
            double totalAmount = 0;// Reset the total amount to zero
            lbltotal.Text = $"{totalAmount:C}";
            lbltxtTable.Visible = false;
            lbltxtWaiter.Visible = false;
            lbltotal.Visible = true;
            guna2DataGridView1.Rows.Clear();

            guna2MessageDialog1.Show("Orde Placed SuccessFully");
        }

        private void btnKot2_Click(object sender, EventArgs e)
        {
            guna2MessageDialog2.Show("Can not Make  Kitchen order ticket with a status Complete");
            btnHoldKot.Visible = false;

        }

        private void btnhold1_Click(object sender, EventArgs e)
        {
            guna2MessageDialog2.Show("The Status is Hold Already ");


        }

        private void btnhold2_Click(object sender, EventArgs e)
        {
            guna2MessageDialog2.Show("Can not Make it Hold with a status Complete ");


        }

        private void btntk1_Click(object sender, EventArgs e)
        {
            guna2MessageDialog2.Show("Cannot Choose Order Type");

        }

        private void btndineIn1_Click(object sender, EventArgs e)
        {
            guna2MessageDialog2.Show("Cannot Choose Order Type");

        }
    }
}

