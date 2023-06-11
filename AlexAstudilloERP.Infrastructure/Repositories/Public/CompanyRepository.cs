using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace AlexAstudilloERP.Infrastructure.Repositories.Public;

public class CompanyRepository : NPPostgreSQLRepository<Company, int>, ICompanyRepository
{
    private readonly PostgreSQLContext _context;

    public CompanyRepository(PostgreSQLContext context) : base(context)
    {
        _context = context;
    }

    public Task<bool> ExistsCompanyByPersonIdCard(string idCard)
    {
        return _context.Companies.AsNoTracking().AnyAsync(c => c.Person!.IdCard.Equals(idCard));
    }
}
