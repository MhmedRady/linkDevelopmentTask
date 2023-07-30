using NewProject.Domain;

namespace NewProject.Application;

public class ValidityDurationDto
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    // public Guid JobTitleId { get; set; }
    // public JobTitleDto JobTitle { get; set; }
}