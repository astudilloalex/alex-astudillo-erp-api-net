using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace AlexAstudilloERP.Infrastructure.Repositories.Public;

public class PersonRepository : NPPostgreSQLRepository<Person, long>, IPersonRepository
{
    private readonly PostgreSQLContext _context;

    public PersonRepository(PostgreSQLContext context) : base(context)
    {
        _context = context;
    }

    public Task<bool> ExistsIdCard(string idCard)
    {
        return _context.People.AsNoTracking().AnyAsync(p => p.IdCard.Equals(idCard));
    }

    public Task<Person?> FindByIdCard(string idCard)
    {
        return _context.People.AsNoTracking().FirstOrDefaultAsync(p => p.IdCard.Equals(idCard));
    }

    public Task<string?> FindCodeByIdCard(string idCard)
    {
        return _context.People.AsNoTracking()
            .Where(p => p.IdCard.Equals(idCard))
            .Select(p => p.Code)
            .FirstOrDefaultAsync();
    }
}
