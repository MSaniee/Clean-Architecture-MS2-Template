namespace MS2Project.Domain.Core.Settings.Mongo;

public interface IMongoDbSettings
{
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
    string CollectionName { get; set; }

    string PersonnelLogsDatabaseName { get; set; }
    string PersonnelLogsCollectionName { get; set; }
}

