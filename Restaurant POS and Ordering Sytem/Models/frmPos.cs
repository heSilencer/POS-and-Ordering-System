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
    }

}
