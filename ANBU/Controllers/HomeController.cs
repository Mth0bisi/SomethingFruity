using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SomethingFruity.Models;
using Microsoft.AspNetCore.Authorization;

namespace SomethingFruity.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Greetings = Greetings();

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult AccessDenied()
        {
            return View();
        }

        protected string Greetings()
        {
            string message;
            if (DateTime.Now.Hour < 12)
            {
                message = "Good Morning";
            }
            else if (DateTime.Now.Hour < 17)
            {
                message = "Good Afternoon";
            }
            else
            {
                message = "Good Evening";
            }
            return message;
        }
    }
}
