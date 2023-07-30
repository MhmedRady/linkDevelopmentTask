using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NewProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppBaseController : ControllerBase
    {
        public string UserId
        {
            get => User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
