using MongoDB.Driver;

namespace LSS.HCM.Core.Infrastructure.DbContext
{
    public static class HardwareApiQueriesContext 
    {
        public static IMongoDatabase MongoDbInitialization(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            return database;
        }
    }
}
