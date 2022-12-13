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
    public partial class Food_Page : Form
    {
        String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
        public Food_Page()
        {
            InitializeComponent();
        }
        private void Food_Page_Load(object sender, EventArgs e)
        {
            
            string sqlQuery = "SELECT * FROM REQUEST_ORDER";
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
            dt.Columns.Add("음식 이름", typeof(string));
            dt.Columns.Add("공급업체 이름", typeof(string));
            dt.Columns.Add("입고일", typeof(string));
            dt.Columns.Add("가격", typeof(string));
            dt.Columns.Add("주문량", typeof(string));
            dt.Columns.Add("유통기한", typeof(string));
            while (loginReader.Read())
            {
                dt.Rows.Add(loginReader.GetString(0), loginReader.GetString(1), loginReader.GetDateTime(2), loginReader.GetDecimal(3).ToString(), loginReader.GetDecimal(4).ToString(), loginReader.GetDateTime(5));

            }
            login_attempt.Close();
            dataGridView1.DataSource = dt;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;



            string sqlQuery1 = "SELECT * FROM FOOD";
            OracleConnection login_attempt1 = new OracleConnection(connInfo);
            OracleCommand loginCommand1 = new OracleCommand();
            loginCommand1.Connection = login_attempt1;
            loginCommand1.CommandText = sqlQuery1;
            login_attempt1.Open();
            OracleDataReader loginReader1;
            loginReader1 = loginCommand1.ExecuteReader();

            OracleDataAdapter oda1 = new OracleDataAdapter();
            oda1.SelectCommand = new OracleCommand(sqlQuery1);
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("음식 이름", typeof(string));
            dt1.Columns.Add("재고량", typeof(string));
            dt1.Columns.Add("상세정보", typeof(string));
            while (loginReader1.Read())
            {
                dt1.Rows.Add(loginReader1.GetString(0), loginReader1.GetDecimal(1).ToString(), loginReader1.GetString(2));
            }
            login_attempt1.Close();
            dataGridView2.DataSource = dt1;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


           
            string sqlQuery2 = "SELECT * FROM SUPPLIER";
            OracleConnection login_attempt2 = new OracleConnection(connInfo);
            OracleCommand loginComman2 = new OracleCommand();
            loginComman2.Connection = login_attempt2;
            loginComman2.CommandText = sqlQuery2;
            login_attempt2.Open();
            OracleDataReader loginReader2;
            loginReader2 = loginComman2.ExecuteReader();

            OracleDataAdapter oda2 = new OracleDataAdapter();
            oda2.SelectCommand = new OracleCommand(sqlQuery2);
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("공급업체 이름", typeof(string));
            dt2.Columns.Add("상세정보", typeof(string));
            while (loginReader2.Read())
            {
                dt2.Rows.Add(loginReader2.GetString(0),loginReader2.GetString(1));
            }
            login_attempt2.Close();
            dataGridView3.DataSource = dt2;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main_Page foodPage = new Main_Page();
            foodPage.Tag = this;
            foodPage.Show();
            this.Hide();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            string sqlQuery1 = "SELECT * FROM FOOD WHERE FAMOUNT<=4";
            OracleConnection login_attempt1 = new OracleConnection(connInfo);
            OracleCommand loginCommand1 = new OracleCommand();
            loginCommand1.Connection = login_attempt1;
            loginCommand1.CommandText = sqlQuery1;
            login_attempt1.Open();
            OracleDataReader loginReader1;
            loginReader1 = loginCommand1.ExecuteReader();

            OracleDataAdapter oda1 = new OracleDataAdapter();
            oda1.SelectCommand = new OracleCommand(sqlQuery1);
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("음식 이름", typeof(string));
            dt1.Columns.Add("재고량", typeof(string));
            dt1.Columns.Add("상세정보", typeof(string));
            while (loginReader1.Read())
            {
                dt1.Rows.Add(loginReader1.GetString(0), loginReader1.GetDecimal(1).ToString(), loginReader1.GetString(2));
            }
            login_attempt1.Close();
            dataGridView1.DataSource = dt1;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            string sqlQuery1 = "SELECT * FROM REQUEST_ORDER WHERE TO_DATE(EXP_DATE,'RRRR-MM-DD')-TO_DATE(SYSDATE,'RRRR-MM-DD')<=7";
            OracleConnection login_attempt1 = new OracleConnection(connInfo);
            OracleCommand loginCommand1 = new OracleCommand();
            loginCommand1.Connection = login_attempt1;
            loginCommand1.CommandText = sqlQuery1;
            login_attempt1.Open();
            OracleDataReader loginReader;
            loginReader = loginCommand1.ExecuteReader();

            OracleDataAdapter oda1 = new OracleDataAdapter();
            oda1.SelectCommand = new OracleCommand(sqlQuery1);
            DataTable dt = new DataTable();
            dt.Columns.Add("음식 이름", typeof(string));
            dt.Columns.Add("공급업체 이름", typeof(string));
            dt.Columns.Add("입고일", typeof(string));
            dt.Columns.Add("가격", typeof(string));
            dt.Columns.Add("주문량", typeof(string));
            dt.Columns.Add("유통기한", typeof(string));
            while (loginReader.Read())
            {
                dt.Rows.Add(loginReader.GetString(0), loginReader.GetString(1), loginReader.GetDateTime(2), loginReader.GetDecimal(3).ToString(), loginReader.GetDecimal(4).ToString(), loginReader.GetDateTime(5));

            }
            login_attempt1.Close();
            dataGridView1.DataSource = dt;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            string sqlQuery = "SELECT * FROM REQUEST_ORDER";
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
            dt.Columns.Add("음식 이름", typeof(string));
            dt.Columns.Add("공급업체 이름", typeof(string));
            dt.Columns.Add("입고일", typeof(string));
            dt.Columns.Add("가격", typeof(string));
            dt.Columns.Add("주문량", typeof(string));
            dt.Columns.Add("유통기한", typeof(string));
            while (loginReader.Read())
            {
                dt.Rows.Add(loginReader.GetString(0), loginReader.GetString(1), loginReader.GetDateTime(2), loginReader.GetDecimal(3).ToString(), loginReader.GetDecimal(4).ToString(), loginReader.GetDateTime(5));

            }
            login_attempt.Close();
            dataGridView1.DataSource = dt;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
    }
}
