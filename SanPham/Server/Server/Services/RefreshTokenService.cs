using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Server.DBSettings;
using Server.IRepository;
using Server.Models;

namespace Server.Services
{
    public class RefreshTokenService : IRefreshToken
    {
        private readonly IMongoCollection<RefreshToken> _refreshTokenCollection;
        public RefreshTokenService(
       IOptions<BookStoreDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _refreshTokenCollection = mongoDatabase.GetCollection<RefreshToken>(
                bookStoreDatabaseSettings.Value.RefreshTokenCollection);
        }

        public async Task Delete(string token)
        {
            try
            {

                var refreshToken = await _refreshTokenCollection.Find(rt => rt.Token == token).FirstOrDefaultAsync();
                await _refreshTokenCollection.DeleteOneAsync(rt => rt == refreshToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

        public async Task Insert(RefreshToken refreshToken) =>
   await _refreshTokenCollection.InsertOneAsync(refreshToken);
    }
}
