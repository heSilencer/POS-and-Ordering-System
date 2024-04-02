using MySql.Data.MySqlClient;
using Restaurant_POS_and_Ordering_Sytem.ForgotPassword;
using Restaurant_POS_and_Ordering_Sytem.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_POS_and_Ordering_Sytem.Models
{
    public partial class frmSettings : Form
    {
        private string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";
        private int userID;

        public frmSettings(int userID)
        {
            InitializeComponent();
            this.userID = userID;
            PopulateUserProfile(userID);

        }

        // Method to populate user profile information
        private void PopulateUserProfile(int userID)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    // Retrieve staff information based on userID
                    string query = "SELECT staffFname, staffLname, staffAddress, staffPhone, staffEmail, staffImage FROM tbl_staff WHERE staffID = (SELECT staffID FROM users WHERE userID = @UserID)";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate UI controls
                                lblFname.Text = reader.GetString(0);
                                lblLname.Text = reader.GetString(1);
                                lbladdress.Text = reader.GetString(2);
                                lblPhone.Text = reader.GetString(3);
                                lblEmail.Text = reader.GetString(4);

                                // Assuming staffImage is stored as byte[] in the database
                                byte[] imageData = (byte[])reader.GetValue(5);
                                using (MemoryStream ms = new MemoryStream(imageData))
                                {
                                    // Set the image in the PictureBox
                                    ProfilePicture.Image = Image.FromStream(ms);
                                    // Ensure the image occupies the entire PictureBox area
                                    ProfilePicture.SizeMode = PictureBoxSizeMode.StretchImage;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Staff data not found for the given userID.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnChangepass_Click(object sender, EventArgs e)
        {

            //frmChangePassword fcp = new frmChangePassword();
            //fcp.Show();
            var fcp = new frmChangePassword(userID);
            MainClass.BlurbackGround(fcp);
        }

        private void ProfilePicture_Click(object sender, EventArgs e)
        {
            var imageViewer = new frmImageViewer(ProfilePicture.Image);
            MainClass.BlurbackGround(imageViewer);
        }

        private void btnUpdateInfo_Click(object sender, EventArgs e)
        {
            var frmChangeInformation = new frmChangeInformation(userID);
            frmChangeInformation.SetInitialValues(lblFname.Text, lblLname.Text, lbladdress.Text, lblPhone.Text, lblEmail.Text);
            frmChangeInformation.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}