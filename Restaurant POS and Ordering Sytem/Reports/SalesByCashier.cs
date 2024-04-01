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
    public partial class SalesByCashier : Form
    {
        string connectionString = "server=localhost;database=pos_ordering_system;userid=root;password=;";

        public SalesByCashier()
        {
            InitializeComponent();
            LoadCashierNames();
            guna2DataGridViewSalesbYCashier.RowTemplate.Height = 200;

            cmbxCashierName.SelectedIndexChanged += (sender, e) => { RetrieveSalesData(); };
            // Subscribe to the value changed events of the date pickers
            guna2DateTimePicker1.ValueChanged += (sender, e) => { RetrieveSalesData(); };
            guna2DateTimePicker2.ValueChanged += (sender, e) => { RetrieveSalesData(); };

        }

        private void LoadCashierNames()
        {
            cmbxCashierName.Items.Clear();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT staffID, staffFname FROM tbl_staff WHERE staffcatID IN (SELECT staffcatID FROM tbl_staffcategory WHERE catName IN ('Cashier', 'Admin', 'Manager'))";


                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string staffFname = reader["staffFname"].ToString();
                                cmbxCashierName.Items.Add(staffFname);
                            }
                        }
                        else
                        {
                            guna2MessageDialog1.Show("No cashiers found.");
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
            string cashierName = cmbxCashierName.SelectedItem?.ToString();
            DateTime startDate = guna2DateTimePicker1.Value;
            DateTime endDate = guna2DateTimePicker2.Value;

            if (string.IsNullOrEmpty(cashierName))
            {
                guna2MessageDialog1.Show("Please select a cashier.");
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT tbl_staff.staffImage, tbl_staff.staffFname, SUM(tblmain.total) AS TotalSales " +
                                     "FROM tblmain " +
                                     "INNER JOIN users ON tblmain.userId = users.userId " +
                                     "INNER JOIN tbl_staff ON users.staffID = tbl_staff.staffID " +
                                     "WHERE tbl_staff.staffFname = @cashierName " +
                                     "AND DATE(tblmain.aDate) BETWEEN DATE(@startDate) AND DATE(@endDate) " +  // Consider dates within the specified range
                                     "AND tblmain.status = 'check Out' " +
                                     "GROUP BY tbl_staff.staffImage, tbl_staff.staffFname";





                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@cashierName", cashierName);
                        command.Parameters.AddWithValue("@startDate", startDate);
                        command.Parameters.AddWithValue("@endDate", endDate);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable salesData = new DataTable();
                            adapter.Fill(salesData);

                            if (salesData.Rows.Count > 0)
                            {
                                // Bind sales data to DataGridView
                                guna2DataGridViewSalesbYCashier.DataSource = salesData;

                                // Adjust DataGridViewImageColumn properties
                                guna2DataGridViewSalesbYCashier.DataSource = salesData;

                                // Adjust DataGridViewImageColumn properties
                                DataGridViewImageColumn imageColumn = (DataGridViewImageColumn)guna2DataGridViewSalesbYCashier.Columns["staffImage"];
                                imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch; // Stretch image to fit cell


                                // Set width and height for the image
                                imageColumn.Width = 150; // Set the width as desired

                                if (salesData.Rows[0]["staffImage"] != DBNull.Value)
                                {
                                    byte[] imageData = (byte[])salesData.Rows[0]["staffImage"];
                                    using (MemoryStream ms = new MemoryStream(imageData))
                                    {
                                        Image cashierImage = Image.FromStream(ms);
                                        imageColumn.DefaultCellStyle.NullValue = null; // Clear any existing null value
                                        imageColumn.DefaultCellStyle.NullValue = cashierImage; // Assign the image again to reflect changes
                                    }
                                }

                                // Set column headers
                                guna2DataGridViewSalesbYCashier.Columns["staffFname"].DefaultCellStyle.Font = new Font("Arial", 12); // Increase font size
                                guna2DataGridViewSalesbYCashier.Columns["staffFname"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center-align the header text
                                guna2DataGridViewSalesbYCashier.Columns["TotalSales"].DefaultCellStyle.Font = new Font("Arial", 12); // Increase font size
                                guna2DataGridViewSalesbYCashier.Columns["TotalSales"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center-align the header text
                                guna2DataGridViewSalesbYCashier.Columns["staffFname"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center-align staff name
                                guna2DataGridViewSalesbYCashier.Columns["TotalSales"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center-align sales
                                guna2DataGridViewSalesbYCashier.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Fill DataGridView width
                                                                                                                            // Set row height for the DataGridView

                            }
                            else
                            {
                                guna2MessageDialog1.Show("No sales data found for the selected cashier within the specified date range.");
                                guna2DataGridViewSalesbYCashier.DataSource = null;
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
            if (guna2DataGridViewSalesbYCashier.Rows.Count == 0)
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

                            PdfPTable table = new PdfPTable(guna2DataGridViewSalesbYCashier.Columns.Count);

                            // Add headers
                            foreach (DataGridViewColumn column in guna2DataGridViewSalesbYCashier.Columns)
                            {
                                PdfPCell headerCell = new PdfPCell(new iTextSharp.text.Phrase(column.HeaderText));
                                headerCell.HorizontalAlignment = Element.ALIGN_CENTER; // Center-align header text
                                table.AddCell(headerCell);
                            }

                            // Add rows
                            foreach (DataGridViewRow row in guna2DataGridViewSalesbYCashier.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    if (cell.OwningColumn.Name == "staffImage") // Check if the cell is in the image column
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

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbxCashierName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSaveAsExcel_Click(object sender, EventArgs e)
        {
            if (guna2DataGridViewSalesbYCashier.Rows.Count == 0)
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
                            worksheet.Cell(1, 1).Value = "Staff Name";
                            worksheet.Cell(1, 2).Value = "Total Sales";

                            // Add data
                            for (int i = 0; i < guna2DataGridViewSalesbYCashier.Rows.Count; i++)
                            {
                                // Retrieve staff name and total sales from DataGridView
                                string staffName = guna2DataGridViewSalesbYCashier.Rows[i].Cells["staffFname"].Value?.ToString() ?? "";
                                string totalSales = guna2DataGridViewSalesbYCashier.Rows[i].Cells["TotalSales"].Value?.ToString() ?? "";

                                // Write staff name and total sales to Excel worksheet
                                worksheet.Cell(i + 2, 1).Value = staffName;
                                worksheet.Cell(i + 2, 2).Value = totalSales;
                            }

                            workbook.SaveAs(fileName);
                        }

                        guna2MessageDialog1.Show("Excel file saved successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                guna2MessageDialog2.Show("Error: " + ex.Message);
            }
        }
    }
}