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

namespace Restaurant_POS_and_Ordering_Sytem.View
{
    public partial class frmKitchenView : Form
    {
        string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";

        public frmKitchenView()
        {
            InitializeComponent();
        }

        private void frmKitchenView_Load(object sender, EventArgs e)
        {
            GetOrders();
        }
        private void GetOrders()
        {
            flowLayoutPanel1.Controls.Clear();
            string qry = "SELECT m.*, d.*, p.prodName FROM tblMain m JOIN tblDetails d ON m.MainID = d.MainID JOIN tbl_products p ON d.prodID = p.prodID WHERE m.status = 'Pending'";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand(qry, connection))
                {
                    DataTable dt1 = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt1);

                    // Group rows by MainID
                    var groupedRows = dt1.AsEnumerable().GroupBy(r => r.Field<int>("MainID"));

                    foreach (var group in groupedRows)
                    {
                        FlowLayoutPanel p1 = new FlowLayoutPanel();
                        p1.BackColor = Color.FromArgb(50, 55, 89); // Half color
                        p1.AutoSize = true;
                        p1.Width = 230;
                        p1.Height = 350;
                        p1.FlowDirection = FlowDirection.TopDown; // Set flow direction to TopDown
                        p1.BorderStyle = BorderStyle.FixedSingle;
                        p1.Margin = new Padding(10, 10, 10, 10);

                        Label lb1 = new Label();
                        lb1.ForeColor = Color.White;
                        lb1.Margin = new Padding(10, 10, 3, 0);
                        lb1.AutoSize = true;
                        lb1.Font = new Font("Arial", 14, FontStyle.Bold); // Set font size and style

                        Label lb2 = new Label();
                        lb2.ForeColor = Color.White;
                        lb2.Margin = new Padding(10, 10, 3, 0);
                        lb2.AutoSize = true;
                        lb2.Font = new Font("Arial", 14); // Set font size

                        Label lb3 = new Label();
                        lb3.ForeColor = Color.White;
                        lb3.Margin = new Padding(10, 10, 3, 0);
                        lb3.AutoSize = true;
                        lb3.Font = new Font("Arial", 14); // Set font size

                        Label lb4 = new Label();
                        lb4.ForeColor = Color.White;
                        lb4.Margin = new Padding(10, 10, 3, 0);
                        lb4.AutoSize = true;
                        lb4.Font = new Font("Arial", 14); // Set font size

                        lb1.Text = "TableName: " + group.First()["TableName"].ToString();
                        lb2.Text = "Waiter Name: " + group.First()["WaiterName"].ToString();
                        lb3.Text = "Order Time: " + group.First()["aTime"].ToString();
                        lb4.Text = "Order Type: " + group.First()["orderType"].ToString();

                        p1.Controls.Add(lb1);
                        p1.Controls.Add(lb2);
                        p1.Controls.Add(lb3);
                        p1.Controls.Add(lb4);

                        string productNames = "";

                        // Collect Product Names and Quantities for each MainID
                        foreach (DataRow row in group)
                        {
                            string productName = row["prodName"].ToString();
                            int qty = Convert.ToInt32(row["qty"]);
                            productNames += $"{productName} (Qty: {qty})\n";
                        }

                        // Create label for product names with white background
                        Label lbl5 = new Label();

                        lbl5.BackColor = Color.White; // White background
                        lbl5.Margin = new Padding(10, 5, 3, 0);
                        lbl5.AutoSize = true;
                        lbl5.Font = new Font("Arial", 14); // Set font size
                        lbl5.Text = productNames; // Set text to collected product names

                        p1.Controls.Add(lbl5);

                        FlowLayoutPanel p2 = new FlowLayoutPanel();
                        p2.AutoSize = true;
                        p2.Width = 230;
                        p2.Height = 50;
                        p2.FlowDirection = FlowDirection.TopDown; // Set flow direction to TopDown
                        p2.Margin = new Padding(0, 10, 0, 0);

                        Guna.UI2.WinForms.Guna2Button completeButton = new Guna.UI2.WinForms.Guna2Button();
                        completeButton.AutoRoundedCorners = true;
                        completeButton.Text = "Complete";
                        completeButton.Size = new Size(100, 35);
                        completeButton.FillColor = Color.Green;
                        completeButton.Margin = new Padding(60, 5, 3, 10); // Adjust the margin for centering
                        completeButton.Font = new Font("Arial", 10); // Set font size
                        completeButton.Tag = group.First()["MainID"]; // Assign MainID to Tag
                        completeButton.Click += CompleteButton_Click;

                        p2.Controls.Add(completeButton);
                        p1.Controls.Add(p2);

                        flowLayoutPanel1.Controls.Add(p1);
                    }
                }
            }
        }

        private void CompleteButton_Click(object sender, EventArgs e)
        {
            if (sender is Guna.UI2.WinForms.Guna2Button)
            {
                Guna.UI2.WinForms.Guna2Button completeButton = (Guna.UI2.WinForms.Guna2Button)sender;
                int orderId = Convert.ToInt32(completeButton.Tag);
                UpdateOrderStatus(orderId);
            }
        }

        private void UpdateOrderStatus(int orderId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string updateQuery = "UPDATE tblMain SET status = 'Complete' WHERE MainID = @orderId";
                using (MySqlCommand cmd = new MySqlCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    cmd.ExecuteNonQuery();
                }

                // Remove completed order from UI
                var controlsToRemove = flowLayoutPanel1.Controls.OfType<FlowLayoutPanel>()
                    .Where(panel => panel.Controls.OfType<Guna.UI2.WinForms.Guna2Button>()
                    .Any(button => Convert.ToInt32(button.Tag) == orderId))
                    .ToList();

                foreach (var controlToRemove in controlsToRemove)
                {
                    flowLayoutPanel1.Controls.Remove(controlToRemove);
                }
                GetOrders();
            }
        }
    }
}