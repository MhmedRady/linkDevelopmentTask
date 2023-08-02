using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewProject.Application;
using NewProject.Domain;
using NewProject.EntityFrameworkCore;
using NewProject.EntityFrameworkCore.Seeding;
using NewProject.Repositories;
using System.Text.Json.Serialization;
using VacanciesTask;
using VacanciesTask.Areas.Admin.Validations;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Localization;
using NToastNotify;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region IdentityOptions

builder.Services.Configure<IdentityOptions>(p =>
{
    p.Password.RequiredLength = 8;
    p.Password.RequireDigit = true;
    p.Password.RequireUppercase = false;
    p.Password.RequiredUniqueChars = 0;
    p.Password.RequireNonAlphanumeric = false;
    p.Password.RequireLowercase = true;
    p.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
    p.Lockout.MaxFailedAccessAttempts = 5;
});
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<MainDbContext>()
    .AddRoles<IdentityRole>()
    .AddDefaultTokenProviders();
#endregion

#region SqlServise
builder.Services.AddDbContext<MainDbContext>(db =>
{
    db.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

#endregion

#region auth
builder.Services.AddAuthentication()
    .AddCookie(a =>
    {
        a.LoginPath = "/Account/login";
        a.LogoutPath = "/Account/logout";
        a.ForwardSignOut = "/Account/logout";
    });
#endregion

#region mapper
    builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
#endregion

#region Managers
builder.Services.AddScoped<IJobTitleService, JobTitleService>();
builder.Services.AddScoped<IUserApplayedService, UserApplayedService>();
builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();
#endregion

#region repositories;
builder.Services.AddScoped(typeof(IGeneralRepository <,> ), typeof(GeneralRepository <,> ));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
#endregion
#region UnitOfWork
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion

#region Services

builder.Services.AddControllersWithViews().AddNToastNotifyToastr(
    new ToastrOptions()
    {
        Rtl = true,
        TimeOut = 4000,
        ProgressBar = true,
        CloseButton = true,
        CloseOnHover = false,
        PositionClass = "toast-bottom-left",
    });

builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions
        .ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddLocalization(o => { o.ResourcesPath = "Resources"; });
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

/*app.Use(async (context, next) =>
{
    context.Response.Cookies.Append
    (
        CookieRequestCultureProvider.DefaultCookieName,
        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("ar-EG")),
        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
    );
    // Call the next delegate/middleware in the pipeline.
    await next(context);
});*/

app.UseStaticFiles();

app.UseRouting();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

//app.UseStatusCodePagesWithRedirects("/Error/{0}");

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseNToastNotify();

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await DataInitialize.Initialize(app);

app.Run();