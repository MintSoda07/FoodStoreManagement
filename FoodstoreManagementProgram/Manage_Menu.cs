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
            string sqlQuery = "SELECT NAME,RANK,HIRED_DATE FROM CLIENT WHERE WORKPLACE=" + Program.SERIAL;
            
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
            while (loginReader.Read())
            {
                dt.Rows.Add(loginReader.GetString(0), loginReader.GetString(1), loginReader.GetDateTime(2));
            }
            login_attempt.Close();
            dataGridView1.DataSource = dt;
            dataGridView1.SelectionMode =  DataGridViewSelectionMode.FullRowSelect;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            String data = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
            string sqlQuery = "SELECT * FROM CLIENT WHERE NAME='"+data+"'";
            OracleConnection login_attempt = new OracleConnection(connInfo);
            OracleCommand loginCommand = new OracleCommand();
            loginCommand.Connection = login_attempt;
            loginCommand.CommandText = sqlQuery; login_attempt.Open();
            OracleDataReader loginReader;
            loginReader = loginCommand.ExecuteReader();
            while (loginReader.Read())
            {
                label1.Text = loginReader.GetString(1);
                label3.Text = loginReader.GetString(2);
                label2.Text = loginReader.GetDateTime(3).ToString("yyyy년 MM월 dd일");
                label10.Text = (int)loginReader.GetDecimal(4)+"";
                label11.Text = loginReader.GetDateTime(5).ToString("yyyy년 MM월 dd일");
                label12.Text = loginReader.GetString(6);
                label13.Text = (int)loginReader.GetDecimal(7) + "원";
                textBox1.Text = loginReader.GetString(8);
            }
            login_attempt.Close();
        }

        private void 직원추가ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manage_add MA = new Manage_add(this);
            MA.ShowDialog();
        }
    }
}
