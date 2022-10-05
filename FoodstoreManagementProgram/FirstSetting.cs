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
    public partial class FirstSetting : Form
    {
        public FirstSetting()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("설정이 완료되었습니다.", "알림");
            Login_Page f1 = new Login_Page();
            f1.Tag = this;
            f1.Show();
            this.Hide();
        }
    }
}
