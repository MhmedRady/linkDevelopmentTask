using NewProject.Domain;
using NewProject.Domain.BaseEntities;

namespace NewProject.Application;

public class JobTitleDto : BaseEntity<Guid>
{
    public string Name { get; set; } 
    /*public string Description { get; set; }
    public string Responsibilities { get; set; }*/
    public string JobCategory { get; set; }
    public int MaximumApplications { get; set; }

    public int SkillsCount
    {
        get;
        set;
    }
    
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public int UserApplyCount { get; set; }

    public ValidityDurationDto ValidityDuration { get; set; }
    public ICollection<SkillsDto> Skills { get; set; }
    public ICollection<UserApply> UserApplies { get; set; }
}