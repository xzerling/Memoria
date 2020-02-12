using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreUI.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreUI.Mvc.Controllers
{
    public class AgrodatosController : Controller
    {

        [HttpGet]
        public IActionResult Agrodatos()
        {
            SensoresContext context = HttpContext.RequestServices.GetService(typeof(CoreUI.Mvc.SensoresContext)) as SensoresContext;
            List<AgrodatosModel> lista = context.GetStations();
            var Graphic = new List<SimpleReportViewModel>();
            return View(lista);
        }

        [HttpGet]
        public JsonResult NombreSensores(int id)
        {
            SensoresContext context = HttpContext.RequestServices.GetService(typeof(CoreUI.Mvc.SensoresContext)) as SensoresContext;
            List<AgrodatosModel> lista = context.GetInstrumentos(id);
            return Json(lista);
        }

        [HttpPost]
        public IActionResult EnviarConsulta(int estacion, int datos, string fechaI, string fechaT)
        {
            SensoresContext context = HttpContext.RequestServices.GetService(typeof(CoreUI.Mvc.SensoresContext)) as SensoresContext;
            List<AgroDataModel> resultado = context.SendDataRquest(estacion, datos, fechaI, fechaT);

            StringBuilder csv = new StringBuilder();

            for(int i = 0; i < resultado.Count; i++)
            {
                csv.AppendLine(resultado.ElementAt(i).fecha.Replace(" 0:00:00", "")+","+ resultado.ElementAt(i).hora+","+ resultado.ElementAt(i).datos.Replace('|', ','));

            }
            return File(System.Text.Encoding.ASCII.GetBytes(csv.ToString()), "text/csv", "data.csv");
            //return resultado;
        }
    }
}