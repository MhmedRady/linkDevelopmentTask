using NewProject.Domain.BaseEntities;

namespace NewProject.Domain;

public class Skills : BaseEntity<Guid>
{
    public string Name { get; set; }
    public Guid JobTitleId { get; set; }
    public JobTitle JobTitle { get; set; }
}