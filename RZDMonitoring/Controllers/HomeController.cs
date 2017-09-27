using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using MJsonCoreLibrary;

namespace WebixApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            return View("index");
        }
    }
}
