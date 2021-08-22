using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Data.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly BookStoreDBContext _context;

        public PlayerRepository(BookStoreDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Player>> GetPlayersList()
        {
            return await _context.Players
                .ToListAsync();
        }

        public async Task<Player> GetPlayerById(int id)
        {
            return await _context.Players
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Player> CreatePlayer(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            return player;
        }

        public async Task<int> UpdatePlayer(Player player)
        {
            _context.Players.Update(player);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeletePlayer(Player player)
        {
            _context.Players.Remove(player);
            return await _context.SaveChangesAsync();
        }
    }
}
