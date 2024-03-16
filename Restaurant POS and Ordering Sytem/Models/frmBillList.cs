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
        public frmBillList()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        private void frmBillList_Load(object sender, EventArgs e)
        {

        }
        private void InitializeDataGridView()
        {
            // Create columns for Guna2DataGridView
            guna2DataGridView1.Columns.Add("Sr#", "Sr#");
            guna2DataGridView1.Columns.Add("Table", "Table");
            guna2DataGridView1.Columns.Add("Waiter", "Waiter");
            guna2DataGridView1.Columns.Add("OrderType", "Order Type");
            guna2DataGridView1.Columns.Add("Status", "Status");
            guna2DataGridView1.Columns.Add("Total", "Total");

            // Set the width of each column
            guna2DataGridView1.Columns["Sr#"].Width = 50;
            guna2DataGridView1.Columns["Table"].Width = 80;
            guna2DataGridView1.Columns["Waiter"].Width = 80;
            guna2DataGridView1.Columns["OrderType"].Width = 80;
            guna2DataGridView1.Columns["Status"].Width = 80;
            guna2DataGridView1.Columns["Total"].Width = 80;

            // Set any additional properties for the DataGridView as needed
            guna2DataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 13);

            // Set the height of the header
            guna2DataGridView1.ColumnHeadersHeight = 40; // Adjust the height as needed

            // Set the font size for header and rows

            // Set any additional properties for the DataGridView as needed
            guna2DataGridView1.AllowUserToAddRows = false; // To prevent the extra row at the end
            guna2DataGridView1.ReadOnly = true; // // To make the DataGridView read-only
            // Set other properties or event handlers if necessary

            // Optionally, you can set the DataGridView data source here or in frmBillList_Load
            // guna2DataGridView1.DataSource = YourDataSource;
        }
    }
}
