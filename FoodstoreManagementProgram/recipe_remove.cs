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
        public recipe Parent_Page;
        public recipe_remove(recipe Form2)
        {
            InitializeComponent();
            Parent_Page = Form2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            if (listView1.SelectedItems.Count <= 0)
            {
                MessageBox.Show("선택된 레시피가 없습니다.");
            }
            else
            {
                String data = listView1.SelectedItems[0].Text;
                String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
                string sqlQuery = "DELETE FROM MENU WHERE MENU_NAME='" + data + "'";
                OracleConnection login_attempt = new OracleConnection(connInfo);
                OracleCommand loginCommand = new OracleCommand();
                loginCommand.Connection = login_attempt;
                loginCommand.CommandText = sqlQuery;
                login_attempt.Open();



                OracleDataAdapter oda = new OracleDataAdapter();
                oda.SelectCommand = new OracleCommand(sqlQuery);
                if (MessageBox.Show("선택하신 레시피가 삭제 됩니다", "레시피 삭제", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (listView1.SelectedItems.Count > 0)
                    {
                        int index = listView1.FocusedItem.Index;
                        listView1.Items.RemoveAt(index);
                        loginCommand.ExecuteNonQuery();
                    }

                }
                login_attempt.Close();
                Parent_Page.Close();
                recipe mp = new recipe();
                mp.Tag = this;
                mp.Show();
            }
            
        }

        private void recipe_remove_Load(object sender, EventArgs e)
        {
            String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
            string sqlQuery = "SELECT * FROM MENU";

            OracleConnection login_attempt = new OracleConnection(connInfo);
            OracleCommand loginCommand = new OracleCommand();
            loginCommand.Connection = login_attempt;
            loginCommand.CommandText = sqlQuery;
            login_attempt.Open();
            OracleDataReader loginReader;
            loginReader = loginCommand.ExecuteReader();

            OracleDataAdapter oda = new OracleDataAdapter();
            oda.SelectCommand = new OracleCommand(sqlQuery);

            while (loginReader.Read())
            {
                ListViewItem li = new ListViewItem();
                li.Text = loginReader.GetString(0);
                li.SubItems.Add(loginReader.GetDecimal(1).ToString());
                li.SubItems.Add(loginReader.GetString(2));
                li.SubItems.Add(loginReader.GetString(3));
                li.SubItems.Add(loginReader.GetString(4));
                li.SubItems.Add(loginReader.GetString(5));
                listView1.Items.Add(li);
            }
            login_attempt.Close();
        }
    }
}
