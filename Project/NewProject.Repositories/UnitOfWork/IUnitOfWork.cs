using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewProject.Domain;

namespace NewProject.Repositories;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    IJobTitleRepository JobTitleRepository { get; }
    //IGeneralRepository<ValidityDuration, Guid> ValidityDurationRepository { get; }
    Task<int> SaveChangesAsync();
    int SaveChanges();
}
