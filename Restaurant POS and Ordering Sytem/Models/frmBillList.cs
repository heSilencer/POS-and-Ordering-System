using MySql.Data.MySqlClient;
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

        public frmBillList()
        {
            InitializeComponent();
            InitializeDataGridView();
            LoadBillData();

        }
        private void InitializeDataGridView()
        {
            cat_datagridview.Columns.Add("MainID", "MainID"); // Hidden column for MainID
            cat_datagridview.Columns["MainID"].Visible = false; // Hide MainID column
            cat_datagridview.Columns.Add("Sr#", "Sr#"); // Column for row count
            cat_datagridview.Columns.Add("Table", "Table");
            cat_datagridview.Columns.Add("Waiter", "Waiter");
            cat_datagridview.Columns.Add("OrderType", "Order Type");
            cat_datagridview.Columns.Add("Status", "Status");
            cat_datagridview.Columns.Add("Total", "Total");

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
            cat_datagridview.Columns.Add(updateColumn);

            // Set the width of each column
            cat_datagridview.Columns["Sr#"].Width = 50;
            cat_datagridview.Columns["Table"].Width = 80;
            cat_datagridview.Columns["Waiter"].Width = 80;
            cat_datagridview.Columns["OrderType"].Width = 80;
            cat_datagridview.Columns["Status"].Width = 80;
            cat_datagridview.Columns["Total"].Width = 80;

            // Set any additional properties for the DataGridView as needed
            cat_datagridview.DefaultCellStyle.Font = new Font("Segoe UI", 13);
            // Set the height of the header
            cat_datagridview.ColumnHeadersHeight = 40;

            // Handle CellClick event for editing
            //cat_datagridview.CellClick += Guna2DataGridViewProducts_CellClick;

        }

        private void LoadBillData()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
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

                            cat_datagridview.Rows.Add(mainID, srNumber, table, waiter, orderType, status, total);
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

     
        private void frmBillList_Load(object sender, EventArgs e)
        {
          
        }
      
    }
}
