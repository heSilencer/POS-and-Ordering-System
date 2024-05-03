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
            string fname = txtfname.Text.Trim();
            string lname = txtLname.Text.Trim();
            string address = txtaddress.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string email = txtemail.Text.Trim();
            string username = txtusername.Text.Trim();
            if (string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(username))
            {
                guna2MessageDialog1.Show("Please fill in all required fields.");
                return;
            }
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

                        // Update username in the users table if the fname has changed
                        string updateUsersQuery = @"
                    UPDATE users
                    SET 
                        uname = @Username
                    WHERE 
                        userID = @UserID";

                        using (MySqlCommand command = new MySqlCommand(updateUsersQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Username", fname);
                            command.Parameters.AddWithValue("@UserID", userID);

                            command.ExecuteNonQuery(); // Execute username update
                        }

                        transaction.Commit(); // Commit the transaction if everything is successful

                        guna2MessageDialog1.Show("Information updated successfully!");
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

        private void txtusername_TextChanged(object sender, EventArgs e)
        {
            if (txtusername == null || string.IsNullOrEmpty(txtusername.Text))
            {
                return;
            }

            // Capitalize the first character of the text
            txtusername.Text = char.ToUpper(txtusername.Text[0]) + txtusername.Text.Substring(1);

            // Set the caret position to the end of the text
            txtusername.SelectionStart = txtusername.Text.Length;
        
        }

        private void txtfname_TextChanged(object sender, EventArgs e)
        {
            if (txtfname == null || string.IsNullOrEmpty(txtfname.Text))
            {
                return;
            }

            // Capitalize the first character of the text
            txtfname.Text = char.ToUpper(txtfname.Text[0]) + txtfname.Text.Substring(1);

            // Set the caret position to the end of the text
            txtfname.SelectionStart = txtfname.Text.Length;

        }

        private void txtLname_TextChanged(object sender, EventArgs e)
        {
            if (txtLname == null || string.IsNullOrEmpty(txtLname.Text))
            {
                return;
            }

            // Capitalize the first character of the text
            txtLname.Text = char.ToUpper(txtLname.Text[0]) + txtLname.Text.Substring(1);

            // Set the caret position to the end of the text
            txtLname.SelectionStart = txtLname.Text.Length;

        }

        private void txtaddress_TextChanged(object sender, EventArgs e)
        {
            if (txtaddress == null || string.IsNullOrEmpty(txtaddress.Text))
            {
                return;
            }

            // Capitalize the first character of the text
            txtaddress.Text = char.ToUpper(txtaddress.Text[0]) + txtaddress.Text.Substring(1);

            // Set the caret position to the end of the text
            txtaddress.SelectionStart = txtaddress.Text.Length;

        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
         
        }

        private void txtemail_TextChanged(object sender, EventArgs e)
        {
            if (txtemail == null || string.IsNullOrEmpty(txtemail.Text))
            {
                return;
            }

            // Capitalize the first character of the text
            txtemail.Text = char.ToUpper(txtemail.Text[0]) + txtemail.Text.Substring(1);

            // Set the caret position to the end of the text
            txtemail.SelectionStart = txtemail.Text.Length;

        }
    }
}
