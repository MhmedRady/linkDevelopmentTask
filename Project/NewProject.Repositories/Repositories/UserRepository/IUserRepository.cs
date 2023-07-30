using NewProject.Domain;
using NewProject.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Repositories;

public interface IUserRepository : IGeneralRepository<User, string>
{
    
}
