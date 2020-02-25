using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DesafioDiegoBenedetti.EF_Dapper.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (View().ViewData.ContainsKey("Message"))
                View().ViewData["Message"] = string.Empty;

            return View();
        }
    }
}