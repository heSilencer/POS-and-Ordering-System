﻿using MySql.Data.MySqlClient;
using Restaurant_POS_and_Ordering_Sytem.Models;
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
        private int MainID;
        private double receivedAmount;
        private List<OrderDetail> orderDetails;
        public frmCheckOut(double totalAmount, List<OrderDetail> orderDetails, int mainID)
        {
            InitializeComponent();
            this.totalAmount = totalAmount;
            this.totalAmount = totalAmount;
            this.orderDetails = orderDetails;
            this.MainID = mainID;
            txtBillAmount.Text = totalAmount.ToString();
            txtChange.ReadOnly = true;
            txtBillAmount.ReadOnly = true;
        }

       
        private void txtBillAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtpRecieve.Text) || !double.TryParse(txtpRecieve.Text, out receivedAmount))
            {
                guna2MessageDialog2.Show("Please enter a valid received amount.");
                return;
            }

            // Calculate the change
            double change = receivedAmount - totalAmount;

            // Check if the change is negative
            if (change < 0)
            {
                guna2MessageDialog2.Show("Cannot check out with a negative change.");
                return;
            }

            // Display the change in the txtChange textbox
            txtChange.Text = change.ToString("F2"); // Format the change to display with two decimal places

            // Update tblMain table in the database with the received amount and change
            UpdateMainTable(receivedAmount, change);

            // Optionally, perform any additional actions after checkout
            guna2MessageDialog1.Show("Checkout successful.");

            

            // Open the frmPrint form after setting DialogResult
            this.Close();
        }
        private void UpdateMainTable(double receivedAmount, double change)
        {
            string query = "UPDATE tblMain SET Received = @receivedAmount, `Change` = @change, Status = 'Check Out', Table_Status = 'Ready' WHERE MainID = @mainID;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@receivedAmount", receivedAmount);
                    cmd.Parameters.AddWithValue("@change", change);
                    cmd.Parameters.AddWithValue("@mainID", MainID);

                    try
                    {
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        guna2MessageDialog2.Show($"Error updating tblMain: {ex.Message}");
                    }
                }
            }
        }
        private void frmCheckOut_Load(object sender, EventArgs e)
        {
            txtBillAmount.Text = totalAmount.ToString();
            UpdateChange();
        }

        private void txtpRecieve_TextChanged(object sender, EventArgs e)
        {
            UpdateChange();
        }
        private void UpdateChange()
        {
            if (double.TryParse(txtpRecieve.Text, out receivedAmount))
            {
                // Calculate the change
                double change = receivedAmount - totalAmount;

                // Display the change in the txtChange textbox
                txtChange.Text = change.ToString("F2"); // Format the change to display with two decimal places
            }
            else
            {
                // Clear the change textbox if the entered value is not a valid number
                txtChange.Clear();
            }
         
        }

        private void txtChange_TextChanged(object sender, EventArgs e)
        {
           
        }
     
    }
}
public class OrderDetail
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public double Amount { get; set; }
    public int ProductID { get; internal set; }
    public string OrderType { get; internal set; }
    public double Total { get; internal set; }
    public double Received { get; internal set; }
    public double Change { get; internal set; }
}