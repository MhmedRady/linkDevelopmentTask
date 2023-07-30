using FluentValidation;
using NewProject.Application;
using NewProject.Shared;

namespace NewProject.API.Validations
{
    public class UserLoginValidation : AbstractValidator<LoginDto>
    {
        public UserLoginValidation()
        {
            RuleFor(u=> u.Email).NotNull().WithMessage(Constanties.EMAIL_REQUIRED);
            RuleFor(u=> u.Email).EmailAddress();
            RuleFor(l => l.Password).NotNull().NotEmpty().WithMessage(Constanties.PASSWORD_REQUIRED);
           }
    }
}


