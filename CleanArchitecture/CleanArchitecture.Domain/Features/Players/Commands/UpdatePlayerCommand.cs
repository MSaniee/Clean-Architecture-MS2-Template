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
    public class UpdatePlayerCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int ShirtNo { get; set; }
        public string Name { get; set; }
        public int Goals { get; set; }

        public class UpdatePlayerCommandHandler : IRequestHandler<UpdatePlayerCommand, int>
        {
            private readonly IPlayerRepository _playerRepo;

            public UpdatePlayerCommandHandler(IPlayerRepository playerRepo)
            {
                _playerRepo = playerRepo;
            }

            public async Task<int> Handle(UpdatePlayerCommand command, CancellationToken cancellationToken)
            {
                var player = await _playerRepo.GetPlayerById(command.Id);
                if (player == null)
                    return default;

                player.ShirtNo = command.ShirtNo;
                player.Name = command.Name;
                player.Goals = command.Goals;

                return await _playerRepo.UpdatePlayer(player);
            }
        }
    }
}
