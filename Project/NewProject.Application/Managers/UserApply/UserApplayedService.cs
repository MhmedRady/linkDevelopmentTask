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

public class UserApplayedService : CrudGenericManager<Guid, UserApply, UserApplyDto, UserApplyDto>, IUserApplayedService
{
    
    private readonly IGeneralRepository<UserApply, Guid> _userRepository;
    private readonly IMapper _mapper;
    public UserApplayedService(IMapper mapper, IGeneralRepository<UserApply, Guid> userRepository) : base( userRepository,mapper)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

}


