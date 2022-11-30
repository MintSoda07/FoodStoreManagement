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
    public partial class recipe : Form
    {
        public recipe()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main_Page mp = new Main_Page();
            mp.Tag = this;
            mp.Show();
            this.Hide();
        }

        private void recipe_Load(object sender, EventArgs e)
        {
           String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
           string sqlQuery = "SELECT * FROM RECIPE WHERE MENU";

            OracleConnection recipe = new OracleConnection(connInfo);
            OracleCommand recipeCommand = new OracleCommand();
            recipeCommand.Connection = recipe;
            recipeCommand.CommandText = sqlQuery;
            recipe.Open();
            OracleDataReader recipeReader;
            recipeReader = recipeCommand.ExecuteReader();

            OracleDataAdapter oda = new OracleDataAdapter();
            oda.SelectCommand = new OracleCommand(sqlQuery);
            DataTable dt = new DataTable();
            dt.Columns.Add("음식이름", typeof(string));
            dt.Columns.Add("재료", typeof(string));
            dt.Columns.Add("순서", typeof(string));
            dt.Columns.Add("주의사항", typeof(string));
            dt.Columns.Add("사진 경로", typeof(string));
            while (recipeReader.Read())
            {
                dt.Rows.Add(
                    recipeReader.GetString(1), 
                    recipeReader.GetString(2), 
                    recipeReader.GetString(3), 
                    recipeReader.GetString(4), 
                    recipeReader.GetString(5));
            }
            recipe.Close();
            dataGridView1.DataSource = dt;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
    }
}
