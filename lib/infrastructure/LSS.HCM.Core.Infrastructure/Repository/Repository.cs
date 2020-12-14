using LSS.HCM.Core.Infrastructure.DbContext;
using MongoDB.Driver;

namespace LSS.HCM.Core.Infrastructure.Repository
{
    public static class Repository<T> where T : class
    {
        public static IMongoCollection<T> Get(string connectionString, string databaseName, string collectionName)
        {
            var database = HardwareApiQueriesContext.MongoDbInitialization(connectionString, databaseName);
            var result = database.GetCollection<T>(collectionName);
            return result;
        }
    }
}
