using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
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

namespace Restaurant_POS_and_Ordering_Sytem.Reports
{
    public partial class StaffList : Form
    {
        public StaffList()
        {
            InitializeComponent();
            InitializeDataGridView();
            LoadProducts();
        }
        private void InitializeDataGridView()
        {
            guna2DataGridViewProducts.RowTemplate.Height = 200; // Set the row height to accommodate the images

            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "staffImage";
            imageColumn.HeaderText = "Staff Image";
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom; // Stretch the image to fill the cell
            imageColumn.Width = 200; // Set the width of the image column
            guna2DataGridViewProducts.Columns.Add(imageColumn);

            guna2DataGridViewProducts.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 15);

            // Add columns for staff details
            guna2DataGridViewProducts.Columns.Add("staffName", "Staff Name");
            guna2DataGridViewProducts.Columns["staffName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Fill the remaining space
            guna2DataGridViewProducts.Columns["staffName"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center-align the header text
            guna2DataGridViewProducts.Columns["staffName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center-align the data

            guna2DataGridViewProducts.Columns.Add("staffEmail", "Email");
            guna2DataGridViewProducts.Columns["staffEmail"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            guna2DataGridViewProducts.Columns["staffEmail"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            guna2DataGridViewProducts.Columns["staffEmail"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            guna2DataGridViewProducts.Columns.Add("staffPhone", "Phone");
            guna2DataGridViewProducts.Columns["staffPhone"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            guna2DataGridViewProducts.Columns["staffPhone"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            guna2DataGridViewProducts.Columns["staffPhone"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            guna2DataGridViewProducts.Columns.Add("staffAddress", "Address");
            guna2DataGridViewProducts.Columns["staffAddress"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            guna2DataGridViewProducts.Columns["staffAddress"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            guna2DataGridViewProducts.Columns["staffAddress"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        private void LoadProducts()
        {
            DataTable staffData = GetStaffDataFromDatabase();

            foreach (DataRow staff in staffData.Rows)
            {
                // Set staff image from database
                byte[] imgData = (byte[])staff["staffImage"];
                System.Drawing.Image image;
                using (MemoryStream ms = new MemoryStream(imgData))
                {
                    image = System.Drawing.Image.FromStream(ms);
                }

                // Add row to DataGridView
                guna2DataGridViewProducts.Rows.Add(new object[] { image, staff["staffFname"].ToString() + " " + staff["staffLname"].ToString(), staff["staffEmail"].ToString(), staff["staffPhone"].ToString(), staff["staffAddress"].ToString() });
            }
        }

        private DataTable GetStaffDataFromDatabase()
        {
            string connectionString = "server=localhost;database=pos_ordering_system;userid=root;password=;";
            string query = "SELECT staffFname, staffLname, staffEmail, staffPhone, staffAddress, staffImage FROM tbl_staff";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                DataTable staffData = new DataTable();

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    staffData.Load(reader);
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

                return staffData;
            }
        }

      
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSavetoPDF_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;

                using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    Document document = new Document();
                    PdfWriter.GetInstance(document, fs);

                    document.Open();

                    PdfPTable table = new PdfPTable(guna2DataGridViewProducts.Columns.Count);

                    // Add headers
                    foreach (DataGridViewColumn column in guna2DataGridViewProducts.Columns)
                    {
                        PdfPCell headerCell = new PdfPCell(new Phrase(column.HeaderText));
                        headerCell.HorizontalAlignment = Element.ALIGN_CENTER; // Center-align header text
                        table.AddCell(headerCell);
                    }

                    // Add rows
                    foreach (DataGridViewRow row in guna2DataGridViewProducts.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell.ColumnIndex == 0) // Check if the cell is in the image column
                            {
                                if (cell.Value != null && cell.Value is System.Drawing.Image)
                                {
                                    // Convert System.Drawing.Image to iTextSharp.text.Image
                                    System.Drawing.Image img = (System.Drawing.Image)cell.Value;
                                    iTextSharp.text.Image iTextImage = iTextSharp.text.Image.GetInstance(img, BaseColor.WHITE);
                                    PdfPCell imageCell = new PdfPCell(iTextImage, true);
                                    imageCell.HorizontalAlignment = Element.ALIGN_CENTER; // Center-align image
                                    imageCell.VerticalAlignment = Element.ALIGN_MIDDLE; // Center-align vertically
                                    table.AddCell(imageCell);
                                }
                                else
                                {
                                    PdfPCell emptyCell = new PdfPCell();
                                    table.AddCell(emptyCell); // Add an empty cell if no image is present
                                }
                            }
                            else
                            {
                                PdfPCell dataCell = new PdfPCell(new Phrase(cell.Value?.ToString() ?? ""));
                                dataCell.HorizontalAlignment = Element.ALIGN_CENTER; // Center-align horizontally
                                dataCell.VerticalAlignment = Element.ALIGN_MIDDLE; // Center-align vertically
                                table.AddCell(dataCell);
                            }
                        }
                    }

                    document.Add(table);
                    document.Close();
                }

                guna2MessageDialog1.Show("PDF file saved successfully.");
                this.Close();
            }
        }
    }
}
