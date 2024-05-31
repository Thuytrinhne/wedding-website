using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using wedding_website.Models;

namespace wedding_website.Data
{
    public class MongoDbService
    {
        private readonly IMongoDatabase ? _database;
        public MongoDbService(IOptions<WeddingDatabaseSettings> weddingDatabaseSettings)
        {
            var mongoUrl = MongoUrl.Create(weddingDatabaseSettings.Value.ConnectionString);
            var mongoClient = new MongoClient(mongoUrl);
            _database = mongoClient.GetDatabase(weddingDatabaseSettings.Value.DatabaseName);
        }
        public IMongoDatabase ?Database => _database;   
    }
}
