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
    public partial class Manage_Menu : Form
    {
        public Manage_Menu()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main_Page mp = new Main_Page();
            mp.Tag = this;
            mp.Show();
            this.Hide();
        }

        private void Manage_Menu_Load(object sender, EventArgs e)
        {
            String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
            string sqlQuery = "SELECT * FROM CLIENT WHERE WORKPLACE=" + Program.SERIAL;
            
            OracleConnection login_attempt = new OracleConnection(connInfo);
            OracleCommand loginCommand = new OracleCommand();
            loginCommand.Connection = login_attempt;
            loginCommand.CommandText = sqlQuery; login_attempt.Open();
            OracleDataReader loginReader;
            loginReader = loginCommand.ExecuteReader();

            OracleDataAdapter oda = new OracleDataAdapter();
            oda.SelectCommand = new OracleCommand(sqlQuery);
            DataTable dt = new DataTable();
            dt.Columns.Add("성명", typeof(string));
            dt.Columns.Add("직급", typeof(string));
            dt.Columns.Add("입사일", typeof(string));
            dt.Columns.Add("직원코드", typeof(string));
            dt.Columns.Add("나이", typeof(string));
            dt.Columns.Add("생년월일", typeof(string));
            dt.Columns.Add("성별", typeof(string));
            dt.Columns.Add("급여", typeof(string));
            dt.Columns.Add("상세정보", typeof(string));
            dt.Columns.Add("사진 경로", typeof(string));
            while (loginReader.Read())
            {
                dt.Rows.Add(loginReader.GetString(1), loginReader.GetString(2), loginReader.GetDateTime(3), loginReader.GetInt64(0),loginReader.GetInt64(4), loginReader.GetDateTime(5), loginReader.GetString(6), loginReader.GetInt64(7), loginReader.GetString(8));
            }
            login_attempt.Close();
            dataGridView1.DataSource = dt;
            dataGridView1.SelectionMode =  DataGridViewSelectionMode.FullRowSelect;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
