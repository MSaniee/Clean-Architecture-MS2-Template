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
    public class DeletePlayerCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int? ShirtNo { get; set; }
        public string Name { get; set; }
        public int? Appearances { get; set; }
        public int? Goals { get; set; }

        public class DeletePlayerCommandHandler : IRequestHandler<DeletePlayerCommand, int>
        {
            private readonly IPlayerRepository _playerRepo;

            public DeletePlayerCommandHandler(IPlayerRepository playerRepo)
            {
                _playerRepo = playerRepo;
            }

            public async Task<int> Handle(DeletePlayerCommand command, CancellationToken cancellationToken)
            {
                var player = await _playerRepo.GetPlayerById(command.Id);
                if (player == null)
                    return default;

                return await _playerRepo.DeletePlayer(player);
            }
        }
    }
}
