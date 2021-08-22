using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Features.Players.Queries
{
    public class GetAllPlayersQuery : IRequest<IEnumerable<Player>>
    {
        public class GetAllPlayersQueryHandler : IRequestHandler<GetAllPlayersQuery, IEnumerable<Player>>
        {
            private readonly IPlayerRepository _playerRepo;

            public GetAllPlayersQueryHandler(IPlayerRepository playerRepo)
            {
                _playerRepo = playerRepo;
            }

            public async Task<IEnumerable<Player>> Handle(GetAllPlayersQuery query, CancellationToken cancellationToken)
            {
                return await _playerRepo.GetPlayersList();
            }
        }
    }
}
