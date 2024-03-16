using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Restaurant_POS_and_Ordering_Sytem.Models
{
    
    public partial class Table : Form
    {
        private string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";
        private List<string> selectedTables = new List<string>();
        public Table()
        {
            InitializeComponent();
        }
        public string TableName { get; private set; }
        private void Table_Load(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open(); // Open the connection

                string qry = "SELECT tblName FROM tbl_table";

                // Use the MySqlConnection to create the MySqlCommand
                using (MySqlCommand cmd = new MySqlCommand(qry, connection))
                {
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        string tableName = row["tblName"].ToString();

                        // Create a button for the table
                        Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                        b.Text = tableName;
                        b.Width = 150;
                        b.Height = 50;
                        b.FillColor = Color.FromArgb(241, 85, 126);
                        b.HoverState.FillColor = Color.FromArgb(50, 56, 89);
                        b.Click += new EventHandler(CategoryButton_Click);

                        // Check if the table is already selected
                        if (selectedTables.Contains(tableName))
                        {
                            // Disable the button if the table is already selected
                            b.Enabled = false;
                            b.FillColor = Color.Gray; // Change color to indicate it's disabled
                        }

                        // Add the button to the form's controls collection
                        flowLayoutPanel1.Controls.Add(b);
                    }
                }
            }
        }

        private void CategoryButton_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button selectedButton = sender as Guna.UI2.WinForms.Guna2Button;
            TableName = selectedButton.Text;

            // Add the selected table to the list of selected tables
            selectedTables.Add(TableName);

            // Disable the selected button
            selectedButton.Enabled = false;
            selectedButton.FillColor = Color.Gray; // Change color to indicate it's disabled

            this.Close();
        }
    }
}
