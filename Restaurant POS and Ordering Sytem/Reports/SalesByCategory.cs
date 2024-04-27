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
using Image = System.Drawing.Image;
using Font = System.Drawing.Font;
using ClosedXML.Excel;

namespace Restaurant_POS_and_Ordering_Sytem.Reports
{
    public partial class SalesByCategory : Form
    {
        string connectionString = "server=localhost;database=pos_ordering_system;userid=root;password=;";

        public SalesByCategory()
        {
            InitializeComponent();
            LoadCategories();
            guna2DataGridViewSalesByCategory.RowTemplate.Height = 200; // Set row height

            cmbxCategory.SelectedIndexChanged += (sender, e) => { RetrieveSalesData(); };

            // Subscribe to the value changed events of the date pickers
            guna2DateTimePicker1.ValueChanged += (sender, e) => { RetrieveSalesData(); };
            guna2DateTimePicker2.ValueChanged += (sender, e) => { RetrieveSalesData(); };
        }

        private void LoadCategories()
        {
            cmbxCategory.Items.Clear();
            cmbxCategory.Items.Add("All Product Category"); // Add "All Cashier" option


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT catID, catName FROM tbl_Category";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string categoryName = reader["catName"].ToString();
                                cmbxCategory.Items.Add(categoryName);
                            }
                        }
                        else
                        {
                            guna2MessageDialog1.Show("No categories found.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    guna2MessageDialog2.Show("Error: " + ex.Message);
                }
            }
        }

        private void RetrieveSalesData()
        {
            string categoryName = cmbxCategory.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(categoryName))
            {
                guna2MessageDialog1.Show("Please select a Product category.");
                return;
            }

            DateTime startDate = guna2DateTimePicker1.Value;
            DateTime endDate = guna2DateTimePicker2.Value;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT tbl_products.prodImage, tbl_products.prodName, SUM(tbldetails.qty * tbldetails.price) AS TotalSales " +
                                     "FROM tblmain " +
                                     "INNER JOIN tbldetails ON tblmain.MainID = tbldetails.MainID " +
                                     "INNER JOIN tbl_products ON tbldetails.prodID = tbl_products.prodID " +
                                     "INNER JOIN tbl_Category ON tbl_products.catID = tbl_Category.catID ";

                    // Check if "All Product Category" is selected
                    if (categoryName != "All Product Category")
                    {
                        query += "WHERE tbl_Category.catName = @categoryName ";
                    }

                    query += "AND DATE(tblmain.aDate) BETWEEN DATE(@startDate) AND DATE(@endDate) " + // Date range filter
                             "AND tblmain.status = 'Check Out' " +
                             "GROUP BY tbl_products.prodImage, tbl_products.prodName";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        if (categoryName != "All Product Category")
                        {
                            command.Parameters.AddWithValue("@categoryName", categoryName);
                        }
                        command.Parameters.AddWithValue("@startDate", startDate);
                        command.Parameters.AddWithValue("@endDate", endDate);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable salesData = new DataTable();
                            adapter.Fill(salesData);

                            if (salesData.Rows.Count > 0)
                            {
                                guna2DataGridViewSalesByCategory.DataSource = salesData;

                                DataGridViewImageColumn imageColumn = (DataGridViewImageColumn)guna2DataGridViewSalesByCategory.Columns["prodImage"];
                                imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
                                imageColumn.Width = 150;

                                if (salesData.Rows[0]["prodImage"] != DBNull.Value)
                                {
                                    byte[] imageData = (byte[])salesData.Rows[0]["prodImage"];
                                    using (MemoryStream ms = new MemoryStream(imageData))
                                    {
                                        Image prodImage = Image.FromStream(ms);
                                        imageColumn.DefaultCellStyle.NullValue = null;
                                        imageColumn.DefaultCellStyle.NullValue = prodImage;
                                    }
                                }

                                guna2DataGridViewSalesByCategory.Columns["prodName"].DefaultCellStyle.Font = new Font("Arial", 12);
                                guna2DataGridViewSalesByCategory.Columns["prodName"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                guna2DataGridViewSalesByCategory.Columns["TotalSales"].DefaultCellStyle.Font = new Font("Arial", 12);
                                guna2DataGridViewSalesByCategory.Columns["TotalSales"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                guna2DataGridViewSalesByCategory.Columns["prodName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                guna2DataGridViewSalesByCategory.Columns["TotalSales"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                guna2DataGridViewSalesByCategory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            }
                            else
                            {
                                guna2MessageDialog1.Show("No sales data found for the selected category within the specified date range.");
                                guna2DataGridViewSalesByCategory.DataSource = null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    guna2MessageDialog2.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnSavetoPDF_Click(object sender, EventArgs e)
        {
            if (guna2DataGridViewSalesByCategory.Rows.Count == 0)
            {
                guna2MessageDialog1.Show("No data to save.");
                return;
            }

            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                    saveFileDialog.FilterIndex = 1;
                    saveFileDialog.RestoreDirectory = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string fileName = saveFileDialog.FileName;

                        using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            iTextSharp.text.Document document = new iTextSharp.text.Document();
                            PdfWriter.GetInstance(document, fs);

                            document.Open();

                            PdfPTable table = new PdfPTable(guna2DataGridViewSalesByCategory.Columns.Count);

                            // Add headers
                            foreach (DataGridViewColumn column in guna2DataGridViewSalesByCategory.Columns)
                            {
                                PdfPCell headerCell = new PdfPCell(new iTextSharp.text.Phrase(column.HeaderText));
                                headerCell.HorizontalAlignment = Element.ALIGN_CENTER; // Center-align header text
                                table.AddCell(headerCell);
                            }

                            // Add rows
                            foreach (DataGridViewRow row in guna2DataGridViewSalesByCategory.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    if (cell.OwningColumn.Name == "prodImage") // Check if the cell is in the image column
                                    {
                                        if (cell.Value != null && cell.Value != DBNull.Value)
                                        {
                                            byte[] imageData = (byte[])cell.Value;
                                            iTextSharp.text.Image iTextImage = iTextSharp.text.Image.GetInstance(imageData);
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
                                        if (cell.ColumnIndex == 1 || cell.ColumnIndex == 2) // Check if the cell is in specific columns
                                        {
                                            dataCell.HorizontalAlignment = Element.ALIGN_CENTER; // Center-align horizontally
                                            dataCell.VerticalAlignment = Element.ALIGN_MIDDLE; // Center-align vertically
                                        }
                                        else
                                        {
                                            dataCell.HorizontalAlignment = Element.ALIGN_LEFT; // Align left
                                            dataCell.VerticalAlignment = Element.ALIGN_MIDDLE; // Center-align vertically
                                        }
                                        table.AddCell(dataCell);
                                    }
                                }
                            }

                            document.Add(table);
                            document.Close();
                        }

                        guna2MessageDialog1.Show("PDF file saved successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                guna2MessageDialog2.Show("Error: " + ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveAsExcel_Click(object sender, EventArgs e)
        {
            if (guna2DataGridViewSalesByCategory.Rows.Count == 0)
            {
                guna2MessageDialog1.Show("No data to save.");
                return;
            }

            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                    saveFileDialog.FilterIndex = 1;
                    saveFileDialog.RestoreDirectory = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string fileName = saveFileDialog.FileName;

                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Sales Data");

                            // Add headers
                            worksheet.Cell(1, 1).Value = "Date";
                            worksheet.Cell(1, 2).Value = "Product Name";
                            worksheet.Cell(1, 3).Value = "Total Sales";

                            // Add data
                            for (int i = 0; i < guna2DataGridViewSalesByCategory.Rows.Count; i++)
                            {
                                // Retrieve date, product name, and total sales from DataGridView
                                string date = guna2DateTimePicker1.Value.ToShortDateString() + " - " + guna2DateTimePicker2.Value.ToShortDateString();
                                string productName = guna2DataGridViewSalesByCategory.Rows[i].Cells["prodName"].Value?.ToString() ?? "";
                                string totalSales = guna2DataGridViewSalesByCategory.Rows[i].Cells["TotalSales"].Value?.ToString() ?? "";

                                // Write date, product name, and total sales to Excel worksheet
                                worksheet.Cell(i + 2, 1).Value = date;
                                worksheet.Cell(i + 2, 2).Value = productName;
                                worksheet.Cell(i + 2, 3).Value = totalSales;
                            }

                            workbook.SaveAs(fileName);
                        }

                        guna2MessageDialog1.Show("Excel file saved successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
