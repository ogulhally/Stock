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
    public partial class Products : MetroFramework.Forms.MetroForm
    {
        public Products()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=del1;Integrated Security=True");


        private void Products_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
                //insert  add data
            con.Open();
            bool statuss = false;
            if (comboBox1.SelectedIndex == 0)
            {
                statuss = true;
            }
            else
            {
                statuss = false;
            }
            var sqlQuery = "";
            if (IfProductExist(con,textBox1.Text))//egerde bizizn barkodymyzdaky haryt on barbolsa update etmeli yokbolsa taze haryt goshmaly insert etmeli(gerek)
            {    //ustune update et
                sqlQuery = @"UPDATE [Products] SET [ProductName] = '" + textBox2.Text + "',[Status] = '" + statuss + "' WHERE [ProductCode] = '" + textBox1.Text + "'";
            }
            else
            {    //taze haryt gosh
                 sqlQuery = @"INSERT INTO [del1].[dbo].[Products]([ProductCode],[ProductName],[Status])
     VALUES
           ('" + textBox1.Text + "','" + textBox2.Text + "','" + statuss + "')";//inset or add data
            }
            SqlCommand cmd = new SqlCommand(sqlQuery, con);//egerde bizizn barkodymyzdaky haryt on barbolsa update etmeli yokbolsa taze haryt goshmaly insert etmeli(gerek)
            cmd.ExecuteNonQuery();
            con.Close();
            LoadData();

            textBox1.Clear();
            textBox2.Clear();
            textBox1.Focus();
            MessageBox.Show("New Product Added!");
        }
        private bool IfProductExist(SqlConnection con,string productCode)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select 1 From [Products] where [ProductCode]='"+ productCode + "'", con);//gozlejek id productCodumyzy belle we deneshdir
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)//deneshdir on bamy yokmy
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public void LoadData()
        {
            //reading data dataGridviewe datany yazdyrmak
            SqlDataAdapter sda = new SqlDataAdapter("Select * From [del1].[dbo].[Products]", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["ProductName"].ToString();
                if ((bool)item["Status"])
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Active";
                }
                else
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Deactive";
                }
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            comboBox1.SelectedIndex = 1;
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

            if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString() == "Active")
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)//for delete data if it have
        {
            var sqlQuery = "";
            if (IfProductExist(con, textBox1.Text))//egerde bizizn barkodymyzdaky haryt on barbolsa delet et yok bolsa mesagela ayt
            {    //delete data
                con.Open();
                sqlQuery = @"DELETE from [Products] WHERE [ProductCode] = '" + textBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);//egerde bizizn barkodymyzdaky haryt on barbolsa ony delete et(gerek)
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Product deleted!");
                textBox1.Clear();
                textBox2.Clear();
                textBox1.Focus();
            }
            else
            {
                MessageBox.Show("Product not exist!");
            }
           
            LoadData();//datagridviewe sonky netijani yazdyr
        }
    }
}
