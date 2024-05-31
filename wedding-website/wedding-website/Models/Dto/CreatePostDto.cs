using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace wedding_website.Models.Dto
{
    public class CreatePostDto
    {
        public string? Name { get; set; }
        public string? Contact { get; set; }
        public string? Content { get; set; }
    }
}
