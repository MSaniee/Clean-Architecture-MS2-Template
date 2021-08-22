using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Features.Players.Commands;
using CleanArchitecture.Domain.Features.Players.Queries;
using CleanArchitecture.Infrastructure.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Services
{
    public class PlayersService : IPlayersService
    {
        private readonly IMediator _mediator;

        public PlayersService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<Player>> GetAllPlayers(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllPlayersQuery(), cancellationToken);

            return result;
        }

        public async Task<Player> Details(int id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetPlayerByIdQuery() { Id = id }, cancellationToken);
        }


        public async Task<Player> Create(CreatePlayerCommand command, CancellationToken cancellationToken)
        {            
            return await _mediator.Send(command, cancellationToken);
        }

        public async Task<int> Edit(int id, UpdatePlayerCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
            {
                throw new ArgumentNullException();
            }

            return await _mediator.Send(command, cancellationToken);                    
        }

        public async Task<int> Delete(int id, CancellationToken cancellationToken)
        {           
            return await _mediator.Send(new DeletePlayerCommand() { Id = id }, cancellationToken);
        }
    }
}
