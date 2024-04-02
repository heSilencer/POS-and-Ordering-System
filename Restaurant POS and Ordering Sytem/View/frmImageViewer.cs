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
    public partial class frmImageViewer : Form
    {
        private Image image;

        public frmImageViewer(Image image)
        {
            InitializeComponent();

            this.image = image;
            ZoomProfilePic.Image = image;
        }

        private void ZoomProfilePic_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
