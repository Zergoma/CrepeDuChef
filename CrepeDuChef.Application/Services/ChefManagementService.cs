using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Extensions;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.Mappers;
using CrepeDuChef.Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace CrepeDuChef.Application.Services
{
    public class ChefManagementService : IChefManagementService
    {
        private readonly ICrepePartyRepository _repo;
        private readonly IValidator<UserDtoAdd> _addValidator;
        private readonly IValidator<UserDtoUpdate> _updateValidator;

        public ChefManagementService(
            ICrepePartyRepository repo,
            IValidator<UserDtoAdd> addValidator,
            IValidator<UserDtoUpdate> updateValidator)
        {
            _repo = repo;
            _addValidator = addValidator;
            _updateValidator = updateValidator;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
            => [.. (await _repo.GetAllChefsAsync()).Select(u => u.ToDto())];

        public async Task<UserDto?> AddUserAsync(UserDtoAdd userDtoAdd)
        {
            ValidationResult validation = _addValidator.Validate(userDtoAdd);

            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }

            UserDto userDto = new()
            {
                FirstName = userDtoAdd.FirstName,
                LastName = userDtoAdd.LastName,
            };
            await _repo.AddChefAsync(userDto.ToEntity());
            return userDto;
        }

        public async Task<UserDto?> UpdateUserAsync(UserDto user, UserDtoUpdate userUpdate)
        {
            // Enforce good ID
            userUpdate.Id = user.Id;

            ValidationResult validation = _updateValidator.Validate(userUpdate);

            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }

            await _repo.UpdateChefAsync(userUpdate.ToDto().ToEntity());

            // update fields
            user.FirstName = userUpdate.FirstName;
            user.LastName = userUpdate.LastName;

            return user;
        }
    }
}
