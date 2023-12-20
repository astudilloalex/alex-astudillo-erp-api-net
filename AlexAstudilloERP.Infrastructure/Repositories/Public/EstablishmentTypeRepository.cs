using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace AlexAstudilloERP.Infrastructure.Repositories.Public;

public class EstablishmentTypeRepository(PostgreSQLContext context) : NPPostgreSQLRepository<EstablishmentType, short>(context), IEstablishmentTypeRepository
{
    private readonly PostgreSQLContext _context = context;

    public Task<List<EstablishmentType>> FindAllActives()
    {
        return _context.EstablishmentTypes.AsNoTracking().Where(et => et.Active).ToListAsync();
    }
}
