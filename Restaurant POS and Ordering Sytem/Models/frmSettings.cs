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
        private string username;
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
                    // Query to get username and staff information
                    string query = @"
                SELECT 
                    u.username,
                    s.staffFname,
                    s.staffLname,
                    s.staffAddress,
                    s.staffPhone,
                    s.staffEmail,
                    s.staffImage
                FROM 
                    users AS u
                JOIN 
                    tbl_staff AS s
                ON 
                    u.staffID = s.staffID
                WHERE 
                    u.userID = @UserID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Save the username to a class-level variable
                                username = reader.GetString(0);

                                // Populate other controls
                                lblFname.Text = reader.GetString(1);
                                lblLname.Text = reader.GetString(2);
                                lbladdress.Text = reader.GetString(3);
                                lblPhone.Text = reader.GetString(4);
                                lblEmail.Text = reader.GetString(5);

                                byte[] imageData = (byte[])reader.GetValue(6);
                                using (MemoryStream ms = new MemoryStream(imageData))
                                {
                                    ProfilePicture.Image = Image.FromStream(ms);
                                    ProfilePicture.SizeMode = PictureBoxSizeMode.StretchImage;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Data not found for the given userID.");
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
        private void FrmChangeInformation_InformationUpdated()
        {
            // Refresh user profile information
            PopulateUserProfile(userID);
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
            frmChangeInformation.SetInitialValues(lblFname.Text, lblLname.Text, lbladdress.Text, lblPhone.Text, lblEmail.Text, username);
            frmChangeInformation.InformationUpdated += FrmChangeInformation_InformationUpdated; // Subscribe to the event after the form is displayed
            MainClass.BlurbackGround(frmChangeInformation);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}