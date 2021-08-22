using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> GetPlayersList();
        Task<Player> GetPlayerById(int id);
        Task<Player> CreatePlayer(Player player);
        Task<int> UpdatePlayer(Player player);
        Task<int> DeletePlayer(Player player);
    }
}
