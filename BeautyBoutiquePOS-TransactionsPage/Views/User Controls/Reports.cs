using BeautyBoutiquePOS_TransactionsPage.Class;
using BeautyBoutiquePOS_TransactionsPage.Views.User_Controls.Sub_Views.Payments;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;



namespace BeautyBoutiquePOS_TransactionsPage.Views.User_Controls
{
    public partial class Reports : UserControl
    {
        public Reports()
        {
            InitializeComponent();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            newReport  reports  = new newReport();
            reports.ShowDialog();

        }
    }
}
