using MediatR;
using MS2Project.Application.Features.Customers.Commands;

namespace MS2Project.Infrastructure.Processing.InternalCommands;

public class CommandsDispatcher : ICommandsDispatcher
{
    private readonly IMediator _mediator;
    private readonly ApplicationDbContext _dbContext;

    public CommandsDispatcher(
        IMediator mediator,
        ApplicationDbContext dbContext)
    {
        _mediator = mediator;
        _dbContext = dbContext;
    }

    public async Task DispatchCommandAsync(Guid id)
    {
        InternalCommand internalCommand = await _dbContext.Set<InternalCommand>().SingleOrDefaultAsync(x => x.Id == id);

        Type type = Assembly.GetAssembly(typeof(MarkCustomerAsWelcomedCommand)).GetType(internalCommand.Type);
        dynamic command = JsonConvert.DeserializeObject(internalCommand.Data, type);

        internalCommand.ProcessedDate = DateTime.Now;

        await _mediator.Send(command);
    }
}

