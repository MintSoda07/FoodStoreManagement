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
    public partial class Main_Page : Form
    {
        string path = @"C:\Users\USER\AppData\Roaming\FSM_NOTICE.json";
        public Main_Page()
        {
            InitializeComponent();
        }

        private void Main_Page_Load(object sender, EventArgs e)
        {
            try
            {
                string notice = "";
                using (StreamReader file = File.OpenText(path))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject json = (JObject)JToken.ReadFrom(reader);
                    notice = (string)json["NOTICE"].ToString();
                }
                if (notice != null)
                {
                    textBox1.Text = notice;
                }
            }
            catch(Exception no_json) { 
                
            }

            // 관리자와 비관리자 인터페이스 차이점
            try
            {
                String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
                string sqlQuery = "SELECT a.NAME,a.RANK FROM CLIENT a JOIN LOGIN_DATA b ON a.CLIENT_NO=b.CLIENT_CODE where b.USER_ID='"+ Program.id + "'";
                OracleConnection login_attempt = new OracleConnection(connInfo);
                OracleCommand loginCommand = new OracleCommand();
                loginCommand.Connection = login_attempt;
                loginCommand.CommandText = sqlQuery; login_attempt.Open();
                OracleDataReader loginReader;
                loginReader = loginCommand.ExecuteReader();
                while (loginReader.Read())
                {
                    label2.Text = loginReader.GetString(0);
                    label3.Text = loginReader.GetString(1);
                }
            }
            catch(Exception himdurua)
            {
                label2.Text = "푸드스토어";
                label3.Text = "관리자";
            }
            if (label3.Text != "관리자")
            {
                button3.Enabled = false;
                button4.Enabled = false;
                button2.Enabled = false;
                linkLabel1.Visible = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            Table_Menu tableMenu = new Table_Menu();
            tableMenu.Tag = this;
            tableMenu.Show();
            this.Hide();
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("로그아웃 되었습니다.", "알림");
            Login_Page loginPage = new Login_Page();
            loginPage.Tag = this;
            loginPage.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Food_Page foodPage= new Food_Page();
            foodPage.Tag = this;
            foodPage.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Manage_Menu mp = new Manage_Menu();
            mp.Tag = this;
            mp.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sales_management mm = new sales_management();
            mm.Tag = this;
            mm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            recipe rc= new recipe();
            rc.Tag = this;
            rc.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Notice_Add na = new Notice_Add(this);
            na.ShowDialog(this);
        }
    }
}
