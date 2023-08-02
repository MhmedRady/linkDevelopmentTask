using NewProject.Domain.BaseEntities;

namespace NewProject.Application;

public class CreateJobTitleInputDto
{
    public string Name { get; set; } 
    public string Description { get; set; }
    public string Responsibilities { get; set; }
    public string JobCategory { get; set; }
    public int MaximumApplications { get; set; }
    public string _Skills
    {
        get;
        set;
    }

    public ICollection<SkillsDto> Skills { get; set; }
    
    public ValidityDurationDto ValidityDuration { get; set; }
}