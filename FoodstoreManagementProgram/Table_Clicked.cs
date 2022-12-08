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
        public Table_Clicked(int table_id)
        {
            InitializeComponent();
            String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
            string sqlQuery = "SELECT ORDER_DATE,ORDER_CODE,INFO FROM MENU_ORDER_INTERGRATE";

            OracleConnection login_attempt = new OracleConnection(connInfo);
            OracleCommand loginCommand = new OracleCommand();
            loginCommand.Connection = login_attempt;
            loginCommand.CommandText = sqlQuery; login_attempt.Open();
            OracleDataReader loginReader;
            loginReader = loginCommand.ExecuteReader();


            OracleDataAdapter oda = new OracleDataAdapter();
            oda.SelectCommand = new OracleCommand(sqlQuery);
            DataTable dt = new DataTable();
            DateTime order_date =null;
            int order_CODE = 0;
            string info = "ERROR";
            while (loginReader.Read())
            {
                order_date = loginReader.GetDateTime(0).ToString();
                order_CODE = loginReader.GetInt32(1);
                info = loginReader.GetString(2);
            }
            string menu_q = "SELECT DISTINCT A.MENU_NAME,A.AMOUNT,B.PRICE,B.AVERAGE_COOKING_TIME FROM MENU_ORDER A JOIN MENU B ON A.MENU_NAME = B.MENU_NAME WHERE ORDER_CODE = '"+order_CODE+"'";

            OracleConnection menu_conn = new OracleConnection(connInfo);
            OracleCommand menu_cmd = new OracleCommand();
            menu_cmd.Connection = menu_conn;
            menu_cmd.CommandText = menu_q; menu_conn.Open();
            OracleDataReader menu_reader;
            menu_reader = menu_cmd.ExecuteReader();
            while (menu_reader.Read())
            {
                ListViewItem li = new ListViewItem();
                li.SubItems.Add(menu_reader.GetString(0));
                li.SubItems.Add(menu_reader.GetInt32(0).ToString());
                listView1.Items.Add(li);
            }
            login_attempt.Close();
            
        }

        private void Table_Clicked_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
