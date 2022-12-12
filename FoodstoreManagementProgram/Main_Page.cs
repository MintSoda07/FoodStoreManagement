using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodstoreManagementProgram
{
    public partial class Main_Page : Form
    {
        public Main_Page()
        {
            InitializeComponent();
        }

        private void Main_Page_Load(object sender, EventArgs e)
        {
            // 관리자와 비관리자 인터페이스 차이점
            if (Program.ownerLogin == false)
            {
                linkLabel1.Enabled = false;
                linkLabel1.Text = "사용자";
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                label3.Text = "아르바이트"; // 직급
                label2.Text = "도미노"; // 이름
            }
            else
            {
                label3.Text = "관리자"; 
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
    }
}
