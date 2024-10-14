using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinQ
{
    public partial class Form1 : Form
    {
        private static string CONNECTIONSTRING= @"Data Source=DESKTOP-FQU8NOI;Initial Catalog=master;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = this.GetAllSanPham().Tables[0];
        }
        private DataSet GetAllSanPham()
        {
            DataSet dataSet = new DataSet();
            string query = "select * from SANPHAM";
            using (SqlConnection connection = new SqlConnection(Form1.CONNECTIONSTRING))
            {
                connection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, connection);
                sqlDataAdapter.Fill(dataSet);
                connection.Close();
            }
            return dataSet;
        }
        private DataSet XoaSanPham(int index)
        {
            DataSet dataSet = new DataSet();
            string query = "delete from SANPHAM where MASANPHAM=SP0"+index;
            using (SqlConnection connection = new SqlConnection(Form1.CONNECTIONSTRING))
            {
                connection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, connection);
                sqlDataAdapter.Fill(dataSet);
                connection.Close();
            }
            return dataSet;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DataSet dataSet = new DataSet();
            string query = "insert into SANPHAM values('"+textBox1.Text
                +"','"+textBox2.Text
                +"','"+int.Parse( textBox3.Text)
                +"','"+int.Parse(textBox4.Text)
                +"','"+textBox5.Text
                +"','"+textBox6.Text+"')";
            using(SqlConnection connection=new SqlConnection(Form1.CONNECTIONSTRING))
            {
                connection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, connection);
                try
                {
                    sqlDataAdapter.Fill(dataSet);

                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
                connection.Close();
            }
            dataGridView1.DataSource = this.GetAllSanPham().Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy chỉ số dòng được chọn
                int rowIndex = dataGridView1.SelectedRows[0].Index;

                // Xác nhận trước khi xóa
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa dòng này?", "Xác nhận", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {

                    // Xóa dòng
                    dataGridView1.DataSource = this.XoaSanPham(rowIndex + 1);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.");
            }
        }
    }
}
