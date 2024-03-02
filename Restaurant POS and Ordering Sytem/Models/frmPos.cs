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
    public partial class frmPos : Form
    {
        string connectionString = @"server=localhost;database=pos_ordering_system;userid=root;password=;";

        public string UserRole { get; set; }
        private string username;
        public frmPos(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

            if (UserRole == "admin")
            {
                // Admin logout behavior (go to MainForm)
                MainForm mf = new MainForm(username);
                mf.Show();
            }
            else if (UserRole == "cashier")
            {
                // Cashier logout behavior (go to SubForm)
                Subform sf = new Subform(username);
                sf.Show();
            }

            this.Close();
        }

        private void frmPos_Load(object sender, EventArgs e)
        {
            AddCategory();
        }
        private void AddCategory()
        {
            string qry = "Select * from tbl_category";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(qry, connection))
                {
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();

                        connection.Open(); // Open the connection before executing the command
                        da.Fill(dt); // Corrected line

                        CategoryPanel.Controls.Clear();

                        // Add a button for "All Categories"
                        Guna.UI2.WinForms.Guna2Button allCategoriesButton = new Guna.UI2.WinForms.Guna2Button();
                        allCategoriesButton.FillColor = Color.FromArgb(64, 64, 64);
                        allCategoriesButton.Size = new Size(249, 45);
                        allCategoriesButton.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
                        allCategoriesButton.Text = "All Categories";
                        allCategoriesButton.Font = new Font("Segoe UI", 14, FontStyle.Bold); // Set the font size
                        allCategoriesButton.Click += BtnAllCategory_Click; // Attach the event handler
                        CategoryPanel.Controls.Add(allCategoriesButton);

                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                                b.FillColor = Color.FromArgb(64, 64, 64);
                                b.Size = new Size(249, 45);
                                b.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
                                b.Text = row["catName"].ToString();
                                b.Font = new Font("Segoe UI", 14, FontStyle.Bold); // Set the font size

                                CategoryPanel.Controls.Add(b);
                            }
                        }
                    }
                }
            }
        }

        private void BtnAllCategory_Click(object sender, EventArgs e)
        {
            // Handle the "All Categories" button click
            // You can implement the logic to show all categories here
        }





    }

}
