using CrepeDuChef.Application.Common;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.Mappers;
using CrepeDuChef.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace CrepeDuChef.Application.Services
{
    public class ChefService : IChefService
    {
        private readonly IChefRepository _chefs;
        private readonly IValidator<AddChefDto> _addValidator;
        private readonly IValidator<UpdateChefDto> _updateValidator;
        private readonly IDeviceIdProvider _deviceId;
        private readonly IDateTimeProvider _dateTime;

        public ChefService(
            IChefRepository chefs,
            IValidator<AddChefDto> addValidator,
            IValidator<UpdateChefDto> updateValidator,
            IDeviceIdProvider deviceId,
            IDateTimeProvider dateTime)
        {
            _chefs = chefs;
            _addValidator = addValidator;
            _updateValidator = updateValidator;
            _deviceId = deviceId;
            _dateTime = dateTime;
        }

        public async Task<IReadOnlyList<ChefDto>> GetAllAsync()
            => [.. (await _chefs.GetAllAsync()).Select(c => c.ToDto())];

        public async Task<Result<ChefDto>> AddAsync(AddChefDto chef)
        {
            ValidationResult validation = await _addValidator.ValidateAsync(chef);
            if (!validation.IsValid)
            {
                return Failure(validation);
            }

            Chef entity = new()
            {
                Id = Guid.NewGuid(),
                FirstName = chef.FirstName,
                LastName = chef.LastName,
                UpdatedAt = _dateTime.UtcNow,
                OriginDeviceId = _deviceId.DeviceId,
            };

            await _chefs.AddAsync(entity);

            return Result<ChefDto>.Success(entity.ToDto());
        }

        public async Task<Result<ChefDto>> UpdateAsync(UpdateChefDto chef)
        {
            ValidationResult validation = await _updateValidator.ValidateAsync(chef);
            if (!validation.IsValid)
            {
                return Failure(validation);
            }

            Chef entity = new()
            {
                Id = chef.Id,
                FirstName = chef.FirstName,
                LastName = chef.LastName,
                UpdatedAt = _dateTime.UtcNow,
                OriginDeviceId = _deviceId.DeviceId,
            };

            await _chefs.UpdateAsync(entity);

            return Result<ChefDto>.Success(entity.ToDto());
        }

        private static Result<ChefDto> Failure(ValidationResult validation)
            => Result<ChefDto>.Failure(validation.Errors.Select(e => e.ErrorMessage));
    }
}
