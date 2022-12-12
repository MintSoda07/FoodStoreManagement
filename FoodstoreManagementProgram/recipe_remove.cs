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
    public partial class recipe_remove : Form
    {
        public recipe_remove()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            recipe re = new recipe();
            re.Tag = this;
            re.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
            string sqlQuery =  "DELETE RECIPE FROM WHERE MENU_NAME";
            if(listView1.SelectedItems == null)
            {
                MessageBox.Show("메뉴를 선택해주세요");
            }
            int summary = 0;
            listView1.SelectedItems[0].Remove();
            foreach (ListViewItem item in listView1.Items)
            {
                summary += Int32.Parse(item.SubItems[2].Text);
            }
        }

        private void recipe_remove_Load(object sender, EventArgs e)
        {
            String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
            string sqlQuery = "SELECT * FROM RECIPE";

            OracleConnection login_attempt = new OracleConnection(connInfo);
            OracleCommand loginCommand = new OracleCommand();
            loginCommand.Connection = login_attempt;
            loginCommand.CommandText = sqlQuery; login_attempt.Open();
            OracleDataReader loginReader;
            loginReader = loginCommand.ExecuteReader();

            OracleDataAdapter oda = new OracleDataAdapter();
            oda.SelectCommand = new OracleCommand(sqlQuery);

            while (loginReader.Read())
            {
                ListViewItem li = new ListViewItem();
                li.Text = loginReader.GetString(0);
                li.SubItems.Add(loginReader.GetString(1));
                li.SubItems.Add(loginReader.GetString(2));
                li.SubItems.Add(loginReader.GetString(3));
                listView1.Items.Add(li);
            }
            login_attempt.Close();
        }
    }
}
