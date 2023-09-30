using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace AlexAstudilloERP.Infrastructure.Repositories.Public;

public class UserMembershipRepository : IUserMembershipRepository
{
    private readonly PostgreSQLContext _context;

    public UserMembershipRepository(PostgreSQLContext context)
    {
        _context = context;
    }

    public Task<List<UserMembership>> FindByUserCode(string userCode)
    {
        return _context.UserMemberships.AsNoTracking()
            .Where(um => um.User.Code.Equals(userCode) && um.EndDate > DateTime.UtcNow)
            .ToListAsync();
    }
}
