using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Domain.Entities;
using CrepeDuChef.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CrepeDuChef.Infrastructure.Repositories
{
    public class ChefRepository : IChefRepository
    {
        private readonly IDbContextFactory<CrepeDbContext> _factory;

        public ChefRepository(IDbContextFactory<CrepeDbContext> factory)
        {
            _factory = factory;
        }

        public async Task AddAsync(Chef chef)
        {
            await using CrepeDbContext ctx = await _factory.CreateDbContextAsync();
            await ctx.Chefs.AddAsync(chef);
            await ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(Chef chef)
        {
            await using CrepeDbContext ctx = await _factory.CreateDbContextAsync();
            Chef stored = await ctx.Chefs.FirstAsync(c => c.Id == chef.Id);
            stored.FirstName = chef.FirstName;
            stored.LastName = chef.LastName;
            stored.UpdatedAt = chef.UpdatedAt;
            await ctx.SaveChangesAsync();
        }

        public async Task<Chef?> GetByIdAsync(Guid id)
        {
            await using CrepeDbContext ctx = await _factory.CreateDbContextAsync();
            return await ctx.Chefs.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Chef>> GetAllAsync()
        {
            await using CrepeDbContext ctx = await _factory.CreateDbContextAsync();
            return await ctx.Chefs.ToListAsync();
        }
    }
}
