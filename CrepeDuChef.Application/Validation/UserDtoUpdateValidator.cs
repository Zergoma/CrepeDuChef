using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Domain.Entities;
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
                .MaximumLength(User.NameMaxLength).WithMessage(localizer["FirstNameLengthRange"]);

            RuleFor(u => u.LastName)
                .NotNull()
                .NotEmpty().WithMessage(localizer["LastNameRequired"])
                .MaximumLength(User.NameMaxLength).WithMessage(localizer["LastNameLengthRange"]);
        }
    }
}
