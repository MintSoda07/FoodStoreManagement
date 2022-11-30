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
    public partial class Table_Menu : Form
    {
        public int table_count=21; //temp 값 (최대 36)
        public Table_Menu()
        {
            InitializeComponent();
            
        }

        private void Table_Menu_Load(object sender, EventArgs e)
        {
            int removed_table = groupBox1.Controls.Count - table_count;
            for(int i = 0; i < removed_table; i++)
            {
                groupBox1.Controls[i].Visible=false;
                
            }
            String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
            string sqlQuery = "SELECT * FROM MENU_ORDER_INTERGRATE";

            OracleConnection login_attempt = new OracleConnection(connInfo);
            OracleCommand loginCommand = new OracleCommand();
            loginCommand.Connection = login_attempt;
            loginCommand.CommandText = sqlQuery; login_attempt.Open();
            OracleDataReader loginReader;
            loginReader = loginCommand.ExecuteReader();

            OracleDataAdapter oda = new OracleDataAdapter();
            oda.SelectCommand = new OracleCommand(sqlQuery);
            DataTable dt = new DataTable();
            dt.Columns.Add("주문번호", typeof(int));
            dt.Columns.Add("주문일자", typeof(DateTime));
            dt.Columns.Add("배달 여부", typeof(string));
            dt.Columns.Add("주소", typeof(string));
            while (loginReader.Read())
            {
                dt.Rows.Add(loginReader.GetDecimal(0), loginReader.GetDateTime(1), loginReader.GetString(2), loginReader.GetString(3));
            }
            login_attempt.Close();
            dataGridView1.DataSource = dt;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button39_Click(object sender, EventArgs e)
        {
            Main_Page mp = new Main_Page();
            mp.Tag = this;
            mp.Show();
            this.Hide();
        }

        private void Table_Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void button40_Click(object sender, EventArgs e)
        {
            order_addPage mp = new order_addPage();
            mp.Tag = this;
            mp.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            String data=dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
            string sqlQuery = "SELECT A.MENU_NAME,A.AMOUNT,A.ORDER_CODE,B.PRICE,C.ORDER_TYPE FROM MENU_ORDER A JOIN MENU B ON A.MENU_NAME=B.MENU_NAME JOIN MENU_ORDER_INTERGRATE C ON A.ORDER_CODE=C.ORDER_CODE WHERE  A.ORDER_CODE='" + data+"'";

            OracleConnection login_attempt = new OracleConnection(connInfo);
            OracleCommand loginCommand = new OracleCommand();
            loginCommand.Connection = login_attempt;
            loginCommand.CommandText = sqlQuery; login_attempt.Open();
            OracleDataReader loginReader;
            loginReader = loginCommand.ExecuteReader();

            OracleDataAdapter oda = new OracleDataAdapter();
            oda.SelectCommand = new OracleCommand(sqlQuery);
            DataTable dt = new DataTable();
            dt.Columns.Add("주문번호", typeof(int));
            dt.Columns.Add("메뉴", typeof(string));
            dt.Columns.Add("가격", typeof(string));
            dt.Columns.Add("개수", typeof(int));
            dt.Columns.Add("총합", typeof(string));
            int summary = 0;
            while (loginReader.Read())
            {
                int item_sum = (int)(loginReader.GetDecimal(3) * loginReader.GetDecimal(1));
                dt.Rows.Add(loginReader.GetDecimal(2), loginReader.GetString(0),loginReader.GetDecimal(3).ToString()+"\\",loginReader.GetDecimal(1), item_sum.ToString() + "\\");
                if (loginReader.GetString(4).Equals("매장"))
                {
                    radioButton1.Checked = true;
                    radioButton2.Checked = false;
                }
                else
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = true;
                }summary += item_sum;
                
            }
            Label_totalCash.Text = "총 가격 :    " + summary.ToString() + "  \\";
           login_attempt.Close();
            dataGridView2.DataSource = dt;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
    }
}
