using BeautyBoutiquePOS_TransactionsPage.Class;
using Microsoft.Reporting.WinForms;
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
    public partial class newReport : Form
    {
        public newReport()
        {
            InitializeComponent();


            reportViewer1.ZoomMode = ZoomMode.PageWidth;
        }

        private void newReport_Load(object sender, EventArgs e)
        {
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Checkout",mysql.Data($"select * from checkout", "checkout").Tables[0]));
            reportViewer1.LocalReport.ReportPath = $"C:\\Users\\samanla\\Documents\\GitHub\\BeautyBoutiquePOS-00\\BeautyBoutiquePOS-TransactionsPage\\Reports\\Checkout.rdlc";
            reportViewer1.RefreshReport();
               
        }
    }
}
