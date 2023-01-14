using MediatR;
using MS2Project.Application.Bases.Commands;
using MS2Project.Infrastructure.Processing.InternalCommands;

namespace MS2Project.Infrastructure.Processing;

public class UnitOfWorkCommandHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
{
    private readonly ICommandHandler<T> _decorated;

    private readonly IUnitOfWork _unitOfWork;

    private readonly ApplicationDbContext _dbContext;

    public UnitOfWorkCommandHandlerDecorator(
        ICommandHandler<T> decorated,
        IUnitOfWork unitOfWork,
        ApplicationDbContext dbContext)
    {
        _decorated = decorated;
        _unitOfWork = unitOfWork;
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(T command, CancellationToken cancellationToken)
    {
        await _decorated.Handle(command, cancellationToken);

        if (command is InternalCommandBase)
        {
            var internalCommand =
                await _dbContext.Set<InternalCommand>().FirstOrDefaultAsync(x => x.Id == command.Id,
                    cancellationToken: cancellationToken);

            if (internalCommand != null)
            {
                internalCommand.ProcessedDate = DateTime.Now;
            }
        }

        await _unitOfWork.CommitAsync(cancellationToken);

        return Unit.Value;
    }
}

