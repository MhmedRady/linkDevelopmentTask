using System.Security.Claims;
using VacanciesTask.Controllers;
using VacanciesTask.Validations;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewProject.API;
using NewProject.Application;
using NewProject.DBL;
using NewProject.Domain;
using NewProject.Shared;
using NToastNotify;

namespace VacanciesTask.Web.Controllers;

public class AccountController: _Controller
{
    private readonly UserManager<User> _userManager;
    private IToastNotification _toastNotification;

    private readonly SignInManager<User> _SignInManager;
    
    public AccountController(IToastNotification toastNotification, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _toastNotification = toastNotification;
        _userManager = userManager;
        _SignInManager = signInManager;
    }
    
    [HttpGet]
    public IActionResult Login()
    {
        _toastNotification.AddErrorToastMessage(Constanties._ERROR, new ToastrOptions()
        {
            Title = "Error"
        });
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }
        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Login");
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginDto model)
    {
        
        var user = await _userManager.FindByEmailAsync(model.Email.Trim().ToLower());
        
        if (user is null)
        {
            TempData["error_msg"] = Constanties.USER_NOT_EXIST;
            return RedirectToAction("Login");
        }
        
        var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
        
        if (!isAdmin)
        {
            TempData["error_msg"] = Constanties.USER_NOT_EXIST;
            return RedirectToAction("Login");
        }
        var checkSignIn = await _SignInManager
            .PasswordSignInAsync(user.UserName, model.Password, true, true);
        if (checkSignIn.Succeeded)
        {
            if (user is not null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                List<Claim> claims = new List<Claim>()
                {
                    new Claim (ClaimTypes.Name,user.Email ?? ""),
                    new Claim (ClaimTypes.NameIdentifier,user.Id),
                };
                roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
                var identityClam = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(identityClam);
                await HttpContext.SignInAsync(claimsPrincipal);
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
        }
        TempData["error_msg"] = Constanties.USER_NOT_EXIST;
        return RedirectToAction("Login");
    }
    
    [HttpGet]
    public async Task<IActionResult> Register()
    {
        ViewBag.courses = new SelectList( 
            "Id", "name", null);
        return View();
    }

    [HttpPost]
     public async Task<IActionResult> Register(CreateUserInput model)
     {
         UserRegisterValidation validator = new UserRegisterValidation();
         
         try
         {
             /*UserDto? user = await _unitOfManager.UserManager.AddAsync(model,"User");
                if (user is not null)
             {
                 return RedirectToAction("Login");
             }*/
         }
         catch (Exception e)
         {
             //this.ErrorToasterMsg(e.Message);
         }
         return RedirectToAction("Register");
     }

     [HttpGet]
     [Authorize]
     public async Task<IActionResult> AccessDenied()
     {
         ViewBag.User = await _userManager.GetUserAsync(User);
         ViewBag.IsAdmin = await _userManager.IsInRoleAsync(ViewBag.User, "admin");
         return View();
     }
     
}