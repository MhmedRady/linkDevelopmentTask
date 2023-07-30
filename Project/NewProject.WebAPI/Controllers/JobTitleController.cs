using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewProject.API;
using NewProject.API.Filters;
using NewProject.Application;
using NewProject.Domain;

namespace NewProject.WebAPI.Controllers;

public class JobTitleController : AppBaseController
{
    private readonly IJobTitleService _jobTitleService;

    public JobTitleController(IJobTitleService jobTitleService)
    {
        _jobTitleService = jobTitleService;
    }

    [HttpPost]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll([FromForm] JobTitleFilter filterModel)
    {
        Expression<Func<JobTitle, bool>> NameExpression = jobTitle => (!string.IsNullOrEmpty(filterModel.Name)? jobTitle.Name.Contains(filterModel.Name): true);
        
        var jobTitles = await _jobTitleService.GetAll(
                NameExpression, take:3, skip: filterModel.page,
                orderBy: jopTitle => jopTitle.Id, include: source => source
                .Include(a => a.ValidityDuration)
                .Include(a => a.Skills)
            );
        
        return jobTitles.Count() > 0? this.AppSuccess(data: jobTitles): this.AppNotFound();
    }
    
    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create([FromForm] CreateJobTitleInputDto model)
    {
        return this.AppSuccess("");
    }
}