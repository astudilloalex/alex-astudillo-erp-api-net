using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace AlexAstudilloERP.Infrastructure.Repositories.Public;

public class PersonDocumentTypeRepository : NPPostgreSQLRepository<PersonDocumentType, short>, IPersonDocumentTypeRepository
{
    private readonly PostgreSQLContext _context;

    public PersonDocumentTypeRepository(PostgreSQLContext context) : base(context)
    {
        _context = context;
    }

    public Task<bool> IsActive(short id)
    {
        return _context.PersonDocumentTypes.AsNoTracking().AnyAsync(p => p.Id == id && p.Active);
    }
}
