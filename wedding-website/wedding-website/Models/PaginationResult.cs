using Microsoft.AspNetCore.Mvc;

namespace wedding_website.Models
{

    public class PaginationResult<TEntity>
  (int pageNumber, int pageSize, int totalPage, long totalItems, IEnumerable<TEntity> data)
  where TEntity : class
    {


        public int PageNumber { get; set; } = pageNumber;
        public int PageSize { get; set; } = pageSize;
        public int TotalPage { get; set; } = totalPage;
        public long TotalItems { get; set; } = totalItems;
        public IEnumerable<TEntity> Data { get; set; } = data;

    }

    
}
