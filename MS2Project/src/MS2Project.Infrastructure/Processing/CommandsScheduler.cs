using MS2Project.Application.Bases.Commands;
using MS2Project.Application.Bases.Processing;

namespace MS2Project.Infrastructure.Processing;

public class CommandsScheduler : ICommandsScheduler, IScopedDependency
{
    private readonly IDapperDbContext _dbContext;

    public CommandsScheduler(IDapperDbContext dbContext)
    {
        _dbContext = dbContext.ThrowIfNull();
    }

    public async Task EnqueueAsync<T>(ICommand<T> command)
    {
        //const string sqlInsert = "INSERT INTO [app].[InternalCommands] ([Id], [EnqueueDate] , [Type], [Data]) VALUES " +
        //                         "(@Id, @EnqueueDate, @Type, @Data)";

        //await _dbContext.Connection.ExecuteAsync(sqlInsert, new
        //{
        //    command.Id,
        //    EnqueueDate = DateTime.UtcNow,
        //    Type = command.GetType().FullName,
        //    Data = JsonConvert.SerializeObject(command)
        //});
    }
}