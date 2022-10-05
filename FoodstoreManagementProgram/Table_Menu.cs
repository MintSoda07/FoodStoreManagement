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
    }
}
