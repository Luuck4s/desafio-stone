using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApiStone.Entities;

namespace WebApiStone.Settings
{
    public class DatabaseConnection : IDatabaseConnection
    {
        MongoClient MongoClient { get; set; }

        private string DATABASE_COLLECTION;
        private string DATABASE_NAME;

        public DatabaseConnection(IOptions<DatabaseSettings> databaseSettings)
        {
            MongoClient = new MongoClient(
            databaseSettings.Value.ConnectionString);

            DATABASE_NAME = databaseSettings.Value.DatabaseName;
            DATABASE_COLLECTION = databaseSettings.Value.CollectionName;
        }

        public IMongoCollection<Person> GetPersonCollection()
        {
            var database = MongoClient.GetDatabase(DATABASE_NAME);
            return database.GetCollection<Person>(DATABASE_COLLECTION);
        }
    }
}
