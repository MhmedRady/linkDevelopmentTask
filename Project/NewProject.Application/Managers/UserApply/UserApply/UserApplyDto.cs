using NewProject.Domain;

namespace NewProject.Application;

public class UserApplyDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string MobileNumber { get; set; }
    public Guid JobTitleId { get; set; }
}