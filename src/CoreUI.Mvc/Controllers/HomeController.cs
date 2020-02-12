using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreUI.Mvc.Models;

namespace CoreUI.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LineChart ()
        {
            var Graphic = new List<SimpleReportViewModel>();


            return View();
        }


                              
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            SensoresContext context = HttpContext.RequestServices.GetService(typeof(CoreUI.Mvc.SensoresContext)) as SensoresContext;
            //SensoresContext c2 = new SensoresContext("server=citra-test.cmmqgr4kvqxz.us-east-2.rds.amazonaws.com;port=3306;database=citra;user=admin;password=12345678");
            List<SensorModel> lista = context.GetAllSensors();

            var Graphic = new List<SimpleReportViewModel>();
            var Tiempo = lista.ElementAt(0).M;
            Graphic.Add(new SimpleReportViewModel
            {
                //DimensionOne = Tiempo,

            });

            return View(lista);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }






    }
}
