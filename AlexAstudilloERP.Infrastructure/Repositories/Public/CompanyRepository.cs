using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Infrastructure.Connections;
using EFCommonCRUD.Interfaces;
using EFCommonCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace AlexAstudilloERP.Infrastructure.Repositories.Public;

public class CompanyRepository : NPPostgreSQLRepository<Company, int>, ICompanyRepository
{
    private readonly PostgreSQLContext _context;

    public CompanyRepository(PostgreSQLContext context) : base(context)
    {
        _context = context;
    }

    public Task<bool> ExistsByCompanyIdAndUserId(int companyId, long userId)
    {
        string query = @"SELECT * FROM companies c WHERE c.id = ANY (
	        SELECT est.company_id FROM establishments est
	        INNER JOIN user_establishments ue ON ue.establishment_id = est.id
	        WHERE est.company_id = {0} AND ue.user_id = {1}
        )";
        return _context.Companies.FromSqlRaw(query, new object[] { companyId, userId }).AsNoTracking().AnyAsync();
    }

    public Task<bool> ExistsByPersonIdCard(string idCard)
    {
        return _context.Companies.AsNoTracking().AnyAsync(c => c.Person!.IdCard.Equals(idCard));
    }

    public Task<Company?> FindByCode(string code)
    {
        return _context.Companies.AsNoTracking()
            .Include(c => c.Person)
            .FirstOrDefaultAsync(c => c.Code!.Equals(code));
    }

    public Task<Company?> FindByIdCard(string idCard)
    {
        return _context.Companies.AsNoTracking()
            .Include(c => c.Person)
            .FirstOrDefaultAsync(c => c.Person!.IdCard.Equals(idCard));
    }

    public async Task<IPage<Company>> FindByUserId(IPageable pageable, long userId)
    {
        string query = @"SELECT * FROM companies c WHERE c.id = ANY (
	        SELECT est.company_id FROM establishments est
	        INNER JOIN user_establishments ue ON ue.establishment_id = est.id
	        WHERE ue.user_id = {0}
        )";
        long count = await _context.Companies.FromSqlRaw(query, userId).AsNoTracking().LongCountAsync();
        List<Company> data = await _context.Companies.FromSqlRaw(query, userId).AsNoTracking()
            .Include(c => c.Person)
            .OrderBy(c => c.Tradename)
            .Skip(Convert.ToInt32(pageable.GetOffset()))
            .Take(pageable.GetPageSize())
            .ToListAsync();
        return new Page<Company>(data, pageable, count);
    }

    public new async ValueTask<Company> UpdateAsync(Company entity)
    {
        Company finded = await _context.Companies.FirstAsync(c => c.Code.Equals(entity.Code));
        finded.Tradename = entity.Tradename;
        finded.Description = entity.Description;
        await _context.SaveChangesAsync();
        return finded;
    }
}
