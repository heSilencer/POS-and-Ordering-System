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
    
    public partial class Waiter : Form
    {
        private string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";

        public Waiter()
        {
            InitializeComponent();
        }

        public string WaiterName;
        private void Waiter_Load(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open(); // Open the connection

                string qry = "SELECT * FROM tbl_staff WHERE `Staff Category` LIKE 'Waiter'";

                // Use the MySqlConnection to create the MySqlCommand
                using (MySqlCommand cmd = new MySqlCommand(qry, connection))
                {
                    DataTable dt = new DataTable();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                        b.Text = row["staffFname"].ToString();
                        b.Width = 150;
                        b.Height = 50;
                        b.FillColor = Color.FromArgb(241, 85, 126);
                        b.HoverState.FillColor = Color.FromArgb(50, 56, 89);

                        b.Click += new EventHandler(CategoryButton_Click);

                        // Add the button to the form's controls collection
                        flowLayoutPanel1.Controls.Add(b);
                    }
                }
            }
        }
        private void CategoryButton_Click(object sender, EventArgs e)
        {
            WaiterName = (sender as Guna.UI2.WinForms.Guna2Button).Text.ToString();
            this.Close();
        }
    }
}
