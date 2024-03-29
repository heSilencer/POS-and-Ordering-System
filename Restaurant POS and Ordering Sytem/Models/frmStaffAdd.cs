﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing.Imaging;

namespace Restaurant_POS_and_Ordering_Sytem.Models
{
    public partial class frmStaffAdd : Form
    {
        string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";
        private int staffId;
        private int id;
    

        public event EventHandler StaffUpdated;
        public frmStaffAdd()
        {
            InitializeComponent();
            LoadCategoryNames();
        }
        public void SetStaffInfo(int id, string fname, string lname, string address, string phone, string email, string staffCategory, byte[] imageData)
        {
            staffId = id;
            lblfname.Text = fname;
            lblLname.Text = lname;
            lblAddress.Text = address;
            lblPhone.Text = phone;
            lblEmail.Text = email;
            cmbxcat.SelectedItem = staffCategory;

            // Change the button text to "Update" if staffId is greater than 0 (indicating an update)
            btnSave.Text = staffId > 0 ? "Update" : "Save";

            // Display the staff image if imageData is not null
            if (imageData != null && imageData.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    StaffImage.Image = Image.FromStream(ms);
                    // Set the original image when the image is initially loaded
                    SetOriginalImage(StaffImage.Image);
                }
            }
            else
            {
                // If imageData is null or empty, set the PictureBox to display a default image or leave it blank
                // For example:
                // picBoxStaffImage.Image = Properties.Resources.DefaultStaffImage; // Set to a default image
                // picBoxStaffImage.Image = null; // Set to null to clear the image
                // Ensure to set the original image to null as well if no image is loaded
                originalImage = null;
            }
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool IsValidEmail(string email)
        {
            // Simple email validation using regular expression
            // This pattern checks for basic email format but may not cover all cases
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        private bool IsValidPhoneNumber(string phone)
        {
            // Phone number validation: exactly 11 digits
            string pattern = @"^\d{11}$";
            return Regex.IsMatch(phone, pattern);
        }
        private bool EmailExists(string email, MySqlConnection connection)
        {
            string query = "SELECT COUNT(*) FROM tbl_staff WHERE staffEmail = @email";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@email", email);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string fname = lblfname.Text;
                    string lname = lblLname.Text;
                    string address = lblAddress.Text;
                    string phone = lblPhone.Text;
                    string email = lblEmail.Text;
                    string category = cmbxcat.SelectedItem?.ToString();

                    // Validate input fields
                    if (string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(email))
                    {
                        MessageBox.Show("Please fill in all required fields.");
                        return;
                    }
                    if (!IsValidEmail(email))
                    {
                        MessageBox.Show("Please enter a valid email address.");
                        return;
                    }
                    if (staffId == 0 && EmailExists(email, connection))
                    {
                        MessageBox.Show("Email already exists. Please use a different email address.");
                        return;
                    }
                    // Validate phone number format
                    if (!IsValidPhoneNumber(phone))
                    {
                        MessageBox.Show("Please enter a valid phone number with 11 digits.");
                        return;
                    }

                    int categoryId = GetCategoryId(category);

                    if (categoryId == -1)
                    {
                        MessageBox.Show("The selected category does not exist. Please choose a valid category.");
                        return;
                    }

                    byte[] imageData = null;

                    // Convert image to byte array if a new image is selected or if there's an existing image and it has changed
                    if (StaffImage.Image != null && (staffId == 0 || ImageChanged()))
                    {
                        imageData = ImageToByteArray(StaffImage.Image);
                    }

                    string insertUpdateQuery;

                    if (staffId == 0)
                    {
                        // Insert a new staff
                        insertUpdateQuery = "INSERT INTO tbl_staff (staffFname, staffLname, staffAddress, staffPhone, staffEmail, staffcatID, `Staff Category`, staffImage) " +
                                            "VALUES (@fname, @lname, @address, @phone, @email, @staffCategoryId, @staffCategory, @image)";
                    }
                    else
                    {
                        // Update an existing staff
                        insertUpdateQuery = "UPDATE tbl_staff SET staffFname = @fname, staffLname = @lname, staffAddress = @address, " +
                                            "staffPhone = @phone, staffEmail = @email, staffcatID = @staffCategoryId, " +
                                            "`Staff Category` = @staffCategory";

                        // Include staffImage update if imageData is provided
                        if (imageData != null)
                        {
                            insertUpdateQuery += ", staffImage = @image";
                        }

                        insertUpdateQuery += " WHERE staffId = @staffId";
                    }

                    using (MySqlCommand command = new MySqlCommand(insertUpdateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@staffId", staffId);
                        command.Parameters.AddWithValue("@fname", fname);
                        command.Parameters.AddWithValue("@lname", lname);
                        command.Parameters.AddWithValue("@address", address);
                        command.Parameters.AddWithValue("@phone", phone);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@staffCategoryId", categoryId);
                        command.Parameters.AddWithValue("@staffCategory", category);

                        // Add image parameter only if imageData is provided
                        if (imageData != null)
                        {
                            command.Parameters.AddWithValue("@image", imageData);
                        }

                        command.ExecuteNonQuery();

                        MessageBox.Show(staffId == 0 ? "Staff added successfully!" : "Staff updated successfully!");

                        // Your additional logic as needed

                        lblfname.Text = "";
                        lblLname.Text = "";
                        lblAddress.Text = "";
                        lblPhone.Text = "";
                        lblEmail.Text = "";
                        cmbxcat.SelectedItem = null;
                        StaffImage.Image = null;
                        OnStaffUpdated();
                        if (staffId != 0)
                        {
                            // Close the form only if it was an update operation
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                MessageBox.Show($"An unexpected error occurred: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Get the image format (use PNG format as default)
                ImageFormat format = ImageFormat.Png;

                // Determine the image format based on the file extension
                if (ImageFormat.Jpeg.Equals(image.RawFormat))
                {
                    format = ImageFormat.Jpeg;
                }
                else if (ImageFormat.Png.Equals(image.RawFormat))
                {
                    format = ImageFormat.Png;
                }
                else if (ImageFormat.Gif.Equals(image.RawFormat))
                {
                    format = ImageFormat.Gif;
                }
                else if (ImageFormat.Bmp.Equals(image.RawFormat))
                {
                    format = ImageFormat.Bmp;
                }
                else if (ImageFormat.Tiff.Equals(image.RawFormat))
                {
                    format = ImageFormat.Tiff;
                }
                else if (ImageFormat.Icon.Equals(image.RawFormat))
                {
                    format = ImageFormat.Icon;
                }

                // Save the image to the memory stream with the specified format
                image.Save(ms, format);

                // Return the byte array
                return ms.ToArray();
            }
        }
        private Image originalImage;

        // Method to set the original image
        private void SetOriginalImage(Image image)
        {
            originalImage = (Image)image.Clone();
        }
        // Method to check if the image has been changed
        private bool ImageChanged()
        {
            // Compare the current image with the original image (if available) to check for changes
            // For simplicity, you may need to implement your own logic to compare images properly
            // This method assumes StaffImage is a PictureBox control where the original image is stored
            if (StaffImage.Image != null && originalImage != null)
            {
                return !ImageEquals((Bitmap)StaffImage.Image, (Bitmap)originalImage);
            }
            return true; // If no original image is available or if the image control is empty, consider it changed
        }



        // Method to compare two images for equality
        private bool ImageEquals(Bitmap image, Bitmap originalImage)
        {
            // Compare image sizes
            if (image.Size != originalImage.Size)
            {
                return false;
            }

            // Compare individual pixels
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    if (image.GetPixel(x, y) != originalImage.GetPixel(x, y))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private int GetCategoryId(string categoryName)
        {
            // Replace the connection string with your actual MySQL database connection string
           

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Replace "tbl_category" with the actual table name in your database
                string query = "SELECT staffcatID FROM tbl_staffcategory WHERE catName = @catName";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@catName", categoryName);

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }

            return -1; // Return -1 if category ID is not found
        }
        private void LoadCategoryNames()
        {
            // Replace the connection string with your actual MySQL database connection string
            string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Replace "tbl_category" with the actual table name in your database
                string query = "SELECT catName FROM tbl_staffcategory";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        // Clear existing items in the Guna ComboBox
                        cmbxcat.Items.Clear();

                        // Check if there are rows in the result set
                        if (reader.HasRows)
                        {
                            // Loop through each row
                            while (reader.Read())
                            {
                                // Assuming "catName" is a string column in your table
                                string categoryName = reader["catName"].ToString();

                                // Add the categoryName to the Guna ComboBox
                                cmbxcat.Items.Add(categoryName);
                            }
                        }
                        else
                        {
                            // Handle the case when there are no rows in the result set
                        }
                    }
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        protected virtual void OnStaffUpdated()
        {
            StaffUpdated?.Invoke(this, EventArgs.Empty);
        }

       

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Choose Image(*.jpg;*.png;*.png;)|*.jpg;*.png;*.png;";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StaffImage.Image = Image.FromFile(openFileDialog.FileName);
            }
        }
    }
}
