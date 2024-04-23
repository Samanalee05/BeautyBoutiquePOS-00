using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeautyBoutiquePOS_TransactionsPage.Views.User_Controls.Sub_Views.Payments
{
    public partial class Card : Form
    {
        public Card(decimal ammount)
        {
            InitializeComponent();

            lblRs.Text = ammount.ToString();
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textAmount.ReadOnly = true;

            textAmount.Text = ammount.ToString();
        }
    }
}
