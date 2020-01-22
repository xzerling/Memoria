using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreUI.Mvc.Controllers
{
    public class SensorController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            SensoresContext context = HttpContext.RequestServices.GetService(typeof(CoreUI.Mvc.Models.SensorModel)) as SensoresContext;

            return View(context.GetAllSensors());
        }
    }
}
