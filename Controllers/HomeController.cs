using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FanFicFabliaux.Models;

namespace FanFicFabliaux.Controllers
{
    /// <summary>
    /// Used for initial navigation.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        /// <summary>
        /// Initializes HomeController.
        /// </summary>
        /// <param name="logger"></param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Navigates to landing page.
        /// </summary>
        /// <returns>Landing page.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Navigates to privacy page.
        /// </summary>
        /// <returns>Privacy page.</returns>
        public IActionResult Privacy()
        {
            return View();
        }
        /// <summary>
        /// Navigates to error page.
        /// </summary>
        /// <returns>error page.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
