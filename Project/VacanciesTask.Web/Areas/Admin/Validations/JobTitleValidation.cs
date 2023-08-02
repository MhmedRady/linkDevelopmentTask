using FluentValidation;
using NewProject.Application;
using NewProject.Shared;

namespace VacanciesTask.Areas.Admin.Validations;

public class JobTitleValidation : AbstractValidator<CreateJobTitleInputDto>
{
    public JobTitleValidation()
    {
        RuleFor(jt => jt.Name).NotNull().WithMessage(Constanties.NAME_REQUIRED)
            .MinimumLength(5).WithMessage("JobTitle Name Must Be more than 4 letters.");
        RuleFor(jt => jt._Skills).NotNull().WithMessage("JobTitle Skills Can't Be Empty.");
        RuleFor(jt => jt.Description).NotNull().WithMessage("JobTitle Description Can't Be Empty.")
            .MinimumLength(5).WithMessage("JobTitle Description Must Be more than 4 letters.");
        RuleFor(jt => jt.JobCategory).NotNull().WithMessage("JobTitle JobCategory Can't Be Empty.")
            .MinimumLength(5).WithMessage("JobTitle JobCategory Must Be more than 4 letters.");
        RuleFor(jt => jt.Responsibilities).NotNull().WithMessage("JobTitle Responsibilities Can't Be Empty.")
            .MinimumLength(5).WithMessage("JobTitle Responsibilities Must Be more than 4 letters.");
        
        RuleFor(l => l.ValidityDuration).SetValidator(new ValidityDurationValidation());
        
    }
}