using AutoMapper;
using BlogApi.Application.DTOs.Comment;
using BlogApi.Application.DTOs.Category;
using BlogApi.Application.DTOs.Post;
using BlogApi.Application.DTOs.Tag;
using BlogApi.Core.Entities;

namespace BlogApi.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Tag, TagDto>();
        CreateMap<Post, PostDto>();
        CreateMap<Comment, CommentDto>();
        CreateMap<Category, CategoryDto>();
    }
}