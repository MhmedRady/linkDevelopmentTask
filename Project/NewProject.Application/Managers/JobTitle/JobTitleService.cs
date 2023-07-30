using AutoMapper;
using NewProject.Domain;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using NewProject.Repositories;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace NewProject.Application;

public class JobTitleService : CrudGenericManager<Guid, JobTitle, JobTitleDto, CreateJobTitleInputDto>, IJobTitleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public JobTitleService(IMapper mapper, IUnitOfWork unitOfWork) : base(unitOfWork.JobTitleRepository, mapper)
    {
        
    }
}


