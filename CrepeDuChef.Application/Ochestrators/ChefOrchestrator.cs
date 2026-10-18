using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Extensions;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.Mappers;

using CrepeDuChef.Domain.Entities;

using FluentValidation;
using FluentValidation.Results;

namespace CrepeDuChef.Application.Ochestrators
{
    public class ChefOrchestrator : IChefOrchestrator
    {
        private readonly IValidator<UserDtoAdd> _addValidator;
        private readonly IValidator<UserDtoUpdate> _updateValidator;
        private readonly IDeviceIdProvider _deviceId;
        private readonly IChefLocalService _chefs;

        public ChefOrchestrator(
            IValidator<UserDtoAdd> addValidator,
            IValidator<UserDtoUpdate> updateValidator,
            IDeviceIdProvider deviceId,
            IDateTimeProvider dateTime,
            IChefLocalService chefs)
        {
            _addValidator = addValidator;
            _updateValidator = updateValidator;
            _deviceId = deviceId;
            _chefs = chefs;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            List<UserDto> usersDto = 
                (await _chefs.GetAllChefsAsync())
                .Select(u => u.ToDto())
                .ToList();

            return usersDto;
        }

        public async Task<UserDto?> AddUserAsync(UserDtoAdd userDtoAdd)
        {
            ValidationResult validation = _addValidator.Validate(userDtoAdd);

            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }

            User user = 
                await _chefs.AddChefAsync(userDtoAdd.FirstName, userDtoAdd.LastName, _deviceId.DeviceId);
            
            return new()
            {
                Id = user.Id,    
                FirstName = userDtoAdd.FirstName,
                LastName = userDtoAdd.LastName,
            };
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

            await _chefs.UpdateChefAsync(
                userUpdate.Id,
                firstName: userUpdate.FirstName,
                lastName: userUpdate.LastName);

            // update fields
            user.FirstName = userUpdate.FirstName;
            user.LastName = userUpdate.LastName;

            return user;
        }
    }
}
