using NewProject.Domain;
using NewProject.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;

namespace NewProject.Application;

public interface IApplicationUserService : ICrudGenericManager<string, User, UserDto, CreateUserInput>
{
    public Task<UserDto>? AddAsync(CreateUserInput dto, string role);
    
    Task<LoginResultDto> Login(LoginDto model);
    Task Logout();
}
