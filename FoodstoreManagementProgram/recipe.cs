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

        private void button2_Click(object sender, EventArgs e)
        {
            recipe_remove rr = new recipe_remove();
            rr.Tag = this;
            rr.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            recipe_add ra = new recipe_add();
            ra.Tag = this;
            ra.Show();
            this.Hide();
        }
    }
}
