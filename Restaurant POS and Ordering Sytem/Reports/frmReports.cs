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
            var Ml = new MenuList();
            MainClass.BlurbackGround(Ml);
        }

        private void btnStaffList_Click(object sender, EventArgs e)
        {
            var SL = new StaffList();
            MainClass.BlurbackGround(SL);
        }

        private void btnSalesbyCashier_Click(object sender, EventArgs e)
        {
            var SBC = new SalesByCashier();
            MainClass.BlurbackGround(SBC);
        }

        private void btnSalesbyCategory_Click(object sender, EventArgs e)
        {
            var SBC = new SalesByCategory();
            MainClass.BlurbackGround(SBC);
        }
    }
}
