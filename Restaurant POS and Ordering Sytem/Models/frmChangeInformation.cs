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
        public delegate void InformationUpdatedEventHandler();
        public event InformationUpdatedEventHandler InformationUpdated;
        public frmChangeInformation(int userID)
        {
            InitializeComponent();
            this.userID = userID;
        }

        public void SetInitialValues(string fname, string lname, string address, string phone, string email, string username)
        {
            txtfname.Text = fname;
            txtLname.Text = lname;
            txtaddress.Text = address;
            txtPhone.Text = phone;
            txtemail.Text = email;
            txtusername.Text = username;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Validate phone number format
        private bool IsValidPhoneNumber(string phone)
        {
            return phone.Length == 11 && phone.All(char.IsDigit);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Retrieve new information from text fields
            string fname = txtfname.Text;
            string lname = txtLname.Text;
            string address = txtaddress.Text;
            string phone = txtPhone.Text;
            string email = txtemail.Text;
            string username = txtusername.Text;
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.");
                return;
            }

            // Validate phone number format
            if (!IsValidPhoneNumber(phone))
            {
                MessageBox.Show("Phone number must have 11 digits.");
                return;
            }
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlTransaction transaction = connection.BeginTransaction(); // Start a transaction

                    try
                    {
                        // Update staff information
                        string updateStaffQuery = @"
                    UPDATE tbl_staff
                    SET 
                        staffFname = @Fname,
                        staffLname = @Lname,
                        staffAddress = @Address,
                        staffPhone = @Phone,
                        staffEmail = @Email
                    WHERE 
                        staffID = (SELECT staffID FROM users WHERE userID = @UserID)";

                        using (MySqlCommand command = new MySqlCommand(updateStaffQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Fname", fname);
                            command.Parameters.AddWithValue("@Lname", lname);
                            command.Parameters.AddWithValue("@Address", address);
                            command.Parameters.AddWithValue("@Phone", phone);
                            command.Parameters.AddWithValue("@Email", email);
                            command.Parameters.AddWithValue("@UserID", userID);

                            command.ExecuteNonQuery(); // Execute staff update
                        }

                        // Update username in the users table
                        string updateUsersQuery = @"
                            UPDATE users
                            SET 
                                username = @Username
                            WHERE 
                                userID = @UserID";

                        using (MySqlCommand command = new MySqlCommand(updateUsersQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Username", username);
                            command.Parameters.AddWithValue("@UserID", userID);

                            command.ExecuteNonQuery(); // Execute username update
                        }

                        transaction.Commit(); // Commit the transaction if everything is successful

                        guna2MessageDialog1.Show();
                        this.Close();

                        InformationUpdated?.Invoke();

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // Roll back the transaction on error
                        MessageBox.Show("Error during update: " + ex.Message);
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
