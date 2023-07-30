using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewProject.Domain;
using NewProject.EntityFrameworkCore;

namespace NewProject.Repositories;

public class UnitOfWork : IUnitOfWork
{
    MainDbContext _DBContext;
    public UnitOfWork(MainDbContext DBContext)
    {
        _DBContext = DBContext;
     
        UserRepository = new UserRepository(_DBContext);
        JobTitleRepository = new JobTitleRepository(_DBContext);
        //ValidityDurationRepository = new GeneralRepository<ValidityDuration, Guid>(_DBContext);
    }
    
    public IUserRepository UserRepository { get; private set; }
    public IJobTitleRepository JobTitleRepository { get; private set; }
    //public IGeneralRepository<ValidityDuration, Guid> ValidityDurationRepository { get; }

    public async Task<int> SaveChangesAsync()
    {
        return await _DBContext.SaveChangesAsync();
    }
    
    public int SaveChanges()
    {
        return _DBContext.SaveChanges();
    }
    
    public void Dispose()
    {
        _DBContext.Dispose();
        GC.SuppressFinalize(this);
    }
    
}
