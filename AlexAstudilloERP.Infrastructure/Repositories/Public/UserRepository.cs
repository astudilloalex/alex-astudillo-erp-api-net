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

    public Task<bool> ExistsByEmail(string mail)
    {
        return _context.Users.AsNoTracking().AnyAsync(u => u.Email!.Mail.Equals(mail));
    }

    public Task<bool> ExistsByIdCard(string idCard)
    {
        return _context.Users.AsNoTracking().AnyAsync(u => u.Person!.IdCard.Equals(idCard));
    }

    public Task<bool> ExistsUsername(string username)
    {
        return _context.Users.AsNoTracking().AnyAsync(u => u.Username.Equals(username));
    }

    public new async ValueTask<User?> FindByIdAsync(long id)
    {
        return await _context.Users.AsNoTracking()
            .Include(u => u.Person)
            .Include(u => u.Email)
            .FirstOrDefaultAsync(u => u.PersonId == id);
    }

    public Task<User?> FindByIdCard(string idCard)
    {
        return _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Person!.IdCard.Equals(idCard));
    }

    public Task<User?> FindByUsernameOrEmail(string value)
    {
        return _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username.Equals(value) || u.Email!.Mail.Equals(value));
    }
}
