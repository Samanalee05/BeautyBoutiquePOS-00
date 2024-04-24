using BeautyBoutiquePOS_TransactionsPage.Class;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BeautyBoutiquePOS_TransactionsPage.Views.User_Controls.Sub_Views
{
    public partial class editForm : Form
    {
        private Inventory inventory1;
        private double oldQty;

        public editForm(Inventory inventory, string[] rowDataArray)
        {
            this.inventory1 = inventory;
            InitializeComponent();

            textBox1.ReadOnly = true;

            if (rowDataArray.Length > 0)
                textBox1.Text = rowDataArray[1]; 
            if (rowDataArray.Length > 1)
                textBoxQty.Text = rowDataArray[2];
            if (rowDataArray.Length > 2)
                textBoxSellingPrice.Text = rowDataArray[4];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double parsedValue;

            if (double.TryParse(textBoxQty.Text, out parsedValue))
            {
                oldQty = parsedValue;
            }


        }

        private void grForm_Load(object sender, EventArgs e)
        {

        }

    }
}
