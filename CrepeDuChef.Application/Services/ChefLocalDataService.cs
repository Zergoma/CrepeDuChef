using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.Mappers;
using CrepeDuChef.Application.Models.Requests.Chefs;
using CrepeDuChef.Domain.Entities;

namespace CrepeDuChef.Application.Services
{
    public class ChefLocalDataService : IChefDataService
    {
        private readonly IChefRepository _repo;
        private readonly IDateTimeProvider _dateTimeProvider;

        public ChefLocalDataService(
            IChefRepository repo,
            IDateTimeProvider dateTimeProvider)
        {
            _repo = repo;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<UserDto> AddChefAsync(AddChefClientRequest userAddRequest)
        {
            User user =
                new()
                {
                    FirstName = userAddRequest.FirstName,
                    LastName = userAddRequest.LastName,
                    OriginDeviceId = userAddRequest.OriginDeviceId,
                    UpdatedAt = _dateTimeProvider.UtcNow,
                };

            await _repo.AddChefAsync(user);

            return
                new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
        }

        public async Task<List<UserDto>> GetAllChefsAsync()
        {
            List<User> resu =
                await _repo.GetAllChefsAsync();

            return [.. resu.Select(u => u.ToDto())];
        }

        public async Task<UserDto?> UpdateChefAsync(Guid userId, UpdateChefClientRequest userUpdateRequest)
        {
            User? existing =
                await _repo.GetChefAsync(userId);

            if (existing is null)
                return null;

            existing.FirstName = userUpdateRequest.FirstName;
            existing.LastName = userUpdateRequest.LastName;
            existing.UpdatedAt = _dateTimeProvider.UtcNow;

            await _repo.UpdateChefAsync(existing);

            return
                new UserDto
                {
                    Id = existing.Id,
                    FirstName = existing.FirstName,
                    LastName = existing.LastName
                };
        }
    }
}
