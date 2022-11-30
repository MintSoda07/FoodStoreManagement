using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace FoodstoreManagementProgram
{
    public partial class order_addPage : Form
    {
        public order_addPage()
        {
            InitializeComponent();
        }

        private void order_addPage_Load(object sender, EventArgs e)
        {
            String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
            string sqlQuery = "SELECT * FROM MENU";

            OracleConnection login_attempt = new OracleConnection(connInfo);
            OracleCommand loginCommand = new OracleCommand();
            loginCommand.Connection = login_attempt;
            loginCommand.CommandText = sqlQuery; login_attempt.Open();
            OracleDataReader loginReader;
            loginReader = loginCommand.ExecuteReader();

            OracleDataAdapter oda = new OracleDataAdapter();
            oda.SelectCommand = new OracleCommand(sqlQuery);
            DataTable dt = new DataTable();
            dt.Columns.Add("메뉴", typeof(string));
            dt.Columns.Add("가격", typeof(string));
            dt.Columns.Add("조리시간", typeof(string));
            while (loginReader.Read())
            {
                dt.Rows.Add(loginReader.GetString(0), loginReader.GetDecimal(1).ToString(), loginReader.GetString(3));
            }
            login_attempt.Close();
            dataGridView1.DataSource = dt;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox4.Text = "1";
        }
        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox4.Text.Equals(""))
            {

            }
            else
            {
                try
                {
                    textBox5.Text = (Int32.Parse(textBox3.Text) * Int32.Parse(textBox4.Text)).ToString();
                }
                catch (FormatException error)
                {
                    MessageBox.Show("숫자를 입력해야 합니다.");
                    textBox4.Text = "1";
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "없음" || textBox4.Text == "0")
            {
                MessageBox.Show("유효한 아이템을 선택해야 합니다.");
            }
            else
            {
                ListViewItem li = new ListViewItem();
                li.Text = textBox2.Text;
                li.SubItems.Add(textBox4.Text);
                li.SubItems.Add(textBox5.Text);
                listView1.Items.Add(li);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }
    }
}
