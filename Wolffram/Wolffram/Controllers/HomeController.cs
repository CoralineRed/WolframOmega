﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using WolframOmega;

namespace Wolffram.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var token = System.IO.File.ReadAllText("token.txt");
            new BotUi(token).Run();
            return View();
        }
    }
}