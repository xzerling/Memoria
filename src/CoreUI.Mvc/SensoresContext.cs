using CoreUI.Mvc.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreUI.Mvc
{
    public class SensoresContext
    {
        public string ConnectionString { get; set; }

        public SensoresContext(string conectionString)
        {
            this.ConnectionString = conectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<SensorModel> GetAllSensors()
        {
            List<SensorModel> sensores = new List<SensorModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
#pragma warning disable IDE0067 // Desechar (Dispose) objetos antes de perder el ámbito
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM ns1", conn);
#pragma warning restore IDE0067 // Desechar (Dispose) objetos antes de perder el ámbito
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sensores.Add(new SensorModel()
                        {
                            M = reader.GetInt32("M"),
                            T = reader.GetFloat("T"),
                            H = reader.GetFloat("H"),
                            I = reader.GetFloat("I"),
                            O = reader.GetFloat("O"),
                            /*Dia = reader.GetInt32("M"),
                            temperatura = reader.GetFloat("T")*/

                        });
                        //Console.WriteLine("Leyendo en mysql: "+ reader.GetInt32("M"));
                    }
                }
            }

            return sensores;
        }

    }
}
