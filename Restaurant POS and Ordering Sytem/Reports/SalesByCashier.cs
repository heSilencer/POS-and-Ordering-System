using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Image = System.Drawing.Image;
using Font = System.Drawing.Font;
using ClosedXML.Excel;
namespace Restaurant_POS_and_Ordering_Sytem.Reports
{
    public partial class SalesByCashier : Form
    {
        bool allCashiersSelected = false;
        string connectionString = "server=localhost;database=pos_ordering_system;userid=root;password=;";

        public SalesByCashier()
        {
            InitializeComponent();
            LoadCashierNames();
            guna2DataGridViewSalesbYCashier.RowTemplate.Height = 200;
            guna2DataGridViewSalesbYCashier.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 15);

            cmbxCashierName.SelectedIndexChanged += (sender, e) => { RetrieveSalesData(); };
            // Subscribe to the value changed events of the date pickers
            guna2DateTimePicker1.ValueChanged += (sender, e) => { RetrieveSalesData(); };
            guna2DateTimePicker2.ValueChanged += (sender, e) => { RetrieveSalesData(); };

        }

        private void LoadCashierNames()
        {
            cmbxCashierName.Items.Clear();
            cmbxCashierName.Items.Add("All Cashier"); // Add "All Cashier" option

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT DISTINCT Uname FROM tblmain";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string uname = reader["Uname"].ToString();
                                cmbxCashierName.Items.Add(uname);
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
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }



        private void RetrieveSalesData()
        {
            string cashierName = cmbxCashierName.SelectedItem?.ToString();
            DateTime startDate = guna2DateTimePicker1.Value;
            DateTime endDate = guna2DateTimePicker2.Value;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT main.CashierImage, main.Uname AS staffFname, " +
                                   "(SELECT SUM(Total) FROM tblmain WHERE tblmain.Uname = main.Uname) AS TotalSales, " +
                                   "tbldetails.prodName, " +
                                   "SUM(tbldetails.qty) AS TotalQuantity, " +
                                   "tbldetails.price AS Price, " +
                                   "SUM(tbldetails.qty * tbldetails.price) AS TotalAmount " +
                                   "FROM tblmain AS main " +
                                   "LEFT JOIN tbldetails ON main.MainID = tbldetails.MainID " +
                                   "WHERE main.Status = 'Check Out' " +
                                   "AND (main.Uname = @cashierName OR @cashierName = 'All Cashier') " +
                                   "AND DATE(main.aDate) BETWEEN DATE(@startDate) AND DATE(@endDate) " +
                                   "GROUP BY main.CashierImage, main.Uname, tbldetails.prodName";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@cashierName", cashierName);
                        command.Parameters.AddWithValue("@startDate", startDate);
                        command.Parameters.AddWithValue("@endDate", endDate);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable salesData = new DataTable();
                            adapter.Fill(salesData);

                            // Check if the DataGridView is initialized before accessing it
                            if (guna2DataGridViewSalesbYCashier != null)
                            {
                                if (salesData.Rows.Count > 0)
                                {
                                    // Bind sales data to DataGridView
                                    guna2DataGridViewSalesbYCashier.DataSource = salesData;

                                    // Adjust DataGridViewImageColumn properties
                                    DataGridViewImageColumn imageColumn = (DataGridViewImageColumn)guna2DataGridViewSalesbYCashier.Columns["CashierImage"];
                                    imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom; // Stretch image to fit cell
                                    imageColumn.Width = 150; // Set the width as desired

                                    // Set column headers and other properties as needed
                                    guna2DataGridViewSalesbYCashier.Columns["staffFname"].DefaultCellStyle.Font = new Font("Arial", 12); // Increase font size
                                    guna2DataGridViewSalesbYCashier.Columns["staffFname"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center-align the header text
                                    guna2DataGridViewSalesbYCashier.Columns["TotalSales"].DefaultCellStyle.Font = new Font("Arial", 12); // Increase font size
                                    guna2DataGridViewSalesbYCashier.Columns["TotalSales"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center-align the header text
                                    guna2DataGridViewSalesbYCashier.Columns["staffFname"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center-align staff name
                                    guna2DataGridViewSalesbYCashier.Columns["TotalSales"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Center-align sales

                                    // Adjust the width and auto-size mode of SoldProducts column
                                    guna2DataGridViewSalesbYCashier.Columns["prodName"].Width = 300; // Set width as desired
                                    guna2DataGridViewSalesbYCashier.Columns["prodName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                                    return;
                                }
                                else
                                {
                                    guna2MessageDialog1.Show("No sales data found for the selected criteria within the specified date range.");
                                    guna2DataGridViewSalesbYCashier.DataSource = null;
                                }
                            }
                            else
                            {
                                // If the DataGridView is null, display an error message
                                guna2MessageDialog1.Show("Error: DataGridView is null.");
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
                MessageBox.Show("No data to save.");
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
                            Document document = new Document();
                            PdfWriter.GetInstance(document, fs);

                            document.Open();

                            PdfPTable table = new PdfPTable(guna2DataGridViewSalesbYCashier.Columns.Count);

                            // Add headers
                            foreach (DataGridViewColumn column in guna2DataGridViewSalesbYCashier.Columns)
                            {
                                PdfPCell headerCell = new PdfPCell(new Phrase(column.HeaderText));
                                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;

                                // Calculate the width of the header cell based on the length of the header text
                                float widthPercentage = (float)(column.HeaderText.Length * 7); // Adjust the multiplier as needed

                                // Set the width of the header cell
                                headerCell.FixedHeight = 25f; // Set the height as needed
                                headerCell.PaddingBottom = 5f; // Optional: Set padding for the bottom of the cell
                                headerCell.MinimumHeight = 25f; // Optional: Set minimum height

                                // Add the header cell to the table
                                table.AddCell(headerCell);
                            }


                            // Add data
                            foreach (DataGridViewRow row in guna2DataGridViewSalesbYCashier.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    if (cell.OwningColumn.Name == "CashierImage")
                                    {
                                        // Convert System.Drawing.Image to iTextSharp.text.Image
                                        byte[] byteArray = (byte[])cell.Value;
                                        iTextSharp.text.Image cashierImage = iTextSharp.text.Image.GetInstance(byteArray);

                                        // Resize the image as needed
                                        cashierImage.ScaleToFit(150f, 150f);

                                        // Add the image to the PDF document
                                        PdfPCell imageCell = new PdfPCell(cashierImage, true);
                                        table.AddCell(imageCell);
                                    }
                                    else
                                    {
                                        PdfPCell dataCell = new PdfPCell(new Phrase(cell.Value?.ToString() ?? ""));
                                        if (cell.ColumnIndex == 1 || cell.ColumnIndex == 2)
                                        {
                                            dataCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                            dataCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                        }
                                        else
                                        {
                                            dataCell.HorizontalAlignment = Element.ALIGN_LEFT;
                                            dataCell.VerticalAlignment = Element.ALIGN_MIDDLE;
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
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Method to convert byte array to Image
     

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
                MessageBox.Show("No data to save.");
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
                            int columnIndex = 0;
                            foreach (DataGridViewColumn column in guna2DataGridViewSalesbYCashier.Columns)
                            {
                                // Skip CashierImage column
                                if (column.Name != "CashierImage")
                                {
                                    columnIndex++;
                                    worksheet.Cell(1, columnIndex).Value = column.HeaderText;
                                }
                            }

                            // Add data
                            for (int i = 0; i < guna2DataGridViewSalesbYCashier.Rows.Count; i++)
                            {
                                columnIndex = 0;
                                for (int j = 0; j < guna2DataGridViewSalesbYCashier.Columns.Count; j++)
                                {
                                    // Skip CashierImage column
                                    if (guna2DataGridViewSalesbYCashier.Columns[j].Name != "CashierImage")
                                    {
                                        columnIndex++;
                                        worksheet.Cell(i + 2, columnIndex).Value = guna2DataGridViewSalesbYCashier.Rows[i].Cells[j].Value;
                                    }
                                }
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
    }
}