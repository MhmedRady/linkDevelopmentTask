using NewProject.Domain.BaseEntities;

namespace NewProject.Domain;

public class UserApply : BaseEntity<Guid>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string MobileNumber { get; set; }
    public Guid JobTitleId { get; set; }
    public JobTitle JobTitle { get; set; }
}