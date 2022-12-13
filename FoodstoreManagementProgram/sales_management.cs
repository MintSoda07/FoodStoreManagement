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
    public partial class sales_management : Form
    {
        public sales_management()
        {
            InitializeComponent();
            sale();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main_Page mp = new Main_Page();
            mp.Tag = this;
            mp.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
            string sqlQuery = "SELECT a.MENU_NAME,a.PRICE,b.AMOUNT,b.ORDER_DATE FROM MENU a JOIN ORDER_COMPLETE_MENU b ON a.MENU_NAME=b.MENU_NAME WHERE extract(month from b.ORDER_DATE)='"+(comboBox1.SelectedIndex+1)+"'";
            OracleConnection login_attempt = new OracleConnection(connInfo);
            OracleCommand loginCommand = new OracleCommand();
            loginCommand.Connection = login_attempt;
            loginCommand.CommandText = sqlQuery;
            login_attempt.Open();
            OracleDataReader loginReader;
            loginReader = loginCommand.ExecuteReader();

            OracleDataAdapter oda = new OracleDataAdapter();
            oda.SelectCommand = new OracleCommand(sqlQuery);
            DataTable dt = new DataTable();
            dt.Columns.Add("메뉴", typeof(string));
            dt.Columns.Add("가격", typeof(int));
            dt.Columns.Add("수량", typeof(int));
            dt.Columns.Add("날짜", typeof(string));
            int summary = 0;
            while (loginReader.Read())
            {
                dt.Rows.Add(loginReader.GetString(0), (int)loginReader.GetDecimal(1), (int)loginReader.GetDecimal(2), loginReader.GetDateTime(3).ToString("yyyy년 MM월 dd일"));
                summary += (int)loginReader.GetDecimal(1) * (int)loginReader.GetDecimal(2);
            }
            // 라벨 업데이트
            textBox2.Text = summary + "원";
            login_attempt.Close();
            dataGridView1.DataSource = dt;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        public void sale()
        {
            try
            {
                String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
                string sqlQuery = "SELECT a.PRICE,b.AMOUNT FROM MENU a JOIN ORDER_COMPLETE_MENU b ON a.MENU_NAME=b.MENU_NAME";
                OracleConnection login_attempt = new OracleConnection(connInfo);
                OracleCommand loginCommand = new OracleCommand();
                loginCommand.Connection = login_attempt;
                loginCommand.CommandText = sqlQuery;
                login_attempt.Open();
                OracleDataReader loginReader;
                loginReader = loginCommand.ExecuteReader();
                int sum = 0;
                while (loginReader.Read())
                {
                    sum += (int)loginReader.GetDecimal(0) * (int)loginReader.GetDecimal(1);
                }
                // 라벨 업데이트
                textBox1.Text = sum + "원";
            }catch(Exception HeLLO)
            {
                MessageBox.Show(HeLLO.Message + "," + HeLLO.StackTrace);
            }
        }
    }
    }
