using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using wedding_website.Data;
using wedding_website.Models;
using wedding_website.Models.Dto;

namespace wedding_website.Controllers
{
    [ApiController]
    [Route("api/posts")]

    public class PostController : ControllerBase
    {
        private readonly IMongoCollection<Post> _posts;
        private readonly IMapper _mapper;
        public PostController(MongoDbService mongoDb,
            IOptions<WeddingDatabaseSettings> weddingDatabaseSettings,
             IMapper mapper)
        {
            _mapper = mapper;
            _posts = mongoDb.Database?.GetCollection<Post>(weddingDatabaseSettings.Value.PostsCollectionName)!;  
        }

        [HttpGet]

        public async Task<PaginationResult<GetPostDto>> Get(
            [FromQuery]  PaginationRequest paginationRequest,
            [FromQuery] string ? Name,
            [FromQuery] string ? Contact,
            [FromQuery] string? Keyword)
        {
            var filterBuilder = Builders<Post>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrEmpty(Name))
            {
                filter = filter & filterBuilder.Eq(post => post.Name, Name);
            }

            if (!string.IsNullOrEmpty(Contact))
            {
                filter = filter & filterBuilder.Eq(post => post.Contact, Contact);
            }

            if (!string.IsNullOrEmpty(Keyword))
            {
                var keywordFilter = filterBuilder.Or(
                    filterBuilder.Regex(post => post.Name, new MongoDB.Bson.BsonRegularExpression(Keyword, "i")),
                    filterBuilder.Regex(post => post.Contact, new MongoDB.Bson.BsonRegularExpression(Keyword, "i")),
                    filterBuilder.Regex(post => post.Content, new MongoDB.Bson.BsonRegularExpression(Keyword, "i"))
                );
                filter = filter & keywordFilter;
            }
            
            var totalItems = await _posts.CountDocumentsAsync(filter);
            if (paginationRequest.PageSize == -1)
                paginationRequest.PageSize =(int) totalItems;
            var totalPages = (int)Math.Ceiling((double)totalItems / paginationRequest.PageSize);

            var posts = await _posts.Find(filter)
                                     .Sort(Builders<Post>.Sort.Descending(post => post.CreatedAt))
                                    .Skip(paginationRequest.PageSize * (paginationRequest.PageNumber - 1)).Limit(paginationRequest.PageSize)
                                    .ToListAsync();

            var postsDto = _mapper.Map<List<GetPostDto>>(posts);    
            return new  PaginationResult<GetPostDto>(paginationRequest.PageNumber, paginationRequest.PageSize, totalPages,  totalItems, postsDto);
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreatePostDto newPost)
        {
            await _posts.InsertOneAsync(_mapper.Map<Post>(newPost));
            return Ok();
        }

    }
}
