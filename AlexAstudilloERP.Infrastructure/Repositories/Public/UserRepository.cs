using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace AlexAstudilloERP.Infrastructure.Repositories.Public;

public class UserRepository : NPPostgreSQLRepository<User, long>, IUserRepository
{
    private readonly PostgreSQLContext _context;

    public UserRepository(PostgreSQLContext context) : base(context)
    {
        _context = context;
    }

    public Task<bool> ExistsUsername(string username)
    {
        return _context.Users.AsNoTracking().AnyAsync(x => x.Username.Equals(username));
    }

    public Task<User?> FindByUsernameOrEmail(string value)
    {
        return _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Username.Equals(value) || x.Email!.Mail.Equals(value));
    }
}
