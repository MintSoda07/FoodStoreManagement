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
    public partial class recipe : Form
    {
        public recipe()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main_Page mp = new Main_Page();
            mp.Tag = this;
            mp.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            recipe_remove rr = new recipe_remove(this);
            rr.Tag = this;
            rr.ShowDialog();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            recipe_add ra = new recipe_add(this);
            ra.Tag = this;
            ra.ShowDialog();

        }

        private void recipe_Load(object sender, EventArgs e)
        {
            String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
            string sqlQuery = "SELECT * FROM MENU";
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
            dt.Columns.Add("이름", typeof(string));
            dt.Columns.Add("가격", typeof(string));
            dt.Columns.Add("재료", typeof(string));
            dt.Columns.Add("시간", typeof(string));
            dt.Columns.Add("설명", typeof(string));
            dt.Columns.Add("순서", typeof(string));
            while (loginReader.Read())
            {
                dt.Rows.Add(loginReader.GetString(0), loginReader.GetDecimal(1).ToString(), loginReader.GetString(2), loginReader.GetString(3), loginReader.GetString(4), loginReader.GetString(5));
            }
            login_attempt.Close();
            dataGridView1.DataSource = dt;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            String data = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
            string sqlQuery = "SELECT * FROM MENU WHERE MENU_NAME='"+data+"'";
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
            dt.Columns.Add("이름", typeof(string));
            dt.Columns.Add("가격", typeof(string));
            dt.Columns.Add("재료", typeof(string));
            dt.Columns.Add("시간", typeof(string));
            dt.Columns.Add("설명", typeof(string));
            dt.Columns.Add("순서", typeof(string));
            int summary = 0;
            while (loginReader.Read())
            {
                dt.Rows.Add(loginReader.GetString(0), loginReader.GetDecimal(1).ToString(), loginReader.GetString(2), loginReader.GetString(3), loginReader.GetString(4), loginReader.GetString(5));
                textBox3.Text = loginReader.GetString(0);
                textBox4.Text = loginReader.GetDecimal(1).ToString();
                textBox5.Text = loginReader.GetString(2);
                textBox6.Text = loginReader.GetString(3);
                textBox2.Text = loginReader.GetString(4);
                textBox1.Text = loginReader.GetString(5);
            }
        }
    }
}