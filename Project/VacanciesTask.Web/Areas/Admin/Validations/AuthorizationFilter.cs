using System.Security.Claims;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewProject.Domain;

namespace VacanciesTask.Areas.Admin.Validations;

public class AuthorizationFilter : IDashboardAuthorizationFilter
{
    private readonly UserManager<User> _userManager;

    public AuthorizationFilter()
    {
    }
    public AuthorizationFilter(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public bool Authorize(DashboardContext context)
    {
        var auth = this.IsAdmin(context).Result;
        return auth;
    }

    public async Task<bool> IsAdmin(DashboardContext context)
    {
        var httpContext = context.GetHttpContext();
        if (httpContext.User.Identity.IsAuthenticated == false) return false;
        var log = httpContext.User.Claims;
        var user = await _userManager.FindByNameAsync(httpContext.User.Identity.Name);
        return await _userManager.IsInRoleAsync(user, "Admin");
    }
}