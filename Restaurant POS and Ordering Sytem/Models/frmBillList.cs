using MySql.Data.MySqlClient;
using Restaurant_POS_and_Ordering_Sytem.RecieptPrint;
//using Restaurant_POS_and_Ordering_Sytem.Models;
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
    public partial class frmBillList : Form
    {
        string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";
        private int userID;
        private string username;
        public int MainID = 0;
        private List<OrderDetail> orderDetails;

        public frmBillList()
        {
            InitializeComponent();
            dgvBillList.Columns.Add("MainID", "MainID"); // Hidden column for MainID
            dgvBillList.Columns["MainID"].Visible = false; // Hide MainID column
            dgvBillList.Columns.Add("Sr#", "Sr#"); // Column for row count
            dgvBillList.Columns.Add("Table", "Table");
            dgvBillList.Columns.Add("Waiter", "Waiter");
            dgvBillList.Columns.Add("OrderType", "Order Type");
            dgvBillList.Columns.Add("Status", "Status");
            dgvBillList.Columns.Add("Total", "Total");

            // Add column for update icon
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
            dgvBillList.Columns.Add(updateColumn);

            DataGridViewImageColumn printColumn = new DataGridViewImageColumn();
            printColumn.Image = Properties.Resources.printIcon; // Replace with your actual update icon
            printColumn.Name = "Print";
            printColumn.HeaderText = ""; // Set the header text to an empty string
            printColumn.HeaderCell.Style.NullValue = "";
            printColumn.Width = 30;
            printColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            printColumn.FillWeight = 30;
            printColumn.MinimumWidth = 30;
            printColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dgvBillList.Columns.Add(printColumn);


         
            dgvBillList.Columns["Sr#"].Width = 50;
            dgvBillList.Columns["Table"].Width = 80;
            dgvBillList.Columns["Waiter"].Width = 80;
            dgvBillList.Columns["OrderType"].Width = 80;
            dgvBillList.Columns["Status"].Width = 80;
            dgvBillList.Columns["Total"].Width = 80;

            // Set any additional properties for the DataGridView as needed
            dgvBillList.DefaultCellStyle.Font = new Font("Segoe UI", 14);
            // Set the height of the header
            dgvBillList.ColumnHeadersHeight = 40;
            dgvBillList.RowTemplate.Height = 45;
            dgvBillList.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            dgvBillList.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Regular);

            // Handle CellClick event for editing
            dgvBillList.CellClick += Guna2DataGridViewProducts_CellClick;
            LoadBillData();
        }

       

        private void Guna2DataGridViewProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgvBillList.Columns[e.ColumnIndex].Name == "Update")
                {
                    MainID = Convert.ToInt32(dgvBillList.Rows[e.RowIndex].Cells["MainID"].Value);
                    string status = GetStatusFromMainID(MainID);

                    // Check if the status is not pending, Hold, or Check Out
                    if (status != "Pending" && status != "Check Out")
                    {
                        // Proceed with updating
                        this.Close(); // Close the frmBillList form
                                      // You can perform additional actions here if needed
                    }
                    else
                    {
                        // Display a message indicating that the action cannot be performed for the current status
                        guna2MessageDialog1.Show("Cannot proceed with update for orders with status: " + status + "");
                    }
                }
                else if (dgvBillList.Columns[e.ColumnIndex].Name == "Print")
                {
                    MainID = Convert.ToInt32(dgvBillList.Rows[e.RowIndex].Cells["MainID"].Value);
                    string status = GetStatusFromMainID(MainID);

                    // Check if the status is "Check Out" for printing
                    if (status == "Check Out")
                    {
                        List<OrderDetail> orderDetails = GetOrderDetails(MainID);
                        ReceiptPrint printForm = new ReceiptPrint();
                        printForm.DisplayOrderDetails(orderDetails);
                        printForm.ShowDialog();
                    }
                    else
                    {
                        // Display a message indicating that printing is not allowed for orders with other statuses
                        guna2MessageDialog1.Show("Printing is only allowed for orders with 'Check Out' status.");
                    }
                }
            }
        }   
        private string GetStatusFromMainID(int MainID)
        {
            string status = "";
            string query = "SELECT Status FROM tblMain WHERE MainID = @mainID";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@mainID", MainID);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        status = result.ToString();
                    }
                }
            }

            return status;
        }


        private void LoadBillData()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM tblMain";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        int srNumber = 1;

                        foreach (DataRow row in dt.Rows)
                        {
                            int mainID = Convert.ToInt32(row["MainID"]);
                            string table = row["TableName"].ToString();
                            string waiter = row["WaiterName"].ToString();
                            string orderType = row["OrderType"].ToString();
                            string status = row["Status"].ToString();
                            string total = row["Total"].ToString();

                            dgvBillList.Rows.Add(mainID, srNumber, table, waiter, orderType, status, total);
                            srNumber++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                OnErrorOccurred(ex.Message);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim().ToLower(); // Convert to lowercase and remove leading/trailing whitespace

            foreach (DataGridViewRow row in dgvBillList.Rows)
            {
                string tableValue = row.Cells["Table"].Value?.ToString().Trim().ToLower() ?? "";
                string waiterValue = row.Cells["Waiter"].Value?.ToString().Trim().ToLower() ?? "";
                string statusValue = row.Cells["Status"].Value?.ToString().Trim().ToLower() ?? "";
                string orderTypeValue = row.Cells["OrderType"].Value?.ToString().Trim().ToLower() ?? "";

                if (tableValue.Contains(searchText) || waiterValue.Contains(searchText) ||
                    statusValue.Contains(searchText) || orderTypeValue.Contains(searchText))
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
          
        }


        private List<OrderDetail> GetOrderDetails(int MainID)
        {
            orderDetails = new List<OrderDetail>();
            string query = @"SELECT
                        D.qty,
                        P.prodName AS ProductName,
                        D.prodID AS ProductID,
                        D.price AS Price, -- Added price column
                        D.amount AS Amount,
                        M.OrderType,
                        M.Total,
                        M.Received,
                        M.Change
                    FROM
                        tbldetails AS D
                    JOIN
                        tblMain AS M ON D.MainID = M.MainID
                    JOIN
                        tbl_products AS P ON D.prodID = P.prodID
                    WHERE
                        D.MainID = @MainID";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MainID", MainID);

                    try
                    {
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OrderDetail detail = new OrderDetail();
                                detail.Quantity = Convert.ToInt32(reader["qty"]);
                                detail.ProductID = Convert.ToInt32(reader["ProductID"]);
                                detail.ProductName = reader["ProductName"].ToString();
                                detail.Price = Convert.ToDouble(reader["Price"]); // Added price
                                detail.Amount = Convert.ToDouble(reader["Amount"]);
                                detail.OrderType = reader["OrderType"].ToString();
                                detail.Total = Convert.ToDouble(reader["Total"]);
                                detail.Received = Convert.ToDouble(reader["Received"]);
                                detail.Change = Convert.ToDouble(reader["Change"]);
                                orderDetails.Add(detail);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        OnErrorOccurred(ex.Message);
                    }
                }
            }

            return orderDetails;
        }

        // Event declaration
        public event EventHandler<string> ErrorOccurred;

        // Event raising method
        protected virtual void OnErrorOccurred(string errorMessage)
        {
            ErrorOccurred?.Invoke(this, errorMessage);
        }
    }
}
