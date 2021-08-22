using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Features.Players.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Interfaces
{
    public interface IPlayersService
    {
        Task<IEnumerable<Player>> GetAllPlayers(CancellationToken cancellationToken);
        Task<Player> Details(int id, CancellationToken cancellationToken);
        Task<Player> Create(CreatePlayerCommand command, CancellationToken cancellationToken);
        Task<int> Edit(int id, UpdatePlayerCommand command, CancellationToken cancellationToken);
        Task<int> Delete(int id, CancellationToken cancellationToken);
    }
}
