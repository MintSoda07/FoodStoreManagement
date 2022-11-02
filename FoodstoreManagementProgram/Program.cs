using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace FoodstoreManagementProgram
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        

        [STAThread]
        static void Main()
        {
            string path = @"C:\Users\USER\AppData\Roaming\FSM.json";
            string Serial = "";
            try
            {
                if (!File.Exists(path))
                {
                    Application.Run(new Serial_Check_Page());
                }
                else
                {
                    using (StreamReader file = File.OpenText(path))
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        JObject json = (JObject)JToken.ReadFrom(reader);
                        Serial = (string)json["SERIAL_CODE"].ToString();
                    }
                }
                if (Serial == null || Serial == "")
                {
                    Application.Run(new Serial_Check_Page());
                }
                else
                {
                    Application.Run(new Login_Page());
                }
            }catch(Exception e)
            {
                Application.Run(new Serial_Check_Page());
            }
            
            
        }
        public static bool ownerLogin = false;
    }
}
