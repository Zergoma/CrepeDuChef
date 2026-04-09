using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Localization.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CrepeDuChef.Application.Validation
{
    public class UserDtoUpdateValidator : AbstractValidator<UserDtoUpdate>
    {
        public UserDtoUpdateValidator(IStringLocalizer<ValidationResources> localizer)
        {
            RuleFor(u => u.FirstName)
                .NotNull()
                .NotEmpty().WithMessage(localizer["FirstNameRequired"])
                .MaximumLength(50).WithMessage(localizer["FirstNameRequired"]);

            RuleFor(u => u.LastName)
                .NotNull()
                .NotEmpty().WithMessage(localizer["LastNameRequired"])
                .MaximumLength(50).WithMessage(localizer["LastNameLengthRange"]);

            RuleFor(u => u.Id).NotEqual(0)
                .WithMessage(localizer["IDIsNotValid"]);
        }
    }
}
