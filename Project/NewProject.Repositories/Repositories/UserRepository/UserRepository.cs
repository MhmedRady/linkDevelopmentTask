using NewProject.Domain;
using NewProject.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using NewProject.Shared;

namespace NewProject.Repositories;

public class UserRepository : GeneralRepository<User, string>, IUserRepository
{
    private readonly UserManager<User> _userManager;
    private readonly MainDbContext _DBContext;
    public UserRepository(MainDbContext DBContext) : base(DBContext)
    {
        _DBContext = DBContext;
    }
}
