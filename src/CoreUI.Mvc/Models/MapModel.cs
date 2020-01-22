using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;	
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace CoreUI.Mvc.Models
{
    public class MapModel
    {
        //private SensoresContext context;
        public int M { get; set; }
        public float T { get; set; }
        public float H { get; set; }
        public float I { get; set; }
        public float O { get; set; }
    }
}
