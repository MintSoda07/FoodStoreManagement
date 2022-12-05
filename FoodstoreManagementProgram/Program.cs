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
        public static bool ownerLogin = false;
        public static String SERIAL;
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>


        [STAThread]
        static void Main()
        {
            Application.Run(new Table_Menu());
            string path = @"C:\Users\USER\AppData\Roaming\FSM.json";
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
                        SERIAL = (string)json["SERIAL_CODE"].ToString();
                    }
                }
                if (SERIAL == null || SERIAL == "")
                {
                    Application.Run(new Serial_Check_Page());
                }
                else
                {
                    String connInfo = "User Id=FSM; Password=vnemtmxhdj; Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.142.10)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe) ) );";
                    string sqlQuery = "SELECT * FROM PROGRAM";

                    OracleConnection Serial_check = new OracleConnection(connInfo);
                    OracleCommand Serial_command = new OracleCommand();
                    Serial_command.Connection = Serial_check;
                    Serial_command.CommandText = sqlQuery; Serial_check.Open();
                    OracleDataReader SerialReader;
                    SerialReader = Serial_command.ExecuteReader();
                    Boolean SERIAL_FOUND = false;
                    while (SerialReader.Read())
                    {
                        if (SERIAL == SerialReader.GetString(0))
                        {
                            SERIAL_FOUND = true;
                            Application.Run(new Login_Page());
                        }
                    }
                    if (!SERIAL_FOUND)
                    {
                        Application.Run(new Serial_Check_Page());
                    }
                    

                }
            }catch(Exception e)
            {
                MessageBox.Show("예상치 못한 오류가 발생했습니다.");
            }
            
            
        }
        
    }
}
