using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.Mappers;
using CrepeDuChef.Common.DTOs;
using CrepeDuChef.Domain.Entities;

namespace CrepeDuChef.Application.Repositories
{
    /// <summary>
    /// Overlay application for MAUI ViewModels.
    /// Converts DTOs to entities and back, and automatically commits.
    /// </summary>
    public class CrepePartyRepositoryApplication : ICrepePartyRepositoryApplication
    {
        private readonly ICrepePartyRepositoryInfrastructure _crepePartyRepo;

        public CrepePartyRepositoryApplication(ICrepePartyRepositoryInfrastructure crepePartyRepo)
        {
            _crepePartyRepo = crepePartyRepo;
        }

        public async Task<List<UserDto>> GetAllChefsAsync()
        {
            List<User> entities =
                await _crepePartyRepo.GetAllChefsAsync();
            return [.. entities.Select(e => e.ToDto())];
        }

        public async Task<UserDto?> GetChefAsync(int id)
        {
            User? entity =
                await _crepePartyRepo.GetChefAsync(id);
            return entity?.ToDto();
        }

        public async Task AddChefAsync(UserDto userDto)
        {
            User entity =
                userDto.ToEntity();
            await _crepePartyRepo.AddChefAsync(entity);
        }

        public async Task UpdateChefAsync(UserDto userDto)
        {
            User entity =
                userDto.ToEntity();
            await _crepePartyRepo.UpdateChefAsync(entity);
        }

        public async Task AddCrepePartyAsync(CrepesPartyDto crepePartyDto)
        {
            CrepesParty entity =
                crepePartyDto.ToEntity();
            await _crepePartyRepo.AddCrepePartyAsync(entity);
        }

        public async Task<List<CrepesPartyDto>> GetAllCrepePartiesAsync()
        {
            List<CrepesParty> entities =
                await _crepePartyRepo.GetAllCrepePartiesAsync();
            return [.. entities.Select(e => e.ToDto())];
        }

        public async Task<List<CrepesPartyDto>> GetCrepePartiesFromSessionAsync(int sessionNumber)
        {
            List<CrepesParty> entities =
                await _crepePartyRepo.GetCrepePartiesFromSessionAsync(sessionNumber);
            return [.. entities.Select(e => e.ToDto())];
        }

        public async Task<int> GetLastSessionNumberAsync()
        {
            return await _crepePartyRepo.GetLastSessionNumberAsync();
        }
    }
}