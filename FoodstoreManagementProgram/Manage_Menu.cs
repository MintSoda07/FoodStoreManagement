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
    public partial class Manage_Menu : Form
    {
        public Manage_Menu()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main_Page mp = new Main_Page();
            mp.Tag = this;
            mp.Show();
            this.Hide();
        }

        private void Manage_Menu_Load(object sender, EventArgs e)
        {
            String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
            string sqlQuery = "SELECT * FROM CLIENT WHERE WORKPLACE=" + Program.SERIAL;

            OracleConnection login_attempt = new OracleConnection(connInfo);
            OracleCommand loginCommand = new OracleCommand();
            loginCommand.Connection = login_attempt;
            loginCommand.CommandText = sqlQuery; login_attempt.Open();
            OracleDataReader loginReader;
            loginReader = loginCommand.ExecuteReader();
            while (loginReader.Read())
            {
                ListViewItem ivt=new ListViewItem();
                ivt.SubItems.Add(loginReader.GetString(1));
                ivt.SubItems.Add(loginReader.GetString(2));
                ivt.SubItems.Add(loginReader.GetDateTime(3).ToString());
                ivt.SubItems.Add(loginReader.GetString(0).ToString());
                listView1.Items.Add(ivt);
            }
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
           /*  ivt = e.Column;
            String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
            string sqlQuery = "SELECT * FROM CLIENT WHERE CLEINT_NO=" + e.SubItems.;

            OracleConnection login_attempt = new OracleConnection(connInfo);
            OracleCommand loginCommand = new OracleCommand();
            loginCommand.Connection = login_attempt;
            loginCommand.CommandText = sqlQuery; login_attempt.Open();
            OracleDataReader loginReader;
            loginReader = loginCommand.ExecuteReader();
            label1.Text=*/
        }
    }
}
