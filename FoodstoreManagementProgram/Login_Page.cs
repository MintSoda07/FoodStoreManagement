using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using Oracle.DataAccess.Client;

namespace FoodstoreManagementProgram
{
   
    public partial class Login_Page : Form
    {
       

        public Login_Page()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //입력 ID와 PASSWORD를 데이터베이스로 전송해 검색
            String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
            string sqlQuery = "SELECT * FROM LOGIN_DATA";
 
            OracleConnection login_attempt = new OracleConnection(connInfo);
            OracleCommand loginCommand = new OracleCommand();
            loginCommand.Connection = login_attempt;
            loginCommand.CommandText = sqlQuery; login_attempt.Open();
            OracleDataReader loginReader;
            loginReader = loginCommand.ExecuteReader();
            Boolean login_success = false;
            while (loginReader.Read())
            {
                if (textBox1.Text == loginReader.GetString(0))
                {
                    if (textBox2.Text == loginReader.GetString(1))
                    {
                        login_success = true;
                        if (loginReader.GetString(2) == "ADMIN")
                        {
                            MessageBox.Show("[관리자] 로그인 되었습니다.", "로그인");
                            Program.ownerLogin = true;
                            Main_Page f2 = new Main_Page();
                            f2.Tag = this;
                            f2.Show();
                            this.Hide();
                            break;
                        }
                        else
                        {
                            MessageBox.Show("[사용자] 로그인 되었습니다.", "로그인");
                            Program.ownerLogin = false;
                            Main_Page f2 = new Main_Page();
                            f2.Tag = this;
                            f2.Show();
                            this.Hide();
                            break;
                        }
                    }
                    else
                    {
                     // 비밀번호만 일치 X   
                    }
                }
            }
            if (login_success == false)
            {
                MessageBox.Show("아이디 혹은 비밀번호가 일치하지 않습니다.", "로그인");
            }
            //결과값이 없으면 NULL, 로그인 실패
            //결과값이 반환되면 로그인 성공
        }

        private void Login_Page_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void Login_Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }
    }
}
