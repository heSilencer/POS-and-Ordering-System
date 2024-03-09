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

       
        private void Waiter_Load(object sender, EventArgs e)
        {

        }
    }
}
