using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Windows.Forms.DataVisualization.Charting;

namespace Restaurant_POS_and_Ordering_Sytem.View
{
    public partial class frmdashboardView : Form
    {
        string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";

        public frmdashboardView()
        {
            InitializeComponent();
            cmbxdate.SelectedIndexChanged += cmbxdate_SelectedIndexChanged;

        }
        private void cmbxdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if an order type is selected
            if (cmbxdate.SelectedIndex > 0) // Assuming the first item is "Choose an Order Type"
            {
                string selectedOrderType = cmbxdate.SelectedItem.ToString();

                // Define the query to calculate total revenue for the selected order type
                string query = "SELECT SUM(Total) AS TotalRevenue FROM tblMain WHERE OrderType = @selectedOrderType AND Status = 'Check Out'";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@selectedOrderType", selectedOrderType);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            decimal totalRevenue = Convert.ToDecimal(result);

                            // Clear existing series in the chart
                            chart4.Series.Clear();

                            // Add new series with custom settings
                            Series series = new Series();
                            series.ChartType = SeriesChartType.Column; // Set chart type to column (bar graph)
                            series["PixelPointWidth"] = "200"; // Set width of the bars

                            // Concatenate order type with legend text
                            series.Name = $"{selectedOrderType}:  {totalRevenue}";

                            // Add data point to the new series
                            series.Points.AddXY(selectedOrderType, totalRevenue);

                            // Add the new series to the chart
                            chart4.Series.Add(series);

                            // Clear existing legends and add a new legend at the bottom
                            chart4.Legends.Clear();
                            Legend legend = new Legend("Legend1");
                            legend.Docking = Docking.Bottom;
                            legend.Font = new Font("Arial", 12);
                            chart4.Legends.Add(legend);
                        }
                        else
                        {
                            guna2MessageDialog1.Show("No revenue data found for selected order type.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error retrieving revenue data: " + ex.Message);
                    }
                }
            }
         
        }

        private void DisplayTotalHoldOrders()
        {
            // SQL query to calculate the sum of hold orders from tblMain
            string query = "SELECT COUNT(*) AS TotalHoldOrders FROM tblMain WHERE Status = 'Hold'";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        // Display the total hold orders in the label
                        lblHold.Text = result.ToString(); // Assuming lblHold is the name of your label
                    }
                    else
                    {
                        // No hold orders found
                        lblHold.Text = "0";
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors
                    guna2MessageDialog2.Show("Error retrieving total hold orders: " + ex.Message);
                }
            }

        }
        private void DisplayTotalProducts()
        {
            string query = "SELECT COUNT(*) AS TotalProducts FROM tbl_products";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        // Display the total number of products in the label
                        lblproducts.Text = result.ToString(); // Assuming lblTotalProducts is the name of your label
                    }
                    else
                    {
                        // No products found
                        lblproducts.Text = "0";
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors
                    guna2MessageDialog2.Show("Error retrieving total number of products: " + ex.Message);
                }
            }
        }
        private void DisplayTotalCategory()
        {
            string query = "SELECT COUNT(*) AS TotalCategories FROM tbl_category";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        // Display the total number of categories in the label
                        lblcategories.Text = result.ToString(); // Assuming lblTotalCategories is the name of your label
                    }
                    else
                    {
                        // No categories found
                        lblcategories.Text = "0";
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors
                    guna2MessageDialog2.Show("Error retrieving total number of categories: " + ex.Message);
                }
            }
        }
        private void DisplayTotalRevenue()
        {
            string query = "SELECT  SUM(Total) AS TotalRevenue FROM tblMain WHERE Status = 'Check Out'";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        // Display the total revenue in the label
                        lbltotalrev.Text = result.ToString(); // Assuming lblTotalRevenue is the name of your label
                    }
                    else
                    {
                        // No revenue found
                        lbltotalrev.Text = "0";
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors
                    guna2MessageDialog2.Show("Error retrieving total revenue: " + ex.Message);
                }
            }
        }
        //////////////////////////////////


        private void frmdashboardView_Load(object sender, EventArgs e)
        {

            ///
            DisplayTotalHoldOrders();
            DisplayTotalProducts();
            DisplayTotalCategory();
            DisplayTotalRevenue();
            ///
            cmbxdate.Items.Add("Choose an Order Type");
            cmbxdate.SelectedIndex = 0;
            cmbxdate.DrawMode = DrawMode.OwnerDrawFixed;
            cmbxdate.DrawItem += Cmbxdate_DrawItem;

            // Set chart title
            Title chartTitle = new Title("Total Revenue per Order Type", Docking.Top, new Font("Arial", 16, FontStyle.Bold), Color.Black);
            chart4.Titles.Add(chartTitle);
            // Display the total number of products in the label
          
            LoadOrderTypeChart();
            LoadTotalRevenueToday();
            LoadTotalSalesByDate();
            LoadDates();
        }
        private void Cmbxdate_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == 0) // Check if it's the default text
            {
                // Set the font style and size for the default text
                Font defaultFont = new Font(cmbxdate.Font.FontFamily, 10f, FontStyle.Bold);
                e.DrawBackground();
                e.Graphics.DrawString(cmbxdate.Items[e.Index].ToString(), defaultFont, Brushes.Black, e.Bounds);
            }
            else
            {
                // Draw other items without bold font style
                e.DrawBackground();
                e.Graphics.DrawString(cmbxdate.Items[e.Index].ToString(), cmbxdate.Font, Brushes.Black, e.Bounds);
            }
        }


        private void LoadOrderTypeChart()
        {
            chart1.Series[0].Points.Clear();
            // Set chart title
            chart1.Titles.Clear();
            Title chartTitle = new Title("Total Order Type", Docking.Top, new Font("Arial", 16, FontStyle.Bold), Color.Black);
            chart1.Titles.Add(chartTitle);

            // Set legend font size
            chart1.Legends[0].Font = new Font("Arial", 12); // Adjust the font size as needed

            // Connect to the database and retrieve data
            string query = "SELECT OrderType, COUNT(*) AS TotalOrders FROM tblMain GROUP BY OrderType";
            using (MySqlConnection connection = new MySqlConnection(@"server=localhost;database=pos_ordering_system;userid=root;password=;"))
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                try
                {
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    // Add data points to the pie chart
                    while (reader.Read())
                    {
                        string orderType = reader.GetString("OrderType");
                        int totalOrders = reader.GetInt32("TotalOrders");

                        // Calculate percentage
                        double percentage = ((double)totalOrders / GetTotalOrderCount()) * 100;

                        // Add data point with label showing both the order type and percentage
                        chart1.Series[0].Points.AddXY($"{orderType} ({percentage:F2}%)", totalOrders);
                    }

                    // Close database connection
                    reader.Close();
                }
                catch (Exception ex)
                {
                    guna2MessageDialog2.Show($"Error: {ex.Message}");
                }
            }
        }
        private int GetTotalOrderCount()
        {
            int totalOrders = 0;

            // Connect to the database and retrieve total order count
            string query = "SELECT COUNT(*) AS TotalOrders FROM tblMain";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                try
                {
                    connection.Open();
                    totalOrders = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    guna2MessageDialog2.Show($"Error: {ex.Message}");
                }
            }

            return totalOrders;
        }
        private void LoadTotalRevenueToday()
        {
            DateTime today = DateTime.Today;
            chart2.Legends[0].Font = new Font("Arial", 12);
            // Format today's date in the MySQL-compatible format (YYYY-MM-DD)
            string todayFormatted = today.ToString("yyyy-MM-dd");

            // Query to retrieve total revenue for today only for orders with status 'Check Out'
            string query = $"SELECT SUM(Total) AS TotalRevenue FROM tblMain WHERE aDate = '{todayFormatted}' AND Status = 'Check Out'";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                try
                {
                    connection.Open();
                    object result = cmd.ExecuteScalar();

                    // Check if the result is not DBNull
                    if (result != DBNull.Value)
                    {
                        double totalRevenue = Convert.ToDouble(result);

                        // Add the total revenue as a data point to the line chart
                        chart2.Series[0].Points.AddXY(todayFormatted, totalRevenue);

                        // Set chart title
                        chart2.Titles.Add("Today's Revenue");
                        chart2.Titles[0].Font = new Font("Arial", 16, FontStyle.Bold);

                        // Add legend to display total
                 
                        chart2.Series[0].LegendText = $"Total ({totalRevenue.ToString("C")})";
                    }
                    else
                    {
                        // If no revenue recorded, set a label indicating no revenue
                        chart2.Titles.Add("No revenue recorded for today");
                        chart2.Titles[0].Font = new Font("Arial", 16, FontStyle.Bold);
                    }
                }
                catch (Exception ex)
                {
                    // Display error message in case of any exception
                    guna2MessageDialog2.Show($"Error: {ex.Message}");
                }
            }
        }
        private void LoadTotalSalesByDate()
        {
            chart3.Series.Clear();
            chart3.Legends.Clear();

            // Query to retrieve total revenue for each date for orders with status 'Check Out'
            string query = "SELECT aDate, SUM(Total) AS TotalRevenue FROM tblMain WHERE Status = 'Check Out' GROUP BY aDate";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                try
                {
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    // Create a new series for the chart
                    Series series = new Series();
                    series.ChartType = SeriesChartType.Line;
                    series.BorderWidth = 3;
                    series.Name = "Total Revenue"; // Set the series name

                    // Loop through the result set
                    while (reader.Read())
                    {
                        // Retrieve date and total revenue from the result set
                        DateTime date = reader.GetDateTime("aDate");
                        double totalRevenue = Convert.ToDouble(reader["TotalRevenue"]);

                        // Add the data point to the series
                        series.Points.AddXY(date.ToShortDateString(), totalRevenue);
                    }

                    // Add the series to the chart
                    chart3.Series.Add(series);

                    // Set chart title
                    chart3.Titles.Add("Total Revenue by Date");
                    chart3.Titles[0].Font = new Font("Arial", 16, FontStyle.Bold);

                    // Set axis labels
                    chart3.ChartAreas[0].AxisX.Title = "Date";
                    chart3.ChartAreas[0].AxisY.Enabled = AxisEnabled.False; // Disable Y-axis on the left side
                    chart3.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True; // Enable Y-axis on the right side
                    chart3.ChartAreas[0].AxisY2.Title = "Total Revenue";
                    chart3.ChartAreas[0].AxisY2.IsReversed = false; // Move Y-axis to the right side
                    chart3.ChartAreas[0].AxisY2.LineColor = Color.Transparent;

                    // Set font size for axis labels
                    chart3.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 10);

                    // Add a legend to the chart
                    Legend legend = new Legend();
                    legend.Name = "Legend1"; // Set the legend name
                    legend.Font = new Font("Arial", 12);
                    legend.Docking = Docking.Bottom; // Set legend position to bottom

                    chart3.Legends.Add(legend);

                    // Associate the series with the legend
                    series.Legend = "Legend1"; // Use the legend name

                }
                catch (Exception ex)
                {
                    // Display error message in case of any exception
                    guna2MessageDialog2.Show($"Error: {ex.Message}");
                }
            }
        }

        private void LoadDates()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT DISTINCT OrderType FROM tblMain WHERE Status = 'Check Out'";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                try
                {
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Add the order type to the ComboBox
                        cmbxdate.Items.Add(reader["OrderType"].ToString());
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    guna2MessageDialog2.Show("Error loading order types: " + ex.Message);
                }
            }
        }

        
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
