using NewProject.Domain.BaseEntities;

namespace NewProject.Application;

public class JobTitleList : BaseEntity<Guid>
{
    public string? Name { get; set; } 
    public string? JobCategory { get; set; }
    public int MaximumApplications { get; set; }
    public int? SkillsCount {get;set;}
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public int? UserApplyCount { get; set; }
}