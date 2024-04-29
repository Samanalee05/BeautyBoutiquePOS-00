using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BeautyBoutiquePOS_TransactionsPage.Views.User_Controls.Sub_Views.Payments
{
    public partial class Cash : Form
    {
        public decimal balance = 0;
        public decimal ammount = 0;
        private newCheckout NewCheckoutform;

        public Cash(decimal ammount, newCheckout newCheckout)
        {
            InitializeComponent();

            lblRs.Text = ammount.ToString();
            this.ammount = ammount;
            this.NewCheckoutform = newCheckout;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            text5000.Text = "";
            text1000.Text = "";
            text500.Text = "";
            text100.Text = "";
            text50.Text = "";
            text20.Text = "";


            label5000.Text = text5000.Text;
            label1000.Text = text1000.Text;
            label500.Text = text500.Text;
            label100.Text = text100.Text;
            label50.Text = text50.Text;
            label20.Text = text20.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label5000.Text = text5000.Text;
            label1000.Text = text1000.Text;
            label500.Text = text500.Text;
            label100.Text = text100.Text;
            label50.Text = text50.Text;
            label20.Text = text20.Text;

            decimal sum = 0;
            decimal value = 0;


            if (textBox2.Text == "")
            {
                if (!string.IsNullOrEmpty(textBox1.Text) && decimal.TryParse(textBox1.Text, out value))
                {
                    sum = value;
                }

                decimal value5000 = 0, value1000 = 0, value500 = 0, value100 = 0, value50 = 0, value20 = 0;

                if (!string.IsNullOrEmpty(text5000.Text) && decimal.TryParse(text5000.Text, out value5000))
                {
                    sum += value5000 * 5000;
                }
                if (!string.IsNullOrEmpty(text1000.Text) && decimal.TryParse(text1000.Text, out value1000))
                {
                    sum += value1000 * 1000;
                }
                if (!string.IsNullOrEmpty(text500.Text) && decimal.TryParse(text500.Text, out value500))
                {
                    sum += value500 * 500;
                }
                if (!string.IsNullOrEmpty(text100.Text) && decimal.TryParse(text100.Text, out value100))
                {
                    sum += value100 * 100;
                }
                if (!string.IsNullOrEmpty(text50.Text) && decimal.TryParse(text50.Text, out value50))
                {
                    sum += value50 * 50;
                }
                if (!string.IsNullOrEmpty(text20.Text) && decimal.TryParse(text20.Text, out value20))
                {
                    sum += value20 * 20;
                }
            } else
            {
                if (!string.IsNullOrEmpty(textBox2.Text) && decimal.TryParse(textBox2.Text, out value))
                {
                    sum = value;
                } 
            }

            if (sum == 0)
            {
                MessageBox.Show("No valid input found.");
            }
            else
            {
                balance = sum - ammount;
                this.NewCheckoutform.balance = balance;
                this.NewCheckoutform.cash = sum;

                this.NewCheckoutform.calculateTotalDiscount();
            }

            this.Close();
        }
    }
}
