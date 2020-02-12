using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreUI.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using CoreUI.Mvc.Data;

namespace CoreUI.Mvc.Controllers
{
    public class SensorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SensorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Sensores()
        {
            SensoresContext context = HttpContext.RequestServices.GetService(typeof(CoreUI.Mvc.SensoresContext)) as SensoresContext;
            List<SensorModel> lista = context.GetAllSensors();
            //List<SensorModel> list = _context.ns1.ToList();

            var Graphic = new List<SimpleReportViewModel>();
            var Tiempo = lista.ElementAt(0).M;
            //Graphic.Add(new SimpleReportViewModel
           // {
                //DimensionOne = Tiempo,

            //});

            return View(lista);
        }
    }
}
