using wedding_website.Helpers;

namespace wedding_website.Models.Dto
{
    public class GetPostDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Contact { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }

        private string? _timeSincePost;
        public string? TimeSincePost
        {
            get
            {
                return _timeSincePost ?? PostHelper.GetTimeSincePosted(CreatedAt);
            }
            set
            {
                _timeSincePost = value;
            }
        }
    }
}
