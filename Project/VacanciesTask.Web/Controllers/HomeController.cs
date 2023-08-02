using VacanciesTask.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewProject.Application;
using NewProject.Domain;
using NewProject.EntityFrameworkCore;
using NewProject.Shared;
using NToastNotify;

namespace VacanciesTask.Web.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IJobTitleService _jobTitleService;
        private IUserApplayedService _userApplayedService;
        private IToastNotification _toastNotification;
        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, IJobTitleService jobTitleService, IToastNotification toastNotification, IUserApplayedService userApplayedService)
        {
            _logger = logger;
            _jobTitleService = jobTitleService;
            _toastNotification = toastNotification;
            _userApplayedService = userApplayedService;
        }

        public async Task<IActionResult> Index()
        {
            var jobTitles = await _jobTitleService.GetAll(jt=>jt.ValidityDuration.To >= DateTime.Today, includes: new[]{ "ValidityDuration"});
            return View(jobTitles);
        }

        [HttpGet]
        public async Task<IActionResult> ShowJobTitle(Guid id)
        {
            var jobTitle = await _jobTitleService.GetWrite(jobTitle => jobTitle.Id == id, includes: new[]{"Skills", "ValidityDuration"});
            ViewBag.JobId = id;
            return View(jobTitle);
        }

        [HttpPost]
        public async Task<IActionResult> Apply(Guid id, UserApplyDto userApply)
        {
            var check = _jobTitleService.IsExisted(jobTitle => jobTitle.Id == id && 
                jobTitle.UserApplies.Any(user => user.Email == userApply.Email || user.MobileNumber == userApply.MobileNumber));

            if (check)
            {
                _toastNotification.AddSuccessToastMessage($"You have signed up for this profession before", new ToastrOptions()
                {
                    Title = "Error"
                });    
            }
            userApply.JobTitleId = id;
            var userUpply = await _userApplayedService.AddAsync(userApply);

            if (userApply is not null)
            {
                _toastNotification.AddSuccessToastMessage($"JobTitle {Constanties.SUCCESS_SAVED}", new ToastrOptions()
                {
                    Title = "Success"
                });
            }
            
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}