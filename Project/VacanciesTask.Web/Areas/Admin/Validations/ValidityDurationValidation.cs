using FluentValidation;
using NewProject.Application;

namespace VacanciesTask.Areas.Admin.Validations;

public class ValidityDurationValidation: AbstractValidator<ValidityDurationDto>
{
    public ValidityDurationValidation()
    {
        RuleFor(VD => VD.From).NotNull().WithMessage("JobTitle Start Validity Date Can't Be Empty.");
        RuleFor(VD => VD.To).NotNull().WithMessage("JobTitle End Validity Date Can't Be Empty.");

        RuleFor(VD => VD)
            .Must(durationDate => durationDate.From >= DateTime.Today && durationDate.From < durationDate.To)
            .WithMessage("Start Validity Duration Date Can't be in the past Or after Validity End Date.")
            .Must(durationDate => durationDate.To > DateTime.Today && durationDate.From < durationDate.To);
    }
}