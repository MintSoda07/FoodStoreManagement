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
    public partial class Table_Clicked : Form
    {
        public bool null_data = true;
        public Table_Clicked(int table_id)
        {
            InitializeComponent();
            this.Text = table_id + "번 테이블 주문 관리";
            int sum = 0;
            label1.Text = table_id + "번 테이블";
            OracleConnection login_attempt = null ;
            
            try
            {
                String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
                string sqlQuery = "SELECT ORDER_DATE,ORDER_CODE,INFO FROM MENU_ORDER_INTERGRATE WHERE ORDER_TABLE='" + table_id + "'";

                login_attempt = new OracleConnection(connInfo);
                OracleCommand loginCommand = new OracleCommand();
                loginCommand.Connection = login_attempt;
                loginCommand.CommandText = sqlQuery; login_attempt.Open();
                OracleDataReader loginReader;
                loginReader = loginCommand.ExecuteReader();

                OracleDataAdapter oda = new OracleDataAdapter();
                oda.SelectCommand = new OracleCommand(sqlQuery);
                DataTable dt = new DataTable();
                DateTime order_date = new DateTime();
                int order_CODE=0;
                string info;
                while (loginReader.Read())
                {
                    null_data = false;
                    order_date = loginReader.GetDateTime(0);
                    order_CODE = (int)loginReader.GetDecimal(1);
                    info = loginReader.GetString(2);
                }
                string menu_q = "SELECT DISTINCT A.MENU_NAME,A.AMOUNT,B.PRICE,B.AVERAGE_COOKING_TIME FROM MENU_ORDER A JOIN MENU B ON A.MENU_NAME = B.MENU_NAME WHERE ORDER_CODE = '" + order_CODE + "'";

                OracleConnection menu_conn = new OracleConnection(connInfo);
                OracleCommand menu_cmd = new OracleCommand();
                menu_cmd.Connection = menu_conn;
                menu_cmd.CommandText = menu_q; menu_conn.Open();
                OracleDataReader menu_reader;
                if (!null_data)
                {
                    menu_reader = menu_cmd.ExecuteReader();
                    while (menu_reader.Read())
                    {
                        sum += (int)menu_reader.GetDecimal(2);
                        ListViewItem li = new ListViewItem();
                        li.SubItems.Add(menu_reader.GetString(0));
                        li.SubItems.Add(menu_reader.GetInt32(1).ToString());
                        li.SubItems.Add(menu_reader.GetDecimal(2).ToString());
                        li.SubItems.Add(menu_reader.GetString(3));
                        listView1.Items.Add(li);
                    }
                    label2.Text=sum+"\\";
                    label4.Visible = false;
                    textBox2.Text = order_date.ToString();
                    textBox1.Text = order_CODE.ToString();
                }
                else
                {
                    button1.Enabled = false;
                }
                
            }catch(Exception all)
            {
                MessageBox.Show(all.StackTrace);
            }
            finally
            {
                login_attempt.Close();
            }
            
            
            
        }

        private void Table_Clicked_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }
    }
}
