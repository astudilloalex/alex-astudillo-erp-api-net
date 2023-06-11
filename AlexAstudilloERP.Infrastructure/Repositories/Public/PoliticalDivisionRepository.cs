using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace AlexAstudilloERP.Infrastructure.Repositories.Public;

public class PoliticalDivisionRepository : NPPostgreSQLRepository<PoliticalDivision, int>, IPoliticalDivisionRepository
{
    private readonly PostgreSQLContext _context;

    public PoliticalDivisionRepository(PostgreSQLContext context) : base(context)
    {
        _context = context;
    }

    public Task<List<PoliticalDivision>> FindByParentId(int parentId)
    {
        return _context.PoliticalDivisions.AsNoTracking().Where(p => p.ParentId == parentId).ToListAsync();
    }

    public Task<List<PoliticalDivision>> FindByTypeId(short typeId)
    {
        return _context.PoliticalDivisions.AsNoTracking().Where(p => p.PoliticalDivisionTypeId == typeId).ToListAsync();
    }
}
