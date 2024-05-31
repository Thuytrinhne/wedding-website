using AutoMapper;
using wedding_website.Models;
using wedding_website.Models.Dto;

namespace wedding_website.Helpers
{
    public class MyMappingProfile : Profile
    {
        public MyMappingProfile()
        {

            
            CreateMap<Post, CreatePostDto >().ReverseMap();
            CreateMap<Post, GetPostDto>()
                    .ForMember(dest => dest.TimeSincePost, opt => opt.MapFrom(src => PostHelper.GetTimeSincePosted(src.CreatedAt)));

        }
    }
}
