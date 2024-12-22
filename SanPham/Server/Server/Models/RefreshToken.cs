using MongoDB.Bson.Serialization.Attributes;

namespace Server.Models
{
    public class RefreshToken
    {
        [BsonId]
        public Guid Id { get; set; } // If you're using Guid as the identifier

        public Guid UserID { get; set; }

        public string Token { get; set; }

        public DateTime DateTime { get; set; }
    }
}
