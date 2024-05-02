using BeautyBoutiquePOS_TransactionsPage.Class;
using System;
using Microsoft.Reporting.WinForms;
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
    public partial class salesDataTodayReportViewer : Form
    {
        public salesDataTodayReportViewer()
        {
            InitializeComponent();
            reportViewer1.ZoomMode = ZoomMode.PageWidth;
                
        }

        private void salesDataTodayReportViewer_Load(object sender, EventArgs e) // load data to checkoutDataToday report
        {

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", mysql.Data($"SELECT * FROM checkoutLine WHERE DATE(date) = CURDATE() ORDER BY date", "checkoutLine").Tables[0]));
            reportViewer1.LocalReport.ReportPath = @"Reports\\salseToday.rdlc";
            reportViewer1.RefreshReport();
        }
    }
}
