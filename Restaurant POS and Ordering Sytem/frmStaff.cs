using Restaurant_POS_and_Ordering_Sytem.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_POS_and_Ordering_Sytem
{
    public partial class frmStaff : Form
    {
        public frmStaff()
        {
            InitializeComponent();
        }
        public void AddControls(Form f)
        {
            ControlsPanel.Controls.Clear();
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            ControlsPanel.Controls.Add(f);
            f.Show();
        }

        private void btnWaiter_Click(object sender, EventArgs e)
        {
            AddControls(new frmStaffView());
        }

        private void btnCashier_Click(object sender, EventArgs e)
        {
            AddControls(new frmStaffCategoryView());
        }
    }
}
