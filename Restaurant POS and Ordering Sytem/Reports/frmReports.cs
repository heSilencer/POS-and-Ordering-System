using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_POS_and_Ordering_Sytem.Reports
{
    public partial class frmReports : Form
    {
        public frmReports()
        {
            InitializeComponent();
        }

        private void btnMenuList_Click(object sender, EventArgs e)
        {
            MenuList Ml = new MenuList();
            Ml.Show();
        }
    }
}
