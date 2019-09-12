using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UI.Helpers;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<MailConfig> _config;
        public HomeController(IOptions<MailConfig> config)
        {
            _config = config;
        }

        public IActionResult GetMailConfig()
        {            
            return Json(_config);
        }
        public IActionResult Index()
        {
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
