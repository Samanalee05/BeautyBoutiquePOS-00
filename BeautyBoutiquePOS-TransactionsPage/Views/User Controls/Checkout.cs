using BeautyBoutiquePOS_TransactionsPage.Views.User_Controls.Sub_Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeautyBoutiquePOS_TransactionsPage.Views.User_Controls
{
    public partial class Checkout : UserControl
    {
        public Checkout()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            newCheckout checkoutForm = new newCheckout();
            checkoutForm.ShowDialog();
        }
    }
}
