using BlogApi.Core.Interfaces.Repositories;
using BlogApi.Infrastructure.Repositories;

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

        return services;
    }
}