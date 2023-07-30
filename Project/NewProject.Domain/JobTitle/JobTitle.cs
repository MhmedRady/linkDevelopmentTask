using NewProject.Domain.BaseEntities;

namespace NewProject.Domain;

public class JobTitle : BaseEntity<Guid>
{
    public string Name { get; set; } 
    public string Description { get; set; }
    public string Responsibilities { get; set; }
    public string JobCategory { get; set; }
    public int MaximumApplications { get; set; }
    public ValidityDuration ValidityDuration { get; set; }
    public ICollection<Skills> Skills { get; set; }
    public ICollection<UserApply> UserApplies { get; set; }
    public JobTitle()
    {
        Id = Guid.NewGuid();
        Skills = new HashSet<Skills>();
        UserApplies = new HashSet<UserApply>();
    }
}