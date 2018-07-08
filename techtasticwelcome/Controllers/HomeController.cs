using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using techtasticwelcome.Models;

namespace techtasticwelcome.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _configuration { get; }

        public HomeController (IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var model = _configuration["Greeting"];
            return View("Index", model);
        }

        public IActionResult Test()
        {
            throw new InvalidOperationException("Sorry, this feature is not supported");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
