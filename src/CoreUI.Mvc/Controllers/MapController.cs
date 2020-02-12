using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CoreUI.Mvc.Controllers
{
    public class MapController : Controller
    {

        public IActionResult Test()
        {
            return View();
        }

        public IActionResult GeoTest()
        {
            return View();
        }
    }
}