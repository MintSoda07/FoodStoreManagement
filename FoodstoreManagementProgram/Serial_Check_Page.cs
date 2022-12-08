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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;


namespace FoodstoreManagementProgram
{
    public partial class Serial_Check_Page : Form
    {
        public Serial_Check_Page()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            textBox1.Focus();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
            string sqlQuery = "SELECT STORE_CODE FROM FOODSTORE";

            OracleConnection login_attempt = new OracleConnection(connInfo);
            OracleCommand loginCommand = new OracleCommand();
            loginCommand.Connection = login_attempt;
            loginCommand.CommandText = sqlQuery; login_attempt.Open();
            OracleDataReader loginReader;
            loginReader = loginCommand.ExecuteReader();
            Boolean SERIAL_FOUND = false;
            while (loginReader.Read())
            {
                if (textBox1.Text == loginReader.GetString(0))
                {
                    textBox1.ReadOnly = true;
                    SERIAL_FOUND = true;
                    textBox2.Text = "사용할 수 있는 시리얼입니다.";
                    button2.Enabled = true;
                    break;
                }
            }
            if (SERIAL_FOUND == false)
            {
                textBox2.Text = "존재하지 않는 시리얼입니다.";
            }

        }

        private void Serial_Check_Page_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.SERIAL = textBox1.Text;

            String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
            string sqlQuery = "SELECT WCODE FROM LOGIN_DATA WHERE ACESS_LEVEL='ADMIN'";

            OracleConnection login_attempt = new OracleConnection(connInfo);
            OracleCommand loginCommand = new OracleCommand();
            loginCommand.Connection = login_attempt;
            loginCommand.CommandText = sqlQuery; login_attempt.Open();
            OracleDataReader loginReader;
            loginReader = loginCommand.ExecuteReader();
            Boolean ID_FOUND = false;
            while (loginReader.Read())
            {
                if (loginReader.GetString(0) == Program.SERIAL)
                {
                    MessageBox.Show("관리자 계정이 존재합니다. 로그인 화면으로 이동합니다.");
                    JObject data = new JObject(
                        new JProperty("SERIAL_CODE", Program.SERIAL)
                        );
                    File.WriteAllText(@"C:\Users\USER\AppData\Roaming\FSM.json", data.ToString());
                    ID_FOUND = true;
                    Login_Page first = new Login_Page();
                    first.Tag = this;
                    first.Show();
                    this.Hide();
                    break;
                }
            }
            if (ID_FOUND == false)
            {
                MessageBox.Show("관리자 계정이 존재하지 않습니다. 처음 설정으로 이동합니다.");
                FirstSetting first = new FirstSetting();
                first.Tag = this;
                first.Show();
                this.Hide();
            }   
        }

        private void Serial_Check_Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.ReadOnly == true)
                {
                    button2_Click(sender, e);
                }
                else
                {
                    button1_Click(sender, e);
                }
            }
        }
    }
}
