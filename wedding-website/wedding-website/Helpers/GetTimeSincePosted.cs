using Microsoft.AspNetCore.Http.HttpResults;

namespace wedding_website.Helpers
{
    public class PostHelper
    {
        public static string GetTimeSincePosted(DateTime CreatedAt)
        {
            TimeSpan timeSpan = DateTime.UtcNow - CreatedAt;

            if (timeSpan.TotalMinutes < 60)
            {
                return $"{(int)timeSpan.TotalMinutes} phút trước";
            }
            else if (timeSpan.TotalHours < 24)
            {
                return $"{(int)timeSpan.TotalHours} giờ trước";
            }
            else if (timeSpan.TotalDays < 7)
            {
                return $"{(int)timeSpan.TotalDays} ngày trước";
            }
            else if (timeSpan.TotalDays < 30)
            {
                return $"{(int)(timeSpan.TotalDays / 7)} tuần trước";
            }
            else if (timeSpan.TotalDays < 365)
            {
                return $"{(int)(timeSpan.TotalDays / 30)} tháng trước";
            }
            else
            {
                return $"{(int)(timeSpan.TotalDays / 365)} năm trước";
            }
        }
    }
}
