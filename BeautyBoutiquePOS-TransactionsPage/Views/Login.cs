using BeautyBoutiquePOS_TransactionsPage.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeautyBoutiquePOS_TransactionsPage.Views
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            UserControlStyles styles = new UserControlStyles();
            styles.ApplyTextBoxStyles(textBox1);
            styles.ApplyTextBoxStyles(textBox2);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Main mainForm = new Main(this);
            mainForm.ShowDialog();
            this.Hide();
        }
    }
}
