using System.Linq.Expressions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using NewProject.API;
using NewProject.API.Extensions;
using NewProject.API.Filters;
using NewProject.Application;
using NewProject.Domain;
using NewProject.Repositories;
using NewProject.Shared;
using NToastNotify;
using NToastNotify.Helpers;
using VacanciesTask.Areas.Admin.Validations;
using VacanciesTask.Controllers;

namespace VacanciesTask.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class JobTitleController : _Controller
{
    private readonly IJobTitleService _jobTitleService;
    private readonly IToastNotification _toastNotification;
        
    
    public JobTitleController(IJobTitleService jobTitleService, IToastNotification toastNotification)
    {
        _jobTitleService = jobTitleService;
        _toastNotification = toastNotification;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Store(CreateJobTitleInputDto jobTitleInputDto)
    {
        try
        {
            JobTitleValidation validator = new JobTitleValidation();
            ValidationResult result = validator.Validate(jobTitleInputDto);
            if (!result.IsValid)
            {
                foreach (var err in result.Errors)
                {
                    _toastNotification.AddErrorToastMessage(err.ErrorMessage, new ToastrOptions()
                    {
                        Title = "Error"
                    });
                }
                return RedirectToAction("Create");
            }
            if (!string.IsNullOrEmpty(jobTitleInputDto._Skills))
            {
                jobTitleInputDto.Skills = jobTitleInputDto._Skills.Split(",")
                    .Select(item => new SkillsDto() { Name = item })
                    .ToList();
            }
            var jobTitleDto = _jobTitleService.Add(jobTitleInputDto);
            _toastNotification.AddSuccessToastMessage($"New JobTitle {Constanties.SUCCESS_SAVED}", new ToastrOptions()
            {
                Title = "Success"
            });
            return RedirectToAction("Index", controllerName: "Home", new { area = "Admin" });
        }
        catch (Exception e)
        {
            _toastNotification.AddErrorToastMessage(Constanties._ERROR, new ToastrOptions()
            {
                Title = "Error"
            });
            return RedirectToAction("Create");
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var jobDto = await _jobTitleService.GetWrite(jt=>jt.Id == id, "ValidityDuration", "Skills");
        jobDto._Skills = jobDto.Skills.Select(dto => dto.Name).Aggregate((a, b) => a + ", " + b);
        return View(jobDto);
    }

    [HttpPost]
    public async Task<ActionResult> Update(Guid? id, CreateJobTitleInputDto jobTitleInputDto)
    {
        
        try
        {
            var isExist = await _jobTitleService.IsExistedAsync(jobTitle =>  jobTitle.Id == id);
            if (!isExist)
            {
                _toastNotification.AddErrorToastMessage($"JobTitle {Constanties.NOTFOUND}", new ToastrOptions()
                {
                    Title = "Error"
                });
            }
            if (!string.IsNullOrEmpty(jobTitleInputDto._Skills))
            {   
                jobTitleInputDto.Skills = jobTitleInputDto._Skills.Split(",")
                    .Select(item => new SkillsDto() { Name = item })
                    .ToList();
            }

           await _jobTitleService.UpdateById(id.Value, jobTitleInputDto);
           _toastNotification.AddSuccessToastMessage($"JobTitle Upadated {Constanties.SUCCESS_SAVED}", new ToastrOptions()
           {
               Title = "Success"
           });
        }catch (Exception e)
        {
            _toastNotification.AddErrorToastMessage(Constanties._ERROR, new ToastrOptions()
            {
                Title = "Error"
            });
            return RedirectToAction("Create");
        }
        
        return RedirectToAction("Edit", new { id });
    }
    [HttpDelete]
    public async Task<ActionResult> Delete(Guid id)
    {
        JobTitleDto? jobTitleDto = await _jobTitleService.GetById(id);
        var result = new Dictionary<string, object>();
        
        if (jobTitleDto is null)
        {
            return Json(new { success = false, msg = Constanties.NOTFOUND});
        }
            
        bool del = await _jobTitleService.Remove(id);
        if (!del)
        {
            return Json(new { success = false, msg = Constanties.FAILED_DELETED});
        }
        
        return Json(new { success = true, msg = $"{jobTitleDto.Name} {Constanties.SUCCESS_DELETED}" });
    }

}