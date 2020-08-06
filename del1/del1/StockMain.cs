using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace del1
{
    public partial class StockMain : MetroFramework.Forms.MetroForm
    {
            public StockMain()
        {
            InitializeComponent();
        }

        private void StockMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();//applicationden cykyar
        }

        private void harytlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Products pro = new Products();
            pro.MdiParent = this;//MDI FOrmyn icinde pro formy goyyar
            pro.Show();
        }
    }
}
