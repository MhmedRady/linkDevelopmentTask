
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewProject.Application;

namespace NewProject.DBL;

public class UnitOfManager : IUnitOfManager
{
    public IApplicationUserService UserService { get; }
    

    public UnitOfManager(IApplicationUserService userService)
    {
        UserService = userService;
    }
}
