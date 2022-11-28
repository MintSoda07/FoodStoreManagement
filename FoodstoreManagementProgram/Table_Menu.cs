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
    public partial class Table_Menu : Form
    {
        public int table_count=21; //temp 값 (최대 36)
        public Table_Menu()
        {
            InitializeComponent();
            
        }

        private void Table_Menu_Load(object sender, EventArgs e)
        {
            int removed_table = groupBox1.Controls.Count - table_count;
            for(int i = 0; i < removed_table; i++)
            {
                groupBox1.Controls[i].Visible=false;
                
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button39_Click(object sender, EventArgs e)
        {
            Main_Page mp = new Main_Page();
            mp.Tag = this;
            mp.Show();
            this.Hide();
        }

        private void Table_Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void button40_Click(object sender, EventArgs e)
        {
            /*try
            {

                String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
                string sqlQuery = "SELECT * FROM LOGIN_DATA";

                OracleDataAdapter DBAdapter = new OracleDataAdapter(sqlQuery, connInfo);
                OracleCommandBuilder myCommandBuilder = new OracleCommandBuilder(DBAdapter);
                DataSet DS = new DataSet();
                DBAdapter.Fill(DS, "LOGIN_DATA");
                DataTable LoginInfo = DS.Tables["LOGIN_DATA"];
                DataRow newRow = LoginInfo.NewRow();
                newRow["USER_ID"] = textBox3.Text;
                newRow["USER_PWD"] = textBox4.Text;
                newRow["ACESS_LEVEL"] = "ADMIN";
                LoginInfo.Rows.Add(newRow); DBAdapter.Update(DS, "LOGIN_DATA");

                MessageBox.Show("관리자 아이디:" + textBox3.Text + "\n관리자 패스워드:" + textBox4.Text + "\n설정이 완료되었습니다.", "알림");
                Login_Page f1 = new Login_Page();
                f1.Tag = this;
                f1.Show();
                this.Hide();
            }
            catch (Exception Oracle_error)
            {
                MessageBox.Show("사용할 수 없는 아이디입니다. 다른 아이디로 시도해 주세요.", "알림");

            }*/
        }
    }
}
