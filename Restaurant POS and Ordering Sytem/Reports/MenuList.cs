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
    public partial class MenuList : Form
    {
        public MenuList()
        {
            InitializeComponent();

            InitializeDataGridView();
            LoadProducts();
        }

        private void InitializeDataGridView()
        {
            guna2DataGridViewProducts.RowTemplate.Height = 200; // Set the row height to accommodate the images

            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn.Name = "prodImage";
            imageColumn.HeaderText = "Product Image";
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom; // Stretch the image to fill the cell
            imageColumn.Width = 200; // Set the width of the image column
            guna2DataGridViewProducts.Columns.Add(imageColumn);

            // Add columns for product name and price
            guna2DataGridViewProducts.Columns.Add("prodName", "Product Name");
            guna2DataGridViewProducts.Columns["prodName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Fill the remaining space
            guna2DataGridViewProducts.Columns["prodName"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center-align the header text
            guna2DataGridViewProducts.Columns["prodName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center-align the data

            guna2DataGridViewProducts.Columns.Add("prodPrice", "Product Price");
            guna2DataGridViewProducts.Columns["prodPrice"].Width = 150; // Set a fixed width for the price column
            guna2DataGridViewProducts.Columns["prodPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center-align the data
        
    }

        private void LoadProducts()
        {
            DataTable products = GetProductsFromDatabase();

            foreach (DataRow product in products.Rows)
            {
                // Set product image from database
                byte[] imgData = (byte[])product["prodImage"];
                System.Drawing.Image image; // Fully qualify System.Drawing.Image
                using (MemoryStream ms = new MemoryStream(imgData))
                {
                    image = System.Drawing.Image.FromStream(ms); // Fully qualify System.Drawing.Image
                }

                // Add row to DataGridView
                guna2DataGridViewProducts.Rows.Add(new object[] { image, product["prodName"].ToString(), product["prodPrice"].ToString() });
            }
        }

        private DataTable GetProductsFromDatabase()
        {
            string connectionString = "server=localhost;database=pos_ordering_system;userid=root;password=;";
            string query = "SELECT prodName, prodPrice, prodImage FROM tbl_products";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                DataTable products = new DataTable();

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    products.Load(reader);
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

                return products;
            }
        }

        private void btnSavetoPDF_Click(object sender, EventArgs e)
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
                    iTextSharp.text.Document document = new iTextSharp.text.Document(); // Fully qualify iTextSharp.text.Document
                    PdfWriter.GetInstance(document, fs);

                    document.Open();

                    PdfPTable table = new PdfPTable(guna2DataGridViewProducts.Columns.Count);

                    // Add headers
                    foreach (DataGridViewColumn column in guna2DataGridViewProducts.Columns)
                    {
                        PdfPCell headerCell = new PdfPCell(new iTextSharp.text.Phrase(column.HeaderText));
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
                                if (cell.Value != null && cell.Value is System.Drawing.Image) // Fully qualify System.Drawing.Image
                                {
                                    // Convert System.Drawing.Image to iTextSharp.text.Image
                                    System.Drawing.Image img = (System.Drawing.Image)cell.Value; // Fully qualify System.Drawing.Image
                                    iTextSharp.text.Image iTextImage = iTextSharp.text.Image.GetInstance(img, BaseColor.WHITE);
                                    PdfPCell imageCell = new PdfPCell(iTextImage, true);
                                    imageCell.HorizontalAlignment = Element.ALIGN_CENTER; // Center-align image
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
                                PdfPCell dataCell = new PdfPCell(new iTextSharp.text.Phrase(cell.Value?.ToString() ?? ""));
                                if (cell.ColumnIndex == 1 || cell.ColumnIndex == 2) // Check if the cell is in the Product name or Price column
                                {
                                    dataCell.HorizontalAlignment = Element.ALIGN_CENTER; // Center-align Product name and Price horizontally
                                    dataCell.VerticalAlignment = Element.ALIGN_MIDDLE; // Center-align vertically
                                }
                                else
                                {
                                    dataCell.HorizontalAlignment = Element.ALIGN_LEFT; // Align other cells to the left
                                    dataCell.VerticalAlignment = Element.ALIGN_MIDDLE; // Align vertically
                                }
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}