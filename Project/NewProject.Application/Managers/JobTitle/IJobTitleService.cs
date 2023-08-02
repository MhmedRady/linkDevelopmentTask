using NewProject.Domain;
using NewProject.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;

namespace NewProject.Application;

public interface IJobTitleService : ICrudGenericManager<Guid, JobTitle, JobTitleDto, CreateJobTitleInputDto>
{
    public Task<IEnumerable<JobTitleList>> CountsOfJobTitle(Expression<Func<JobTitle, bool>> expression, int? take,
        int? skip, Expression<Func<JobTitle, object>> orderBy, string orderDirection = Constanties.ORDERASC,
        params string[] includes);
}
