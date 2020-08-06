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


namespace del1
{
    public partial class LogIn : MetroFramework.Forms.MetroForm
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Clear();
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=del1;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * FROM[del1].[dbo].[Login] Where UserName = '"+textBox1.Text+"' and Password = '"+textBox2.Text+"'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                this.Hide();
                //check username and password
                StockMain main = new StockMain();
                main.Show();
            }
            else
            {
                MessageBox.Show("yalnysh!","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                button1_Click(sender, e);//textboxlary arassalayan knopganyn funksiyasyny cagyryldy
            }
        }
    }
}
