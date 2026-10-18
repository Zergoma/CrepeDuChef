using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Localization.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CrepeDuChef.Application.Validation
{
    public class AddChefDtoValidator : AbstractValidator<AddChefDto>
    {
        public AddChefDtoValidator(IStringLocalizer<ValidationResources> localizer)
        {
            RuleFor(c => c.FirstName).ChefFirstName(localizer);
            RuleFor(c => c.LastName).ChefLastName(localizer);
        }
    }
}
