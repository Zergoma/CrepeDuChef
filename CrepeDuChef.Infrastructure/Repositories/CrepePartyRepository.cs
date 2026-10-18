using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Domain.Entities;
using CrepeDuChef.Infrastructure.Persistence;
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

        public async Task AddAsync(CrepeParty crepeParty)
        {
            await using CrepeDbContext ctx = await _factory.CreateDbContextAsync();
            await ctx.CrepeParties.AddAsync(crepeParty);
            await ctx.SaveChangesAsync();
        }

        public async Task<List<CrepeParty>> GetAllAsync()
        {
            await using CrepeDbContext ctx = await _factory.CreateDbContextAsync();
            return await ctx.CrepeParties.ToListAsync();
        }

        public async Task<List<CrepeParty>> GetBySessionAsync(int sessionNumber)
        {
            await using CrepeDbContext ctx = await _factory.CreateDbContextAsync();
            return await ctx.CrepeParties
                .Where(cp => cp.SessionNumber == sessionNumber)
                .OrderBy(cp => cp.Date)
                .ToListAsync();
        }

        public async Task<int> GetLastSessionNumberAsync()
        {
            await using CrepeDbContext ctx = await _factory.CreateDbContextAsync();
            return await ctx.CrepeParties.MaxAsync(cp => (int?)cp.SessionNumber) ?? 0;
        }
    }
}
