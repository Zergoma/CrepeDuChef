using CrepeDuChef.Domain.Entities;
using CrepeDuChef.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrepeDuChef.Infrastructure.Repositories
{
    public class CrepePartyRepository : ICrepePartyRepository
    {
        private readonly IDbContextFactory<CrepeDbContext> _factory;

        public CrepePartyRepository(IDbContextFactory<CrepeDbContext> factory)
        {
            _factory = factory;
        }

        public async Task AddChefAsync(User user)
        {
            await using var ctx = await _factory.CreateDbContextAsync();
            await ctx.Users.AddAsync(user);
            await ctx.SaveChangesAsync();
        }        
        public async Task UpdateChefAsync(User user)
        {
            await using var ctx = await _factory.CreateDbContextAsync();
            User userToUpdate = await ctx.Users.FirstAsync(u => u.Id == user.Id);
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            await ctx.SaveChangesAsync();
        }

        public async Task<User?> GetChefAsync(int id)
        {
            await using var ctx = await _factory.CreateDbContextAsync();
            return await ctx.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<User>> GetAllChefsAsync()
        {
            await using var ctx = await _factory.CreateDbContextAsync();
            return [.. await ctx.Users.ToListAsync()];
        }

        public async Task AddCrepePartyAsync(CrepesParty crepeParty)
        {
            await using var ctx = await _factory.CreateDbContextAsync();
            crepeParty.User = await ctx.Users.FirstAsync(u => u.Id == crepeParty.UserId);
            await ctx.CrepesParty.AddAsync(crepeParty);
            await ctx.SaveChangesAsync();
        }

        public async Task<List<CrepesParty>> GetAllCrepePartiesAsync()
        {
            await using var ctx = await _factory.CreateDbContextAsync();
            return [.. await ctx.CrepesParty.ToListAsync()];
        }


        public async Task<int> GetLastSessionNumberAsync()
        {
            await using var ctx = await _factory.CreateDbContextAsync();
            return await ctx.CrepesParty.AnyAsync()
                         ? ctx.CrepesParty.Max(u => u.SessionNumber)
                         : 0;
        }

        public async Task<List<CrepesParty>> GetCrepePartiesFromSessionAsync(int SessionNumber)
        {
            await using var ctx = await _factory.CreateDbContextAsync();
            return [.. await ctx.CrepesParty.Where(p => p.SessionNumber == SessionNumber)
                                      .OrderBy(c => c.Date)
                                      .ToListAsync()];
        }
    }
}
