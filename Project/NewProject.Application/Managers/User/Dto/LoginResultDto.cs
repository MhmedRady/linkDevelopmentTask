using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Application;

public class LoginResultDto
{
    public string? UserId { get; set; }
    public string? FullName { get; set; }
    public string? Token { get; set; }
    public DateTime? ExpiresIn { get; set; }
    public List<string>? Roles { get; set; }
    
    public ClaimsIdentity? ClaimIdentity { get; set; }
}
