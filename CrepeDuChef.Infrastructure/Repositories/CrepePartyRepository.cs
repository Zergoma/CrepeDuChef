using AppInterface = CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using CrepeDuChef.Infrastructure.DbContexts;

namespace CrepeDuChef.Infrastructure.Repositories
{
    public class CrepePartyRepository : AppInterface.ICrepePartyRepository
    {
        private readonly IDbContextFactory<CrepeDbContext> _factory;

        public CrepePartyRepository(IDbContextFactory<CrepeDbContext> factory)
        {
            _factory = factory;
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


        //public async Task<int> GetLastSessionNumberAsync()
        //{
        //    await using var ctx = await _factory.CreateDbContextAsync();
        //    return await ctx.CrepesParty.AnyAsync()
        //                 ? ctx.CrepesParty.Max(u => u.SessionNumber)
        //                 : 0;
        //}

        //public async Task<List<CrepesParty>> GetCrepePartiesFromSessionAsync(int SessionNumber)
        //{
        //    await using var ctx = await _factory.CreateDbContextAsync();
        //    return [.. await ctx.CrepesParty.Where(p => p.SessionNumber == SessionNumber)
        //                              .OrderBy(c => c.Date)
        //                              .ToListAsync()];
        //}
    }
}
