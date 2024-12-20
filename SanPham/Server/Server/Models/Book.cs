using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Server.Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }

        public string author { get; set; }
        public double price { get; set; }
        public int yearOfPublishing { get; set; }
        public string langue { get; set; }
        public string weight { get; set; }
        public string name { get; set; }
        public string size { get; set; }
        public int page { get; set; }
        public string appearance { get; set; }
        public int publisherID { get; set; }

        public string[] thumbnails { get; set; }

        public int categoryId { get; set; }


    }
}
