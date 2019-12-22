using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WolframOmega;

namespace Wolfram.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var token = System.IO.File.ReadAllText("token.txt");
            new BotUi(token).Run();
            return View();
        }
    }
}
