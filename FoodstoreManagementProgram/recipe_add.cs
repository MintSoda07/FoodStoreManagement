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
    public partial class recipe_add : Form
    {
        public recipe_add()
        {
            InitializeComponent();
        }

        private void recipe_add_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
                string sqlQuery = "INSERT INTO RECIPE VALUES('" + textBox4.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox4.Text + "')";

                OracleConnection login_attempt = new OracleConnection(connInfo);
                OracleCommand loginCommand = new OracleCommand();
                loginCommand.Connection = login_attempt;
                loginCommand.CommandText = sqlQuery; login_attempt.Open();
                loginCommand.ExecuteNonQuery();
                login_attempt.Close();
                MessageBox.Show("메뉴를 추가했습니다");
                this.Close();

            }
            catch(Exception e11)
            {
                MessageBox.Show("메뉴를 추가하지 못했습니다");
            }
        }
    }
    }

