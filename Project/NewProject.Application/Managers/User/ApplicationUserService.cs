using AutoMapper;
using NewProject.Domain;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using NewProject.Repositories;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using NewProject.Shared;

namespace NewProject.Application;

public class ApplicationUserService: IApplicationUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IGeneralRepository<User, string> _userRepository;
    private readonly IMapper _mapper;
    private readonly SignInManager<User> _SignInManager;
    IConfiguration Configuration;
    public ApplicationUserService(UserManager<User> userManager, IMapper mapper,
        SignInManager<User> signInManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager, IGeneralRepository<User, string> userRepository)
    {
        _userManager = userManager;
        _mapper = mapper;
        _SignInManager = signInManager;
        Configuration = configuration;
        _roleManager = roleManager;
        _userRepository = userRepository;
    }

    public async Task<UserDto> AddAsync(CreateUserInput dto, string role)
    {
        dto.CreatedAt = DateTime.Now;
        var entity = _mapper.Map<CreateUserInput, User>(dto);
        
        try
        {
            var insertUser = await _userManager.CreateAsync(entity, dto.PasswordHash);
            var user = await _userManager.FindByEmailAsync(dto.Email);
            var insertRole = await _userManager.AddToRoleAsync(user, role);
            if (insertUser.Succeeded && insertRole.Succeeded)
            {
                return _mapper.Map<User, UserDto>(user);
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            throw;
        }

        return null;
    }

    public async Task<LoginResultDto>? Login(LoginDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        var checkSignIn = await _SignInManager.PasswordSignInAsync(user.Email, model.Password, true, true);
        if (checkSignIn.Succeeded)
        {
            var result = new LoginResultDto();
            if (user is not null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                roles.ToList().ForEach(r => result.Roles.Add(r));
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim (ClaimTypes.Name,user.Email ?? ""),
                new Claim (ClaimTypes.NameIdentifier,user.Id),
            };
            
            foreach (var role in result.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            
            result.ClaimIdentity = new ClaimsIdentity(claims);
            return result;
        }
        return null;
    }
    
    public async Task Logout()
    {
        await _SignInManager.SignOutAsync();
    }
}


