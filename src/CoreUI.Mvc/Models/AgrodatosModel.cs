using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;	
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace CoreUI.Mvc.Models
{
    public class AgrodatosModel
    {
        public string NombreEstacion { get; set; }
        public int Id { get; set; }
        public string NombreSensor { get; set; }

    }


}
