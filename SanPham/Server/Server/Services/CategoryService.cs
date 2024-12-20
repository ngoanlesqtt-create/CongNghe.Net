using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Server.DBSettings;
using Server.Models;

namespace Server.Services
{
    public class CategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        public CategoryService(
     IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _categoryCollection = mongoDatabase.GetCollection<Category>(
                bookStoreDatabaseSettings.Value.CategoryCollection);
        }

        public async Task<List<Category>> GetAsync() =>
   await _categoryCollection.Find(_ => true).ToListAsync();

    }
}
