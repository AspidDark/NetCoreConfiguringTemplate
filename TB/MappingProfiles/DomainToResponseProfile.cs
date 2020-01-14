using System.Linq;
using AutoMapper;
using TB.Contracts.V1.Responses;
using TB.Domain;

namespace TB.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Post, PostResponse>()
                .ForMember(dest => dest.Tags, opt =>
                    opt.MapFrom(src => src.Tags.Select(x => new TagResponse { Name = x.TagName })));

            CreateMap<Tag, TagResponse>();
        }
    }
}
