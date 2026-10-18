using AppInterface = CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Domain.Entities;
using CrepeDuChef.Infrastructure.DbContexts;

using Microsoft.EntityFrameworkCore;

namespace CrepeDuChef.Infrastructure.Repositories
{
    public class ChefRepository : AppInterface.IChefRepository
    {
        private readonly IDbContextFactory<CrepeDbContext> _factory;

        public ChefRepository(IDbContextFactory<CrepeDbContext> factory)
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
            var existing = await ctx.Users.FirstAsync(u => u.Id == user.Id);

            existing.FirstName = user.FirstName;
            existing.LastName = user.LastName;
            existing.UpdatedAt = user.UpdatedAt;

            await ctx.SaveChangesAsync();
        }

        public async Task<User?> GetChefAsync(Guid id)
        {
            await using var ctx = await _factory.CreateDbContextAsync();
            return await ctx.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<User>> GetAllChefsAsync()
        {
            await using var ctx = await _factory.CreateDbContextAsync();
            return [.. await ctx.Users.ToListAsync()];
        }
    }
}
