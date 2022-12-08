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
    public partial class FirstSetting : Form
    {
        public FirstSetting()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox3.Text == null || textBox4.Text == null || textBox5.Text == null || textBox6.Text == null)
                {
                    MessageBox.Show("일부 항목에 값이 입력되지 않았습니다. 값을 입력해야 합니다.");
                }
                else if (Int32.Parse(textBox6.Text) < 0 && Int32.Parse(textBox6.Text) > 36)
                {
                    MessageBox.Show("좌석 수는 0부터 36까지의 값을 입력해야 합니다.");
                }
                else
                {
                    try
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
                        newRow["WCODE"] = Program.SERIAL;
                        

                        string sqlQuery2 = "INSERT INTO FOODSTORE VALUES('" + Program.SERIAL + "','" + textBox5.Text + "')";
                        OracleConnection sql_conn = new OracleConnection(connInfo);
                        OracleCommand sql_cmd = new OracleCommand();
                        sql_conn.Open();
                        sql_cmd.Connection = sql_conn;
                        sql_cmd.CommandText = sqlQuery2;
                        sql_cmd.ExecuteNonQuery();

                        MessageBox.Show("관리자 아이디:" + textBox3.Text + "\n관리자 패스워드:" + textBox4.Text + "\n설정이 완료되었습니다.", "알림");
                        JObject data = new JObject(
                        new JProperty("IP", textBox1.Text),
                        new JProperty("PORT", textBox2.Text),
                        new JProperty("SERIAL_CODE", Program.SERIAL),
                        new JProperty("SEAT", textBox6.Text)
                        );
                        LoginInfo.Rows.Add(newRow); DBAdapter.Update(DS, "LOGIN_DATA");
                        File.WriteAllText(@"C:\Users\USER\AppData\Roaming\FSM.json", data.ToString());

                        Login_Page f1 = new Login_Page();
                        f1.Tag = this;
                        f1.Show();
                        this.Hide();
                    }
                    catch (Exception Oracle_error)
                    {
                        MessageBox.Show("사용할 수 없는 아이디입니다. 다른 아이디로 시도해 주세요.", "알림");
                        throw (Oracle_error);

                    }
                }
            }
            catch (Exception Unexpected_Err)
            {
                MessageBox.Show("항목에 올바른 값을 입력해야 합니다.");
                throw (Unexpected_Err);
            }
        }



        private void FirstSetting_Load(object sender, EventArgs e)
        {

        }
    }
}
