using Restaurant_POS_and_Ordering_Sytem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_POS_and_Ordering_Sytem.View
{
    public partial class frmAddUserView : SampleView
    {
        public frmAddUserView()
        {
            InitializeComponent();
        }
        public override void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddUser adduser= new frmAddUser();
            //addSatffForm.StaffUpdated += FrmStaffAdd_staffUpdated; // Subscribe to the event
            adduser.ShowDialog();

        }
    }
}
