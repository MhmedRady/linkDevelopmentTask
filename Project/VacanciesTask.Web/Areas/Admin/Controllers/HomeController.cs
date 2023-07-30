using System.Diagnostics;
using VacanciesTask.Controllers;
using VacanciesTask.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewProject.Application;
using NewProject.DBL;
using NToastNotify;

namespace VacanciesTask.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : _Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IToastNotification _toastNotification;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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