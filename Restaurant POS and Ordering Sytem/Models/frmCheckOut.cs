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
    public partial class frmCheckOut : Form
    {
        string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";
        private double totalAmount;
        private List<OrderDetail> orderDetails;
        private int mainID;
        private double receivedAmount;
        public frmCheckOut(double totalAmount, List<OrderDetail> orderDetails, int mainID)
        {
            InitializeComponent();
            this.totalAmount = totalAmount;
            this.orderDetails = orderDetails;
            this.mainID = mainID;
        }
        
        
        private void txtBillAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(txtpRecieve.Text, out receivedAmount))
            {
                MessageBox.Show("Invalid input for received amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Calculate the change
            double change = receivedAmount - totalAmount;

            // Display the change in the txtChange textbox
            txtChange.Text = change.ToString();

            // Update tblMain table in the database with the received amount and change
            UpdateMainTable(receivedAmount, change);

            // Optionally, perform any additional actions after checkout
            MessageBox.Show("Checkout successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Close the frmCheckOut form with OK result
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void UpdateMainTable(double receivedAmount, double change)
        {
            string query = "UPDATE tblMain SET Received = @receivedAmount, Change = @change WHERE MainID = @mainID;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@receivedAmount", receivedAmount);
                    cmd.Parameters.AddWithValue("@change", change);
                    cmd.Parameters.AddWithValue("@mainID", mainID);

                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error updating tblMain: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void frmCheckOut_Load(object sender, EventArgs e)
        {
            txtBillAmount.Text = totalAmount.ToString();
        }
    }
}
public class OrderDetail
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public double Amount { get; set; }
}