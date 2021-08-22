using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Features.Players.Queries
{
    public class GetPlayerByIdQuery : IRequest<Player>
    {
        public int Id { get; set; }

        public class GetPlayerByIdQueryHandler : IRequestHandler<GetPlayerByIdQuery, Player>
        {
            private readonly IPlayerRepository _playerRepo;

            public GetPlayerByIdQueryHandler(IPlayerRepository playerRepo)
            {
                _playerRepo = playerRepo;
            }

            public async Task<Player> Handle(GetPlayerByIdQuery query, CancellationToken cancellationToken)
            {
                return await _playerRepo.GetPlayerById(query.Id);
            }
        }
    }
}
