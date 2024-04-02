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
    public partial class frmChangeInformation : Form
    {
        private string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";
        private int userID;

        public frmChangeInformation(int userID)
        {
            InitializeComponent();
            this.userID = userID;
        }

        public void SetInitialValues(string fname, string lname, string address, string phone, string email)
        {
            txtfname.Text = fname;
            txtLname.Text = lname;
            txtaddress.Text = address;
            txtPhone.Text = phone;
            txtemail.Text = email;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string fname = txtfname.Text;
            string lname = txtLname.Text;
            string address = txtaddress.Text;
            string phone = txtPhone.Text;
            string email = txtemail.Text;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE tbl_staff SET staffFname = @Fname, staffLname = @Lname, staffAddress = @Address, staffPhone = @Phone, staffEmail = @Email WHERE staffID = (SELECT staffID FROM users WHERE userID = @UserID)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Fname", fname);
                        command.Parameters.AddWithValue("@Lname", lname);
                        command.Parameters.AddWithValue("@Address", address);
                        command.Parameters.AddWithValue("@Phone", phone);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@UserID", userID);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Information updated successfully.");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update information.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
 

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
