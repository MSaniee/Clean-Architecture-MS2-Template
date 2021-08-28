namespace MS2Project.Domain.Core.Settings.Mongo
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }

        public string PersonnelLogsDatabaseName { get; set; }
        public string PersonnelLogsCollectionName { get; set; }
    }
}
