using MS2Project.Application.Bases.Commands;
using MS2Project.Application.Bases.DomainEvents;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;

namespace MS2Project.Infrastructure.Processing.Outbox;

public record ProcessOutboxCommand : CommandBase<Unit>, IRecurringCommand;

internal class ProcessOutboxCommandHandler : ICommandHandler<ProcessOutboxCommand, Unit>
{
    private readonly IMediator _mediator;

    private readonly IDapperDbContext _dapperDbContext;

    public ProcessOutboxCommandHandler(IMediator mediator, IDapperDbContext dapperDbContext)
    {
        _mediator = mediator;
        _dapperDbContext = dapperDbContext;
    }

    public async Task<Unit> Handle(ProcessOutboxCommand command, CancellationToken cancellationToken)
    {
        var connection = _dapperDbContext.Connection;
        const string sql = "SELECT " +
                           "[OutboxMessage].[Id], " +
                           "[OutboxMessage].[Type], " +
                           "[OutboxMessage].[Data] " +
                           "FROM [app].[OutboxMessages] AS [OutboxMessage] " +
                           "WHERE [OutboxMessage].[ProcessedDate] IS NULL";

        var messages = await connection.QueryAsync<OutboxMessageDto>(sql);
        var messagesList = messages.AsList();

        const string sqlUpdateProcessedDate = "UPDATE [app].[OutboxMessages] " +
                                              "SET [ProcessedDate] = @Date " +
                                              "WHERE [Id] = @Id";
        if (messagesList.Count > 0)
        {
            foreach (var message in messagesList)
            {
                Type type = Assemblies.Application
                    .GetType(message.Type);
                var request = JsonConvert.DeserializeObject(message.Data, type) as IDomainEventNotification;

                using (LogContext.Push(new OutboxMessageContextEnricher(request)))
                {
                    await this._mediator.Publish(request, cancellationToken);

                    await connection.ExecuteAsync(sqlUpdateProcessedDate, new
                    {
                        Date = DateTime.UtcNow,
                        message.Id
                    });
                }

            }
        }

        return Unit.Value;
    }

    private class OutboxMessageContextEnricher : ILogEventEnricher
    {
        private readonly IDomainEventNotification _notification;

        public OutboxMessageContextEnricher(IDomainEventNotification notification)
        {
            _notification = notification;
        }
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddOrUpdateProperty(new LogEventProperty("Context", new ScalarValue($"OutboxMessage:{_notification.Id.ToString()}")));
        }
    }
}