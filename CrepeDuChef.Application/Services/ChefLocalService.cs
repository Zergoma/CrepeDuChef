using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Domain.Entities;

namespace CrepeDuChef.Application.Services
{
    public class ChefLocalService : IChefLocalService
    {
        private readonly IChefRepository _chefRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public ChefLocalService(
            IChefRepository chefRepository,
            IDateTimeProvider dateTimeProvider)
        {
            _chefRepository = chefRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public Task<List<User>> GetAllChefsAsync()
            => _chefRepository.GetAllChefsAsync();


        public async Task<User> AddChefAsync(string firstName, string lastName, Guid OriginDeviceId)
        {
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                OriginDeviceId = OriginDeviceId,
                UpdatedAt = _dateTimeProvider.UtcNow,
            };

            await _chefRepository.AddChefAsync(newUser);
            return newUser;
        }

        public async Task<User?> UpdateChefAsync(Guid id, string firstName, string lastName)
        {
            var user = await _chefRepository.GetChefAsync(id);
            if (user is null)
                return null;

            user.FirstName = firstName;
            user.LastName = lastName;
            user.UpdatedAt = _dateTimeProvider.UtcNow;

            await _chefRepository.UpdateChefAsync(user);
            return user;
        }
    }
}
