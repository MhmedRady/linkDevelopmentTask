using NewProject.Domain;
using NewProject.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NewProject.Shared;

namespace NewProject.Repositories;

public class JobTitleRepository : GeneralRepository<JobTitle, Guid>, IJobTitleRepository
{
    private readonly MainDbContext _DBContext;
    public JobTitleRepository(MainDbContext dbContext) : base(dbContext)
    {
        _DBContext = dbContext;
    }
}
