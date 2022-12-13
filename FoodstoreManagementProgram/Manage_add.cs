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
    public partial class Manage_add : Form
    {
        
        public Manage_add(Manage_Menu ma)
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            

            if (radioButton8.Checked)
            {
                textBox3.Enabled = true;
                textBox3.ReadOnly = false;
            }
            else
            {
                textBox3.Enabled = false;
                textBox3.ReadOnly = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int age = 0, pay = 0;
            if (textBox1.Text == "")
            {
                MessageBox.Show("이름을 입력하십시오.");
                textBox1.Focus();
            }
            else if(textBox2.Text == "")
            {
                MessageBox.Show("나이를 입력하십시오.");
                textBox2.Focus();
            }
            else if(textBox4.Text == "")
            {
                MessageBox.Show("급여를 입력하십시오.");
                textBox4.Focus();
            }
            else if(textBox5.Text == "")
            {
                MessageBox.Show("이름을 입력하십시오.");
                textBox5.Focus();
            }
            else if(textBox3.Text=="" && radioButton8.Checked)
            {
                MessageBox.Show("직급을 입력하십시오.");
                textBox3.Focus();
            }
            else
            {
                try
                {
                    age = Int32.Parse(textBox2.Text);
                    pay = Int32.Parse(textBox4.Text);
                }
                catch (Exception intparse)
                {
                    MessageBox.Show("올바른 값을 입력해야 합니다.");
                }
            }
            if (age < 1)
            {
                MessageBox.Show("나이는 0보다 작을 수 없습니다.");
                textBox2.Text = "1";
            }else if (pay < 0)
            {
                MessageBox.Show("급여는 0보다 작을 수 없습니다.");
                textBox4.Text = "0";
            }
            else
            {
                string sex = "";
                string rank = "";
                if (radioButton1.Checked)
                {
                    sex = radioButton1.Text;
                }
                else
                {
                    sex = radioButton2.Text;
                }
                if (radioButton3.Checked)
                {
                    rank = radioButton3.Text;
                }else if (radioButton4.Checked)
                {
                    rank = radioButton4.Text;
                }else if (radioButton5.Checked)
                {
                    rank = radioButton5.Text;
                }else if (radioButton6.Checked)
                {
                    rank = radioButton6.Text;
                }else if (radioButton7.Checked)
                {
                    rank = radioButton7.Text;
                }else if (radioButton8.Checked)
                {
                    rank = textBox3.Text;
                }else if (radioButton8.Checked)
                {
                    rank = radioButton8.Text;
                }
                try
                {
                    DateTime myDateTime = DateTime.Now;
                    String sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
        String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
                    String sql_insert = "INSERT INTO CLIENT VALUES(CLIENT_NO_GENERATOR.NEXTVAL(),'"+textBox1.Text+"','"+rank+"','"+ sqlFormattedDate + "','"+sex+"','"+dateTimePicker1.Value+"','"+textBox5.Text+"','"+null+"','"+Program.SERIAL+"'";
                    OracleConnection Order_Conn = new OracleConnection(connInfo);
                    Order_Conn.Open();
                    OracleCommand Order_Cmd = new OracleCommand();
                    Order_Cmd.Connection = Order_Conn;
                    Order_Cmd.CommandText = sql_insert;
                    Order_Cmd.ExecuteNonQuery();
                }
                catch (Exception EXEXEX)
                {

                }
            }
        }
    }
}
