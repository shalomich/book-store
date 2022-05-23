using AutoMapper;
using BookStore.Application.Queries.Tags.GetTagGroups;
using BookStore.Domain.Entities.Products;

namespace BookStore.Application.Queries.Tags;
internal class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<TagGroup, TagGroupDto>();
        CreateMap<Tag, TagDto>();
    }
}

