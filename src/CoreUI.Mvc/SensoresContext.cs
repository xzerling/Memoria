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
        public string ConnStringLocal { get; set; }

        public SensoresContext(string conectionString, string local)
        {
            this.ConnectionString = conectionString;
            this.ConnStringLocal = local;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        private MySqlConnection GetDefaultConn()
        {
            return new MySqlConnection(ConnStringLocal);
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

        public List<AgrodatosModel> GetStations()
        {
            List<AgrodatosModel> estaciones = new List<AgrodatosModel>();
            using(MySqlConnection conn = GetDefaultConn())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT nombre, id FROM estacioneve", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        estaciones.Add(new AgrodatosModel()
                        {
                            NombreEstacion = reader.GetString("nombre"),
                            Id = reader.GetInt32("id")

                        });
                    }
                }
                conn.Close();
            }
            return estaciones;
        }

        public List<AgrodatosModel> GetInstrumentos(int id)
        {
            List<AgrodatosModel> Instrumentos = new List<AgrodatosModel>();
            using (MySqlConnection conn = GetDefaultConn())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT modelo, id, tipo_instrumento_id FROM instrumento WHERE estacion_id = @id ORDER BY tipo_instrumento_id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Instrumentos.Add(new AgrodatosModel()
                        {
                            NombreSensor = reader.GetString("modelo"),
                            Id = reader.GetInt32("tipo_instrumento_id")

                        });
                    }
                }
                conn.Close();
            }
            return Instrumentos;

        }

        //Consulta para obtener datos agronomicos de una estacion y fechas determinadas.

        public List<AgroDataModel> SendDataRquest(int estacion, int datos, string fechaI, string fechaT)
        {
            List<AgroDataModel> reporte = new List<AgroDataModel>();
            using(MySqlConnection conn = GetDefaultConn())
            {
                conn.Open();
                if(datos == 1)
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT fecha, hora, GROUP_CONCAT(medicion.medicion order by tipo_instrumento_id SEPARATOR '|')AS medicion FROM `medicion`, `estacioneve`, `instrumento` WHERE estacioneve.id = instrumento.estacion_id AND estacioneve.id = @estacion AND medicion.instrumento_id = instrumento.id AND fecha >= @fechaI AND fecha <= @fechaT group by fecha, hora", conn);
                    cmd.Parameters.AddWithValue("@estacion", estacion);
                    cmd.Parameters.AddWithValue("@fechaI", fechaI);
                    cmd.Parameters.AddWithValue("@fechaT", fechaT);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("holamundo");
                            reporte.Add(new AgroDataModel()
                            {
                                fecha = reader["fecha"].ToString(),

                                hora = reader["hora"].ToString(),
                                datos = reader.GetString("medicion")



                            });
                        }
                    }
                }
                if(datos == 2)
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT fecha, hora, GROUP_CONCAT(medicion.medicion order by tipo_instrumento_id SEPARATOR '|')AS medicion FROM `medicion`, `estacioneve`, `instrumento` WHERE estacioneve.id = instrumento.estacion_id AND estacioneve.id = @estacion AND medicion.instrumento_id = instrumento.id AND fecha >= @fechaI AND fecha <= @fechaT AND EXTRACT(MINUTE FROM hora)=0 group by fecha, hora", conn);
                    cmd.Parameters.AddWithValue("@estacion", estacion);
                    cmd.Parameters.AddWithValue("@fechaI", fechaI);
                    cmd.Parameters.AddWithValue("@fechaT", fechaT);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("holamundo");
                            reporte.Add(new AgroDataModel()
                            {
                                fecha = reader["fecha"].ToString(),

                                hora = reader["hora"].ToString(),
                                datos = reader.GetString("medicion")



                            });
                        }
                    }
                }


                conn.Close();


            }
            return reporte;
        }

    }
}
