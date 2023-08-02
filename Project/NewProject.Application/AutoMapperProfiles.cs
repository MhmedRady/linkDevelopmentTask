using AutoMapper;
using NewProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Application;

public class AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserInput, User>().ForMember(x=>x.UserName,op=>op.MapFrom(z=>z.Email));
            CreateMap<User, UserDto>()
                .ForMember(x => x.RolesName, xx => xx.Ignore());
        }
    }
    
    public class JobTitleProfile : Profile
    {
        public JobTitleProfile()
        {
            CreateMap<CreateJobTitleInputDto, JobTitle>().ReverseMap();

            CreateMap<JobTitle, JobTitleList>();
            CreateMap<JobTitle, JobTitleDto>();
        }
    }
    
    public class ValidityDurationProfile : Profile
    {
        public ValidityDurationProfile()
        {
            CreateMap<ValidityDurationDto, ValidityDuration>().ReverseMap();
        }
    }
    
    public class SkillsProfile : Profile
    {
        public SkillsProfile()
        {
            CreateMap<SkillsDto, Skills>().ReverseMap();
        }
    }
    
    public class UserApplyProfile : Profile
    {
        public UserApplyProfile()
        {
            CreateMap<UserApplyDto, UserApply>().ReverseMap();
        }
    }
}