using MongoDB.Bson.Serialization.Attributes;

namespace Server.Models
{
    public class Category
    {
        [BsonId]
        public int _id { get; set; }
        public string name {  get; set; }
    }
}
