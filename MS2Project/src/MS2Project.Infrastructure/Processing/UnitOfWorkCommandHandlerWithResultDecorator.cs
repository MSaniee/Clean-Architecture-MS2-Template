using MS2Project.Application.Bases.Commands;
using MS2Project.Infrastructure.Processing.InternalCommands;

namespace MS2Project.Infrastructure.Processing;

public class UnitOfWorkCommandHandlerWithResultDecorator<T, TResult> : ICommandHandler<T, TResult> where T : ICommand<TResult>
{
    private readonly ICommandHandler<T, TResult> _decorated;

    private readonly IUnitOfWork _unitOfWork;

    private readonly ApplicationDbContext _dbContext;

    public UnitOfWorkCommandHandlerWithResultDecorator(
        ICommandHandler<T, TResult> decorated,
        IUnitOfWork unitOfWork,
        ApplicationDbContext dbContext)
    {
        _decorated = decorated;
        _unitOfWork = unitOfWork;
        _dbContext = dbContext;
    }

    public async Task<TResult> Handle(T command, CancellationToken cancellationToken)
    {
        var result = await _decorated.Handle(command, cancellationToken);

        if (command is InternalCommandBase<TResult>)
        {
            var internalCommand = await _dbContext.Set<InternalCommand>().FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken: cancellationToken);

            if (internalCommand != null)
            {
                internalCommand.ProcessedDate = DateTime.UtcNow;
            }
        }

        await _unitOfWork.CommitAsync(cancellationToken);

        return result;
    }
}

