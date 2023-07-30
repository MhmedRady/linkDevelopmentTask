using System.ComponentModel.DataAnnotations.Schema;
using NewProject.Domain.BaseEntities;

namespace NewProject.Domain;

public class ValidityDuration : BaseEntity<Guid>
{
    public ValidityDuration()
    {
        Id = Guid.NewGuid();
    }
    
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public Guid JobTitleId { get; set; }
    public JobTitle JobTitle { get; set; }
}