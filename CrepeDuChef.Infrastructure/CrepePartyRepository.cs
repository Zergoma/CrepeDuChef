using CrepeDuChef.Common.DTOs;
using CrepeDuChef.Common.Interfaces;
using CrepeDuChef.Infrastructure.Entities;
using CrepeDuChef.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CrepeDuChef.Infrastructure
{
    public class CrepePartyRepository : ICrepePartyRepository
    {
        private CrepeDbContext ctx { get; }

        public CrepePartyRepository(CrepeDbContext CrepeDbContext)
        {
            ctx = CrepeDbContext;
        }
        public async Task CommitAsync()
        {
            //foreach (var u in ctx.Users.Local)
            //{
            //    Console.WriteLine($"Attached User: Id={u.Id}, FirstName='{u.FirstName}', LastName='{u.LastName}'");
            //}

            await ctx.SaveChangesAsync();
        }

        public async Task AddChefAsync(UserDto user)
        {
            await ctx.Users.AddAsync(user.ToEntity());
        }        
        public async Task UpdateChefAsync(UserDto user)
        {
            User userToUpdate = await ctx.Users.FirstAsync(u => u.Id == user.Id);
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
        }

        public async Task<UserDto?> GetChefAsync(int id)
        {
            User? resu = await ctx.Users.FirstOrDefaultAsync(u => u.Id == id);
            return resu?.ToDto()
                        ?? null;
        }

        public async Task<List<UserDto>> GetAllChefsAsync()
        {
            return [.. await ctx.Users.Select(u => u.ToDto()).ToListAsync()];
        }

        public async Task AddCrepePartyAsync(CrepesPartyDto crepePartyDto)
        {
            CrepesParty crepeParty = crepePartyDto.ToEntity();
            crepeParty.User = ctx.Users.First(u => u.Id == crepeParty.UserId);
            await ctx.CrepesParty.AddAsync(crepeParty);
        }

        public async Task<List<CrepesPartyDto>> GetAllCrepePartiesAsync()
        {
            return [.. await ctx.CrepesParty.Select(cp => cp.ToDto()).ToListAsync()];
        }


        public async Task<int> GetLastSessionNumberAsync()
        {
            return await ctx.CrepesParty.AnyAsync()
                         ? ctx.CrepesParty.Max(u => u.SessionNumber)
                         : 0;
        }

        public async Task<List<CrepesPartyDto>> GetCrepePartiesFromSessionAsync(int SessionNumber)
        {
            return [.. await ctx.CrepesParty.Where(p => p.SessionNumber == SessionNumber)
                                      .OrderBy(c => c.Date)
                                      .Select(cp => cp.ToDto()).ToListAsync()];
                                      //.Include(u => u.User)];
        }



        //public void ExecuteInTransaction(Action<CrepePartyRepository> action)
        //{
        //    using var transaction = ctx.Database.BeginTransaction();
        //    action(this);
        //    ctx.SaveChanges();
        //    transaction.CommitAsync();
        //}

    }
}
