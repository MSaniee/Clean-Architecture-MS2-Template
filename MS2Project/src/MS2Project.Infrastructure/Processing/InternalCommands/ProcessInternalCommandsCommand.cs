using MS2Project.Application.Bases.Commands;
using MS2Project.Infrastructure.Processing.Outbox;

namespace MS2Project.Infrastructure.Processing.InternalCommands;

internal record ProcessInternalCommandsCommand : CommandBase<Unit>, IRecurringCommand;

internal class ProcessInternalCommandsCommandHandler : ICommandHandler<ProcessInternalCommandsCommand, Unit>
{
    private readonly IDapperDbContext _dapperDbContext;

    public ProcessInternalCommandsCommandHandler(
        IDapperDbContext dapperDbContext)
    {
        _dapperDbContext = dapperDbContext;
    }

    public async Task<Unit> Handle(ProcessInternalCommandsCommand command, CancellationToken cancellationToken)
    {
        var connection = this._dapperDbContext.Connection;

        const string sql = "SELECT " +
                           "[Command].[Type], " +
                           "[Command].[Data] " +
                           "FROM [app].[InternalCommands] AS [Command] " +
                           "WHERE [Command].[ProcessedDate] IS NULL";
        var commands = await connection.QueryAsync<InternalCommandDto>(sql);

        var internalCommandsList = commands.AsList();

        foreach (var internalCommand in internalCommandsList)
        {
            Type type = Assemblies.Application.GetType(internalCommand.Type);
            dynamic commandToProcess = JsonConvert.DeserializeObject(internalCommand.Data, type);

            await CommandsExecutor.Execute(commandToProcess);
        }

        return Unit.Value;
    }

    private class InternalCommandDto
    {
        public string Type { get; set; }

        public string Data { get; set; }
    }
}