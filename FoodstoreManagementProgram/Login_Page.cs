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

namespace FoodstoreManagementProgram
{
    public partial class Login_Page : Form
    {
        public Login_Page()
        {
            InitializeComponent();
        }
        //임시 데이터베이스 참조
        public String user_id = "domino";
        public String user_password = "1552";

        public String owner_id = "BHC";
        public String owner_password = "1954";
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //입력 ID와 PASSWORD를 데이터베이스로 전송해 검색
            
            //결과값이 없으면 NULL, 로그인 실패
            //결과값이 반환되면 로그인 성공
            if (textBox1.Text == owner_id && textBox2.Text == owner_password)
            {
                MessageBox.Show("[관리자] 로그인 되었습니다.", "로그인");
                Program.ownerLogin = true;
                Main_Page f2 = new Main_Page();
                f2.Tag = this;
                f2.Show();
                this.Hide();
            }
            else if (textBox1.Text == user_id && textBox2.Text == user_password)
            {
                MessageBox.Show("[사용자] 로그인 되었습니다.", "로그인");
                Program.ownerLogin = false;
                Main_Page f2 = new Main_Page();
                f2.Tag = this;
                f2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("아이디 혹은 비밀번호가 일치하지 않습니다.", "로그인");
            }
        }

        private void Login_Page_Load(object sender, EventArgs e)
        {
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FirstSetting first = new FirstSetting();
            first.Tag = this;
            first.Show();
            this.Hide();
        }
    }
}
