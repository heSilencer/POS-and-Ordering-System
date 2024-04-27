using MySql.Data.MySqlClient;
using Restaurant_POS_and_Ordering_Sytem.RecieptPrint;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_POS_and_Ordering_Sytem.View
{
    public partial class frmBillListView : Form
    {
        string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";
        private int userID;
        private string username;
        public int MainID = 0;
        private List<OrderDetail> orderDetails;
        public frmBillListView()
        {
            InitializeComponent();
            dgvBillList.Columns.Add("MainID", "MainID"); // Hidden column for MainID
            dgvBillList.Columns["MainID"].Visible = false; // Hide MainID column
            dgvBillList.Columns.Add("Sr#", "Sr#");
            dgvBillList.Columns.Add("Date", "Date");
            // Column for row count
            dgvBillList.Columns.Add("Table", "Table");
            dgvBillList.Columns.Add("Waiter", "Waiter");
            dgvBillList.Columns.Add("OrderType", "Order Type");
            dgvBillList.Columns.Add("Status", "Status");
            dgvBillList.Columns.Add("Total", "Total");
            DataGridViewImageColumn printColumn = new DataGridViewImageColumn();
            printColumn.Image = Properties.Resources.printIcon; // Replace with your actual print icon
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
            dgvBillList.Columns["Date"].Width = 80;

            dgvBillList.Columns["Table"].Width = 80;
            dgvBillList.Columns["Waiter"].Width = 80;
            dgvBillList.Columns["OrderType"].Width = 80;
            dgvBillList.Columns["Status"].Width = 80;
            dgvBillList.Columns["Total"].Width = 80;

            // Set any additional properties for the DataGridView as needed
            dgvBillList.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 14);
            // Set the height of the header
            dgvBillList.ColumnHeadersHeight = 40;
            dgvBillList.RowTemplate.Height = 45;
            dgvBillList.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.Gray;
            dgvBillList.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Regular);
            dgvBillList.CellClick += Guna2DataGridViewProducts_CellClick;

            LoadBillData();
        }
        private void LoadBillData()
        {
            try
            {
                string query = "SELECT * FROM tblMain WHERE status = 'Check Out' AND 1 = 1";
                // Select all data from tblMain
                if (radiobuttonHold.Checked)
                {
                    query += " AND Status = 'Hold'";
                }
                else if (radiobuttonDineIn.Checked)
                {
                    query += " AND OrderType = 'Dine In'";
                }
                else if (radiobuttonTakeOut.Checked)
                {
                    query += " AND OrderType = 'Take Out'";
                }
                else if (RadioButtonCheckOut.Checked)
                {
                    query += " AND Status = 'Check Out'";
                }
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            dgvBillList.Rows.Clear(); // Clear existing rows before loading new data

                            int srNumber = 1;

                            foreach (DataRow row in dt.Rows)
                            {
                                int mainID = Convert.ToInt32(row["MainID"]);
                                string table = row["TableName"].ToString();
                                string waiter = row["WaiterName"].ToString();
                                string orderType = row["OrderType"].ToString();
                                string status = row["Status"].ToString();
                                string total = row["Total"].ToString();
                                DateTime date = Convert.ToDateTime(row["aDate"]);

                                // Format the date to display month in letters (e.g., "January 23, 2024")
                                string formattedDate = date.ToString("MMMM dd, yyyy"); // "MMMM" represents full month name

                                dgvBillList.Rows.Add(mainID, srNumber, formattedDate, table, waiter, orderType, status, total);
                                srNumber++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Guna2DataGridViewProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvBillList.Columns[e.ColumnIndex].Name == "Print")
            {
                int mainID = Convert.ToInt32(dgvBillList.Rows[e.RowIndex].Cells["MainID"].Value);
                string status = GetStatusFromMainID(mainID);

                // Check if the status is "Check Out" for printing
                if (status == "Check Out")
                {
                    // Proceed with printing
                    List<OrderDetail> orderDetails = GetOrderDetails(mainID);
                    ReceiptPrint printForm = new ReceiptPrint();
                    printForm.DisplayOrderDetails(orderDetails);
                    printForm.ShowDialog();
                }
                else
                {
                    // Display a message indicating that printing is not allowed for orders with other statuses
                    MessageBox.Show("Printing is only allowed for orders with 'Check Out' status.", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        public event EventHandler<string> ErrorOccurred;

        // Event raising method
        protected virtual void OnErrorOccurred(string errorMessage)
        {
            ErrorOccurred?.Invoke(this, errorMessage);
        }

        private void radiobuttonDineIn_CheckedChanged(object sender, EventArgs e)
        {
            LoadBillData();

        }

        private void radiobuttonTakeOut_CheckedChanged(object sender, EventArgs e)
        {
            LoadBillData();

        }

        private void RadioButtonCheckOut_CheckedChanged(object sender, EventArgs e)
        {
            LoadBillData();

        }

        private void radiobuttonHold_CheckedChanged(object sender, EventArgs e)
        {
            LoadBillData();

        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            radiobuttonHold.Checked = false;
            radiobuttonDineIn.Checked = false;
            radiobuttonTakeOut.Checked = false;
            RadioButtonCheckOut.Checked = false;
            LoadBillData();
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
                string dateValue = row.Cells["Date"].Value?.ToString().Trim().ToLower() ?? "";

                // Check if any of the fields contain the search text or the date matches the search text
                if (tableValue.Contains(searchText) || waiterValue.Contains(searchText) ||
                    statusValue.Contains(searchText) || orderTypeValue.Contains(searchText) ||
                    dateValue.Contains(searchText))
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
            }

        }
    }
}
