using CrepeDuChef.Domain.Entities;
using CrepeDuChef.Localization.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CrepeDuChef.Application.Validation
{
    /// <summary>
    /// Name rules shared by the add and update validators, so both stay in sync.
    /// </summary>
    public static class ChefNameRules
    {
        public static IRuleBuilderOptions<T, string> ChefFirstName<T>(
            this IRuleBuilder<T, string> rule,
            IStringLocalizer<ValidationResources> localizer)
        {
            return rule
                .NotEmpty().WithMessage(localizer["FirstNameRequired"])
                .MaximumLength(Chef.NameMaxLength).WithMessage(localizer["FirstNameLengthRange"]);
        }

        public static IRuleBuilderOptions<T, string> ChefLastName<T>(
            this IRuleBuilder<T, string> rule,
            IStringLocalizer<ValidationResources> localizer)
        {
            return rule
                .NotEmpty().WithMessage(localizer["LastNameRequired"])
                .MaximumLength(Chef.NameMaxLength).WithMessage(localizer["LastNameLengthRange"]);
        }
    }
}
