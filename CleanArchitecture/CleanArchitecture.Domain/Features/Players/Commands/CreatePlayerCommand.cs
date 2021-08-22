using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Features.Players.Commands
{
    public class CreatePlayerCommand : IRequest<Player>
    {
        public int ShirtNo { get; set; }
        public string Name { get; set; }
        public int Goals { get; set; }

        public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, Player>
        {
            private readonly IPlayerRepository _playerRepo;

            public CreatePlayerCommandHandler(IPlayerRepository playerRepo)
            {
                _playerRepo = playerRepo;
            }

            public async Task<Player> Handle(CreatePlayerCommand command, CancellationToken cancellationToken)
            {
                var player = new Player()
                {
                    ShirtNo = command.ShirtNo,
                    Name = command.Name,
                    Goals = command.Goals
                };

                return await _playerRepo.CreatePlayer(player);
            }
        }
    }
}
