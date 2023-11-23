using BlogApi.Core.Interfaces.Repositories;
using BlogApi.Core.Interfaces.UoW;
using BlogApi.Infrastructure.Repositories;
using BlogApi.Infrastructure.UoW;

namespace BlogApi.Shared.Extensions.Repositories;

public static class RepositoriesExtension
{
    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}