using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Localization.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CrepeDuChef.Application.Validation
{
    public class UpdateChefDtoValidator : AbstractValidator<UpdateChefDto>
    {
        public UpdateChefDtoValidator(IStringLocalizer<ValidationResources> localizer)
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.FirstName).ChefFirstName(localizer);
            RuleFor(c => c.LastName).ChefLastName(localizer);
        }
    }
}
