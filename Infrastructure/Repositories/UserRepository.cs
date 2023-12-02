using AutoMapper;
using BlogApi.Core.Interfaces.Repositories;
using BlogApi.Infrastructure.Data;

namespace BlogApi.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    
    private readonly ILogger<UserRepository> _logger;
    private readonly IMapper _mapper;
    private readonly AppDbContext _appDbContext;

    public UserRepository(ILogger<UserRepository> logger, IMapper mapper, AppDbContext appDbContext)
    {
        _logger = logger;
        _appDbContext = appDbContext;
        _mapper = mapper;
    }
    
    public void Dispose()
    {
        // TODO release managed resources here
    }


}