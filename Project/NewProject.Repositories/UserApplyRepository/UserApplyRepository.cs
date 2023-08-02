using NewProject.Domain;
using NewProject.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using NewProject.Shared;

namespace NewProject.Repositories;

public class UserApplyRepository : GeneralRepository<UserApply, Guid>, IUserApplyRepository
{
    private readonly MainDbContext _DBContext;
    public UserApplyRepository(MainDbContext dbContext) : base(dbContext)
    {
        _DBContext = dbContext;
    }
}
