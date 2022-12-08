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
            int summary = 0;
            if (textBox2.Text == "없음" || textBox4.Text == "0")
            {
                MessageBox.Show("유효한 메뉴를 선택해야 합니다.");
            }
            else
            {
                bool is_exist = false;
                foreach (ListViewItem item in listView1.Items)
                {
                    if (item.Text.Equals(textBox2.Text))
                    {
                        item.SubItems[1].Text = "" + (Int32.Parse(item.SubItems[1].Text) + Int32.Parse(textBox4.Text));
                        item.SubItems[2].Text = "" + (Int32.Parse(item.SubItems[1].Text) * Int32.Parse(textBox3.Text));
                        is_exist = true;
                    }
                }
                if (is_exist == false)
                {
                    ListViewItem li = new ListViewItem();
                    li.Text = textBox2.Text;
                    li.SubItems.Add(textBox4.Text);
                    li.SubItems.Add(textBox5.Text);
                    listView1.Items.Add(li);
                }
                foreach (ListViewItem item in listView1.Items)
                {
                    summary += Int32.Parse(item.SubItems[2].Text);
                }
                textBox6.Text = summary+"";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int summary = 0;
            listView1.SelectedItems[0].Remove();
            foreach (ListViewItem item in listView1.Items)
            {
                summary += Int32.Parse(item.SubItems[2].Text);
            }
            textBox6.Text = summary + "";
        }

        public String type = "배달주문";
        public int table_num = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                    int val = Int32.Parse(textBox8.Text);
            }
            catch (Exception er)
            {
                MessageBox.Show("테이블 수에 올바른 값을 입력해야 합니다.");
                textBox8.Focus();
            }
            try
            {
                if (listView1.Items.Count==0)
                {
                    MessageBox.Show("선택한 메뉴가 존재하지 않습니다.");
                }else if (textBox1.Text == ""&&radioButton1.Checked) {
                    MessageBox.Show("주소를 입력해야 합니다.");
                    textBox1.Focus();
                }
                else
                {
                    DateTime myDateTime = DateTime.Now;
                    string str_table = textBox8.Text;
                    String sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    String address = "테이블주문";
                    String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
                    if (type == "배달주문")
                    {
                        address = textBox1.Text;
                        str_table = "X";
                    }
                    String more_info = "없음";
                    if (textBox7.Text != null || textBox7.Text != "")
                    {
                        more_info = textBox7.Text;
                    }
                    String sql_insert = "INSERT INTO MENU_ORDER_INTERGRATE VALUES(ORDER_CODE_SEQ.NEXTVAL,TO_DATE('" + sqlFormattedDate + "','YYYY-MM-DD HH24:MI:SS'),'"+type+"','"+ str_table + "','" + Program.SERIAL + "','" + address + "','"+ more_info+ "')";
                    OracleConnection Order_Conn = new OracleConnection(connInfo);
                    Order_Conn.Open();
                    OracleCommand Order_Cmd = new OracleCommand();
                    Order_Cmd.Connection = Order_Conn;
                    Order_Cmd.CommandText = sql_insert;
                    Order_Cmd.ExecuteNonQuery();

                    OracleCommand Order_Cmd2 = new OracleCommand();
                    foreach (ListViewItem item in listView1.Items)
                    {
                        Order_Cmd2.Connection = Order_Conn;
                        Order_Cmd2.CommandText = "INSERT INTO MENU_ORDER VALUES('" + item.SubItems[0].Text + "','" + item.SubItems[1].Text + "',ORDER_CODE_SEQ.CURRVAL,TO_DATE('" + sqlFormattedDate + "','YYYY-MM-DD HH24:MI:SS'))";
                        Order_Cmd2.ExecuteNonQuery();
                    }
                    Order_Conn.Close();
                    MessageBox.Show("주문이 완료되었습니다.");
                    Table_Menu mp = new Table_Menu();
                    mp.Tag = this;
                    mp.Show();
                    this.Hide();
                }

            }
            catch (Exception error)
            {
                MessageBox.Show("연결에 실패했습니다. 다음에 다시 시도해 주세요."+error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label12.Text = "테이블";
            textBox1.ReadOnly = true;
            textBox8.ReadOnly = false;
            type = "테이블주문";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label12.Text = "배달";
            textBox1.ReadOnly = false;
            textBox8.ReadOnly = true;
            type = "배달주문";
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox8.Text != null)
                {
                    int val = Int32.Parse(textBox8.Text);
                    if (val < 1 || val > 36)
                    {
                        MessageBox.Show("테이블 수는 1~36까지만 가능합니다.");
                        textBox8.Text = 1 + "";
                    }
                }
                
            }catch(Exception er)
            {
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
