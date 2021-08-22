using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.CommandHandlers;
using CleanArchitecture.Domain.Commands;
using CleanArchitecture.Domain.Core.Bus;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Features.Players.Queries;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Infrastructure.Bus;
using CleanArchitecture.Infrastructure.Data.Context;
using CleanArchitecture.Infrastructure.Data.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CleanArchitecture.Domain.Features.Players.Queries.GetAllPlayersQuery;

namespace CleanArchitecture.Infrastructure.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Domain InMemoryBus MediatR
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            //Domain Handlers
            services.AddScoped<IRequestHandler<CreateCategoryCommand, bool>, CategoryCommandHandler>();
            services.AddScoped<IRequestHandler<GetAllPlayersQuery, IEnumerable<Player>>, GetAllPlayersQueryHandler>();

            //Application Layer
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IPlayersService, PlayersService>();


            //Infra.Data
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();

            //??
            services.AddScoped<BookStoreDBContext>();
        }
    }
}
