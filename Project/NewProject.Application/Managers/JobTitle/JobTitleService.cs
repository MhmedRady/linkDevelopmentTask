using AutoMapper;
using NewProject.Domain;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using NewProject.Repositories;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using NewProject.Shared;

namespace NewProject.Application;

public class JobTitleService : CrudGenericManager<Guid, JobTitle, JobTitleDto, CreateJobTitleInputDto>, IJobTitleService
{
    private readonly IMapper _mapper;
    private IGeneralRepository<JobTitle, Guid> _jobRepository;
    public JobTitleService(IMapper mapper, IGeneralRepository<JobTitle, Guid> repository) : base(repository, mapper)
    {
        _mapper = mapper;
        _jobRepository = repository;
    }

    public async Task<IEnumerable<JobTitleList>> CountsOfJobTitle(Expression<Func<JobTitle, bool>> expression,  int? take, int? skip, Expression<Func<JobTitle, object>> orderBy, string orderDirection = Constanties.ORDERASC, params string[] includes)
    {
        var queryResult = _jobRepository.Get(expression)
            .Take(take.Value)
            .Skip(skip.Value)
            .OrderBy(x=>x.Id)
            .Select(x=> new JobTitleList()
        {
            Id = x.Id,
            Name = x.Name,
            UserApplyCount = x.UserApplies.Count(),
            SkillsCount = x.Skills.Count,
            StartDate = x.ValidityDuration.From.ToString("dd/MM/yyyy"),
            EndDate = x.ValidityDuration.To.ToString("dd/MM/yyyy"),
            JobCategory = x.JobCategory,
            MaximumApplications = x.MaximumApplications,
        });
        return _mapper.Map<IEnumerable<JobTitleList>>(queryResult);
    }
}


