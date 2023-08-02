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
    private readonly MainDbContext _DBContext;
    public UnitOfWork(MainDbContext DBContext)
    {
        _DBContext = DBContext;
    }

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
