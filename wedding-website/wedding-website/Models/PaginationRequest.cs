using Microsoft.AspNetCore.Mvc;

namespace wedding_website.Models
{
    public class PaginationRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = -1;
    }
}
