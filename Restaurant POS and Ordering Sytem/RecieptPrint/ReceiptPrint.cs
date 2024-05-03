using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_POS_and_Ordering_Sytem.RecieptPrint
{
    public partial class ReceiptPrint : Form
    {
        public ReceiptPrint()
        {
            InitializeComponent();
        }

        internal void DisplayOrderDetails(List<OrderDetail> orderDetails, DateTime orderDate)
        {
            panel1.Controls.Clear();

            Label lblQtyHeader = new Label();
            lblQtyHeader.Text = "Qty";
            lblQtyHeader.Location = new Point(10, 10);
            panel1.Controls.Add(lblQtyHeader);

            Label lblProductNameHeader = new Label();
            lblProductNameHeader.Text = "Product Name";
            lblProductNameHeader.Location = new Point(106, 10); // Adjusted location for better alignment
            panel1.Controls.Add(lblProductNameHeader);

            Label lblPriceHeader = new Label(); // New label for price header
            lblPriceHeader.Text = "Price";
            lblPriceHeader.Location = new Point(206, 10); // Adjusted location for better alignment
            panel1.Controls.Add(lblPriceHeader);

            Label lblAmountHeader = new Label();
            lblAmountHeader.Text = "Amount";
            lblAmountHeader.Location = new Point(306, 10); // Adjusted location for better alignment
            panel1.Controls.Add(lblAmountHeader);

            int yOffset = 40; // Initial Y position for data

            // Loop through each order detail and create labels for display
            foreach (var orderDetail in orderDetails)
            {
                // Create labels for order details
                Label lblQty = new Label();
                lblQty.Text = orderDetail.Quantity.ToString();
                lblQty.Location = new Point(10, yOffset);
                panel1.Controls.Add(lblQty);

                Label lblProductName = new Label();
                lblProductName.Text = orderDetail.ProductName;
                lblProductName.Location = new Point(106, yOffset); // Adjusted location for better alignment
                lblProductName.AutoSize = true;
                panel1.Controls.Add(lblProductName);

                Label lblPrice = new Label(); // New label for price
                lblPrice.Text = orderDetail.Price.ToString("0.00");
                lblPrice.Location = new Point(206, yOffset); // Adjusted location for better alignment
                panel1.Controls.Add(lblPrice);

                Label lblAmount = new Label();
                lblAmount.Text = orderDetail.Amount.ToString("0.00");
                lblAmount.Location = new Point(306, yOffset); // Adjusted location for better alignment
                panel1.Controls.Add(lblAmount);

                // Increment Y position for the next set of labels
                yOffset += 30;
            }

            // Set other details like order type, total, received, and change
            // Example:
            lblOderType.Text = orderDetails.First().OrderType; // Assuming all items have the same order type
            lbltotal.Text = orderDetails.First().Total.ToString("0.00"); // Assuming total is the same for all items
            lblCash.Text = orderDetails.First().Received.ToString("0.00"); // Assuming received amount is the same for all items
            lblChange.Text = orderDetails.First().Change.ToString("0.00");
            lblDate.Text = orderDate.ToString("yyyy-MM-dd");

        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(PrintPage);
            pd.Print();
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap bmp = new Bitmap(receiptPanel.Width, receiptPanel.Height);
            receiptPanel.DrawToBitmap(bmp, new System.Drawing.Rectangle(0, 0, receiptPanel.Width, receiptPanel.Height));
            e.Graphics.DrawImage(bmp, 0, 0);



        }

        private void btnSaveasPdf_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    // Convert pixels to points
                    float panelWidthInPoints = (float)(receiptPanel.Width * 1.05); // Assuming 1 pixel = 0.75 points
                    float panelHeightInPoints = (float)(receiptPanel.Height * 1.02); // Assuming 1 pixel = 0.75 points

                    // Create the PDF document with the calculated size
                    Document pdfDoc = new Document(new iTextSharp.text.Rectangle(panelWidthInPoints, panelHeightInPoints), 10f, 10f, 10f, 10f);
                    PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    using (MemoryStream receiptStream = new MemoryStream())
                    {
                        Bitmap bmpReceipt = new Bitmap(receiptPanel.Width, receiptPanel.Height);
                        receiptPanel.DrawToBitmap(bmpReceipt, new System.Drawing.Rectangle(0, 0, receiptPanel.Width, receiptPanel.Height));
                        bmpReceipt.Save(receiptStream, System.Drawing.Imaging.ImageFormat.Png);
                        iTextSharp.text.Image receiptImage = iTextSharp.text.Image.GetInstance(receiptStream.GetBuffer());
                        pdfDoc.Add(receiptImage);
                    }

                    pdfDoc.Close();
                }
                guna2MessageDialog1.Show("PDF file saved successfully.");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Separator1_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void lblDate_Click(object sender, EventArgs e)
        {

        }
    }
}
