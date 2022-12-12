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
        public DataTable dt = new DataTable();
        private void Table_Menu_Load(object sender, EventArgs e)
        {
            dt.Reset();
            int removed_table = groupBox1.Controls.Count - table_count;
            for(int i = 0; i < removed_table; i++)
            {
                groupBox1.Controls[i].Visible=false;
            }
            for (int j = 36; j > 0; j--)
            {
                String connInfo2 = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
                string sqlQuery2 = "SELECT ORDER_DATE,ORDER_CODE FROM MENU_ORDER_INTERGRATE WHERE ORDER_TABLE='"+j + "'";

                OracleConnection table_data_conn = new OracleConnection(connInfo2);
                OracleCommand table_data_command = new OracleCommand();
                table_data_command.Connection = table_data_conn;
                table_data_command.CommandText = sqlQuery2; table_data_conn.Open();
                OracleDataReader table_data_reader;
                table_data_reader = table_data_command.ExecuteReader();

                OracleDataAdapter oda2 = new OracleDataAdapter();
                oda2.SelectCommand = new OracleCommand(sqlQuery2);
                while (table_data_reader.Read())
                {
                    try
                    {
                        if ((int)table_data_reader.GetDecimal(1)>0)
                        {
                            groupBox1.Controls[36 - j].BackColor = Color.Wheat;
                        }
                    }
                    catch(Exception allErr)
                    {
                    }
                    

                }
                table_data_conn.Close();
                groupBox1.Controls[36-j].Text = j+"";
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
            
            dt.Columns.Add("주문번호", typeof(int));
            dt.Columns.Add("주문일자", typeof(DateTime));
            dt.Columns.Add("배달 여부", typeof(string));
            dt.Columns.Add("주소", typeof(string));
            dt.Columns.Add("테이블 번호", typeof(string));
            dt.Columns.Add("요청사항", typeof(string));
            while (loginReader.Read())
            {
                String val = "X";
                if (loginReader.GetString(3) != "0")
                {
                    val = loginReader.GetString(3);
                }
                dt.Rows.Add(loginReader.GetDecimal(0), loginReader.GetDateTime(1), loginReader.GetString(2), loginReader.GetString(5), val, loginReader.GetString(6));
            }
            
            login_attempt.Close();
            dataGridView1.DataSource = dt;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
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
            order_addPage mp = new order_addPage(this);
            mp.Tag = this;
            mp.ShowDialog();
        }
        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            String data=dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
            string sqlQuery = "SELECT A.MENU_NAME,A.AMOUNT,A.ORDER_CODE,B.PRICE,C.ORDER_TYPE,C.ORDER_DATE,C.INFO,C.MORE_INFO,ORDER_TABLE FROM MENU_ORDER A JOIN MENU B ON A.MENU_NAME=B.MENU_NAME JOIN MENU_ORDER_INTERGRATE C ON A.ORDER_CODE=C.ORDER_CODE WHERE  A.ORDER_CODE='" + data+"'";
            OracleConnection login_attempt = new OracleConnection(connInfo);
            OracleCommand loginCommand = new OracleCommand();
            loginCommand.Connection = login_attempt;
            loginCommand.CommandText = sqlQuery; login_attempt.Open();
            OracleDataReader loginReader;
            loginReader = loginCommand.ExecuteReader();

            OracleDataAdapter oda = new OracleDataAdapter();
            oda.SelectCommand = new OracleCommand(sqlQuery);
            DataTable dat = new DataTable();
            dat.Columns.Add("주문번호", typeof(int));
            dat.Columns.Add("메뉴", typeof(string));
            dat.Columns.Add("가격", typeof(string));
            dat.Columns.Add("개수", typeof(int));
            dat.Columns.Add("총합", typeof(string));
            int summary = 0;
            while (loginReader.Read())
            {
                int item_sum = (int)(loginReader.GetDecimal(3) * loginReader.GetDecimal(1));
                dat.Rows.Add(loginReader.GetDecimal(2), loginReader.GetString(0),loginReader.GetDecimal(3).ToString()+"\\",loginReader.GetDecimal(1), item_sum.ToString() + "\\");
                textBox1.Text = loginReader.GetDateTime(5).ToString();
                textBox3.Text = loginReader.GetString(7);
                if (loginReader.GetString(4).Equals("테이블주문"))
                {
                    radioButton1.Checked = true;
                    radioButton2.Checked = false;
                    textBox2.Text = loginReader.GetString(8);
                }
                else
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = true;
                    textBox2.Text = loginReader.GetString(6);
                }
                summary += item_sum;
                
            }
            Label_totalCash.Text = "총 가격 :    " + summary.ToString() + "  \\";
           login_attempt.Close();
            dataGridView2.DataSource = dat;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void TABLE_SEAT(object sender, EventArgs e)
        {
            Button click_btn = (Button)sender;
            Table_Clicked form2 = new Table_Clicked(Int32.Parse(click_btn.Text));
            form2.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            String data = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            if (data == null)
            {
                MessageBox.Show("항목을 선택해야 합니다.");
            }
            else
            {
                try
                {
                    string connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
                    String sql_insert = "DELETE FROM MENU_ORDER WHERE ORDER_CODE='"+ data+"'";
                    String sql_insert2 = "DELETE FROM MENU_ORDER_INTERGRATE WHERE ORDER_CODE='"+ data+"'";
                    OracleConnection Order_Conn = new OracleConnection(connInfo);
                    Order_Conn.Open();
                    OracleCommand Order_Cmd = new OracleCommand();
                    OracleCommand Order_Cmd2 = new OracleCommand();
                    Order_Cmd.Connection = Order_Conn;
                    Order_Cmd.CommandText = sql_insert;
                    Order_Cmd2.Connection = Order_Conn;
                    Order_Cmd2.CommandText = sql_insert2;
                    Order_Cmd.ExecuteNonQuery();
                    Order_Cmd2.ExecuteNonQuery();
                    MessageBox.Show("[주문코드:"+data+"] 를 취소하였습니다.");
                    dt.Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
                    force_refresh();
                    this.Update();
                }
                catch (Exception errrr)
                {
                    MessageBox.Show("삭제에 실패했습니다.");
                    throw (errrr);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            String data = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            if (data == null)
            {
                MessageBox.Show("항목을 선택해야 합니다.");
            }
            else
            {
                try
                {
                    string connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
                    String sql_insert = "DELETE FROM MENU_ORDER WHERE ORDER_CODE='" + data + "'";
                    String sql_insert2 = "DELETE FROM MENU_ORDER_INTERGRATE WHERE ORDER_CODE='" + data + "'";
                    String sql_insert3 = "INSERT INTO ORDER_COMPLETE SELECT * FROM MENU_ORDER_INTERGRATE WHERE ORDER_CODE='" + data + "'";
                    String sql_insert4 = "INSERT INTO ORDER_COMPLETE_MENU SELECT * FROM MENU_ORDER WHERE ORDER_CODE='" + data + "'";
                    OracleConnection Order_Conn = new OracleConnection(connInfo);
                    Order_Conn.Open();
                    OracleCommand Order_Cmd = new OracleCommand();
                    OracleCommand Order_Cmd2 = new OracleCommand();
                    OracleCommand Order_Cmd3 = new OracleCommand();
                    OracleCommand Order_Cmd4 = new OracleCommand();

                    Order_Cmd.Connection = Order_Conn;
                    Order_Cmd.CommandText = sql_insert;

                    Order_Cmd2.Connection = Order_Conn;
                    Order_Cmd2.CommandText = sql_insert2;

                    Order_Cmd3.Connection = Order_Conn;
                    Order_Cmd3.CommandText = sql_insert3;

                    Order_Cmd4.Connection = Order_Conn;
                    Order_Cmd4.CommandText = sql_insert4;

                    Order_Cmd3.ExecuteNonQuery();
                    Order_Cmd4.ExecuteNonQuery();
                    Order_Cmd.ExecuteNonQuery();
                    Order_Cmd2.ExecuteNonQuery();
                    MessageBox.Show("[주문코드:" + data + "] 가 완료되었습니다.");
                    dt.Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
                    force_refresh();
                }
                catch (Exception errrr)
                {
                    MessageBox.Show("완료에 실패했습니다.");
                    throw (errrr);
                }
            }
        }
        public void force_refresh()
        {
            this.Close();
            Table_Menu mp = new Table_Menu();
            mp.Tag = this;
            mp.Show();
        }
       
    }
}
