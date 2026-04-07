using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CrepeDuChef.Application.Validation
{
    public class UserDtoAddValidator : AbstractValidator<UserDtoAdd>
    {
        public UserDtoAddValidator(IStringLocalizer<ValidationResources> localizer)
        {
            RuleFor(u => u.FirstName)
                .NotNull()
                .NotEmpty().WithMessage(localizer["FirstNameRequired"])
                .MaximumLength(50).WithMessage(localizer["FirstNameLengthRange"]);

            RuleFor(u => u.LastName)
                .NotNull()
                .NotEmpty().WithMessage(localizer["LastNameRequired"])
                .MaximumLength(50).WithMessage(localizer["LastNameLengthRange"]);
        }
    }
}
