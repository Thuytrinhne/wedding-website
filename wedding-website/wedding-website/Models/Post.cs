using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace wedding_website.Models
{
    public class Post
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public string? Name { get; set; }

        [BsonElement("contact"), BsonRepresentation(BsonType.String)]
        public string? Contact { get; set; }

        [BsonElement("content"), BsonRepresentation(BsonType.String)]
        public string ? Content { get; set; }

        [BsonElement("CreatedAt"), BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // turn to VN hour

       
    }
}
