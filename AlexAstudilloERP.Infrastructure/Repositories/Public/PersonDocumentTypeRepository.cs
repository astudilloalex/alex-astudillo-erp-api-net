using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace AlexAstudilloERP.Infrastructure.Repositories.Public;

public class PersonDocumentTypeRepository(PostgreSQLContext context) : NPPostgreSQLRepository<PersonDocumentType, short>(context), IPersonDocumentTypeRepository
{
    private readonly PostgreSQLContext _context = context;

    public Task<List<PersonDocumentType>> FindByCountryCodeAsync(string countryCode)
    {
        return _context.PersonDocumentTypes.AsNoTracking()
            .Where(pdt => pdt.Country.Code.Equals(countryCode.ToUpper()))
            .ToListAsync();
    }

    public Task<bool> IsActive(short id)
    {
        return _context.PersonDocumentTypes.AsNoTracking().AnyAsync(p => p.Id == id && p.Active);
    }
}
