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

namespace BeautyBoutiquePOS_TransactionsPage.Views.User_Controls.Report_Views
{
    public partial class inventoryReportView : Form
    {
        public inventoryReportView()
        {
            InitializeComponent();

            reportViewer1.ZoomMode = ZoomMode.PageWidth;
        }

        private void inventoryReportView_Load(object sender, EventArgs e) // load data to inventory data report 
        {

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSetProducts", mysql.Data($"select * from products", "products").Tables[0]));
            reportViewer1.LocalReport.ReportPath = @"Reports\\Inventory.rdlc";
            reportViewer1.RefreshReport();
        }
    }
}
