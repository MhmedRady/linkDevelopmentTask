using NewProject.Domain;

namespace NewProject.Application;

public class JobTitleDto
{
    public string Name { get; set; } 
    public string Description { get; set; }
    public string Responsibilities { get; set; }
    public string JobCategory { get; set; }
    public int MaximumApplications { get; set; }

    public int SkillsCont
    {
        get => Skills.Count();
    }
    
    public string StartDate { get => ValidityDuration.From.ToString(@"yyyy-MM-dd"); }
    public string EndDate { get => ValidityDuration.To.ToString(@"yyyy-MM-dd"); }

    public ValidityDurationDto ValidityDuration { get; set; }
    public ICollection<SkillsDto> Skills { get; set; }
}