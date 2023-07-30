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
    
}
