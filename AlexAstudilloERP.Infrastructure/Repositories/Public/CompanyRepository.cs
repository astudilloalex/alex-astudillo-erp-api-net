using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Enums.Public;
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

    public Task<Company?> FindByCodeAsync(string code)
    {
        return _context.Companies.AsNoTracking()
            .Include(c => c.Person)
            .FirstOrDefaultAsync(c => c.Code!.Equals(code));
    }

    public Task<Company?> FindByIdCardAsync(string idCard)
    {
        return _context.Companies.AsNoTracking()
            .Include(c => c.Person)
            .FirstOrDefaultAsync(c => c.Person!.IdCard.Equals(idCard));
    }

    public async Task<IPage<Company>> FindByUserCodeAsync(IPageable pageable, string userCode)
    {
        short permission = (short)PermissionEnum.CompanyList;
        string query = "SELECT c.* FROM companies c WHERE EXISTS(" +
            "SELECT 1 FROM roles r INNER JOIN user_roles ur ON ur.role_id = r.id " +
            "INNER JOIN role_permissions rp ON rp.role_id = r.id " +
            "INNER JOIN users u ON u.id = ur.user_id " +
            "WHERE r.company_id = c.id AND rp.permission_id = {0} AND u.code = {1})";
        long count = await _context.Companies.FromSqlRaw(query, new object[] { permission, userCode }).AsNoTracking().LongCountAsync();
        List<Company> data = await _context.Companies.FromSqlRaw(query, new object[] { permission, userCode }).AsNoTracking()
            .Include(c => c.Person)
            .OrderBy(c => c.Tradename)
            .Skip(Convert.ToInt32(pageable.GetOffset()))
            .Take(pageable.GetPageSize())
            .ToListAsync();
        return new Page<Company>(data, pageable, count);
    }

    public Task<string?> FindCodeById(int id)
    {
        return _context.Companies.AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => c.Code)
            .FirstOrDefaultAsync();
    }

    public new async ValueTask<Company> UpdateAsync(Company entity)
    {
        Company finded = await _context.Companies.FirstAsync(c => c.Code.Equals(entity.Code));
        finded.Tradename = entity.Tradename;
        finded.Description = entity.Description;
        finded.PersonId = entity.PersonId;
        await _context.SaveChangesAsync();
        return finded;
    }
}
