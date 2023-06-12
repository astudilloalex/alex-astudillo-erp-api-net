using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace AlexAstudilloERP.Infrastructure.Repositories.Public;

public class EstablishmentRepository : NPPostgreSQLRepository<Establishment, int>, IEstablishmentRepository
{
    private readonly PostgreSQLContext _context;

    public EstablishmentRepository(PostgreSQLContext context) : base(context)
    {
        _context = context;
    }

    public new async ValueTask<Establishment> UpdateAsync(Establishment establishment)
    {
        Establishment? finded = await _context.Establishments.FirstOrDefaultAsync(e => e.Id == establishment.Id);
        if (finded == null) throw new ArgumentNullException(nameof(finded));
        finded.Name = establishment.Name;
        finded.Active = establishment.Active;
        finded.Description = establishment.Description;
        finded.Main = establishment.Main;
        await _context.SaveChangesAsync();
        return finded;
    }

    public Task<Establishment?> FindMainByCompanyId(int companyId)
    {
        return _context.Establishments.AsNoTracking()
            .FirstOrDefaultAsync(e => e.Main && e.CompanyId == companyId);
    }
}
