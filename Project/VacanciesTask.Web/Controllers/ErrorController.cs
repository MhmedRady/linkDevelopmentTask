using VacanciesTask.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewProject.Domain;
using NewProject.EntityFrameworkCore;

namespace VacanciesTask.Web.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        public ErrorController(ILogger<HomeController> logger, UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, PageNot Found!";
                    break;
                case 403:
                    ViewBag.ErrorMessage = "Sorry, AccessDenid!";
                    return RedirectToAction("AccessDenied");
            }
            ViewBag.statusCode = statusCode;
            return View("404");
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AccessDenied()
        {
            ViewBag.User = await _userManager.GetUserAsync(User);
            ViewBag.IsAdmin = await _userManager.IsInRoleAsync(ViewBag.User, "admin");
            return View("404");
        }
        
    }
}