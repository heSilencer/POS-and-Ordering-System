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
namespace Restaurant_POS_and_Ordering_Sytem.View
{
    public partial class frmdashboardView : Form
    {
        private readonly MySqlConnection con;
        public frmdashboardView()
        {
            InitializeComponent();
            con = new MySqlConnection(@"server=localhost;database=pos_ordering_system;userid=root;password=;");
        }

        private int GetTotalProducts()
        {
            int totalProducts = 0;

            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "SELECT COUNT(*) FROM tbl_products"; // Replace with your actual table name

                try
                {
                    con.Open();
                    totalProducts = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
                finally
                {
                    con.Close();
                }
            }

            return totalProducts;
        }
        private int GetTotalCategories()
        {
            int totalCategory = 0;

            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandText = "SELECT COUNT(*) FROM tbl_category"; // Replace with your actual table name

                try
                {
                    con.Open();
                    totalCategory = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
                finally
                {
                    con.Close();
                }
            }

            return totalCategory;
        }



        private void frmdashboardView_Load(object sender, EventArgs e)
        {
            int totalProducts = GetTotalProducts();
            int totalCategory = GetTotalCategories();


            // Display the total number of products in the label
            lblproducts.Text = $" {totalProducts}";
            lblcategories.Text = $" {totalCategory}";

        }
    }
}
