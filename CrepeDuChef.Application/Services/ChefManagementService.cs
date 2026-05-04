using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Extensions;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.Mappers;

using FluentValidation;
using FluentValidation.Results;

namespace CrepeDuChef.Application.Services
{
    public class ChefManagementService : IChefManagementService
    {
        private readonly ICrepePartyRepository _repo;
        private readonly IValidator<UserDtoAdd> _addValidator;
        private readonly IValidator<UserDtoUpdate> _updateValidator;
        private readonly IDeviceIdProvider _deviceId;
        private readonly IDateTimeProvider _dateTime;

        public ChefManagementService(
            ICrepePartyRepository repo,
            IValidator<UserDtoAdd> addValidator,
            IValidator<UserDtoUpdate> updateValidator,
            IDeviceIdProvider deviceId,
            IDateTimeProvider dateTime)
        {
            _repo = repo;
            _addValidator = addValidator;
            _updateValidator = updateValidator;
            _deviceId = deviceId;
            _dateTime = dateTime;
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

            var entity = userDto.ToEntity(_deviceId.DeviceId);
            entity.Id = Guid.NewGuid();
            entity.UpdatedAt = _dateTime.UtcNow;

            await _repo.AddChefAsync(entity);
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

            var entity = userUpdate.ToDto().ToEntity(_deviceId.DeviceId);
            entity.UpdatedAt = _dateTime.UtcNow;

            await _repo.UpdateChefAsync(entity);

            // update fields
            user.FirstName = userUpdate.FirstName;
            user.LastName = userUpdate.LastName;

            return user;
        }
    }
}
