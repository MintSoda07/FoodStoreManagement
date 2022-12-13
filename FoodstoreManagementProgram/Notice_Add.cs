using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace FoodstoreManagementProgram
{
    public partial class Notice_Add : Form
    {
        Main_Page h;
        public Notice_Add(Main_Page mp)
        {
            InitializeComponent();
            h = mp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            File.Delete(@"C:\Users\USER\AppData\Roaming\FSM_NOTICE.json");
            JObject data = new JObject(
                       new JProperty("NOTICE", textBox1.Text)
                       );
            File.WriteAllText(@"C:\Users\USER\AppData\Roaming\FSM_NOTICE.json", data.ToString());
            Main_Page mp = new Main_Page();
            mp.Show();
            h.Close();
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string notice = "";
                using (StreamReader file = File.OpenText(@"C:\Users\USER\AppData\Roaming\FSM_NOTICE.json"))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject json = (JObject)JToken.ReadFrom(reader);
                    notice = (string)json["NOTICE"].ToString();
                }
                if (notice != null)
                {
                    textBox1.Text = notice;
                }
            }
            catch (Exception no_json)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
