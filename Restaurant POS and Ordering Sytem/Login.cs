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

namespace Restaurant_POS_and_Ordering_Sytem
{
    public partial class frmlogin : Form
    {
        private readonly MySqlConnection con;
        public frmlogin()
        {
            InitializeComponent();
            con = new MySqlConnection(@"server=localhost;database=pos_ordering_system;userid=root;password=;");
        }
       

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //See if there is no username and password
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                guna2MessageDialog1.Show("Please input both username and password.");
                return;
            }

            // Open a connection and authenticate the user
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM users WHERE username=@username AND userpass=@password";
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                try
                {
                    con.Open();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            // Get the user role from the database
                            string userRole = dr["role"].ToString();

                            // Check the user role
                            if (userRole == "admin")
                            {
                                // Admin login successful
                                this.Hide();
                                MainForm mf = new MainForm();
                                mf.Show();
                            }
                            else if (userRole == "cashier")
                            {
                                // Cashier login successful
                                this.Hide();
                                Subform sf = new Subform();
                                sf.Show();
                            }
                        }
                        else
                        {
                            // Login failed
                            guna2MessageDialog1.Show("Invalid username or password");
                        }
                    }
                }
                catch (Exception ex)
                {
                    guna2MessageDialog1.Show($"Error: {ex.Message}");
                }
                finally
                {
                    con.Close();
                }
            }
        }


        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
