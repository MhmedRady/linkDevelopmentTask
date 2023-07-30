using Microsoft.AspNetCore.Mvc;
using VacanciesTask.Controllers;

namespace VacanciesTask.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class JobTitleController : _Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        ViewBag.Columns = new[] { "Name", "JobCategory", "MaximumApplications", "StartDate", "EndDate", "activate" };
        return View();
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
}