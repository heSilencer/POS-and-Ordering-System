using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_POS_and_Ordering_Sytem.RecieptPrint
{
    public partial class frmPrint : Form
    {
        private double change;
        private int mainID;
        private List<OrderDetail> orderDetails;
        private double receivedAmount;
        private double totalAmount;

        public frmPrint()
        {
            InitializeComponent();
        }

        public frmPrint(double totalAmount, double receivedAmount, double change, List<OrderDetail> orderDetails, int mainID)
        {
            this.totalAmount = totalAmount;
            this.receivedAmount = receivedAmount;
            this.change = change;
            this.orderDetails = orderDetails;
            this.mainID = mainID;
        }
    }
}
