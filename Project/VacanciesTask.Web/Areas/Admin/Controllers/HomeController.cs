using System.Diagnostics;
using System.Linq.Expressions;
using VacanciesTask.Controllers;
using VacanciesTask.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewProject.API.Extensions;
using NewProject.Application;
using NewProject.Domain;
using NToastNotify;
using NToastNotify.Helpers;

namespace VacanciesTask.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : _Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IToastNotification _toastNotification;
        private readonly IJobTitleService _jobTitleService;
        public HomeController(ILogger<HomeController> logger, IJobTitleService jobTitleService)
        {
            _logger = logger;
            _jobTitleService = jobTitleService;
        }

        public async Task<IActionResult> Index()
        {
            if (Request.IsNtoastNotifyAjaxRequest())
            {
                var dataTableRequest = this.GetPagedData();
                Expression<Func<JobTitle, bool>> NameExpression = jobTitle => (!string.IsNullOrEmpty(dataTableRequest["searchValue"])? jobTitle.Name.Contains(dataTableRequest["searchValue"]): true);
                var JobTitles = await _jobTitleService.CountsOfJobTitle(expression: NameExpression, take:3, skip: Int32.Parse(dataTableRequest["start"]), jt => jt.Id);
                var total = _jobTitleService.Count(NameExpression);
                return Json(new 
                {
                    recordsFiltered = total, 
                    recordsTotal = total, 
                    data = JobTitles,
                    pageSize = 3,
                });
            }
            
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