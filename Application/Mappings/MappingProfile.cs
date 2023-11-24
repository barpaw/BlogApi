using AutoMapper;
using BlogApi.Application.DTOs;
using BlogApi.Core.Entities;

namespace BlogApi.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Tag, TagDto>();
    }
}