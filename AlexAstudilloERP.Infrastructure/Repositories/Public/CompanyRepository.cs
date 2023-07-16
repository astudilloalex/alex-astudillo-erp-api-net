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
        Company? finded = await _context.Companies
            .Include(c => c.Person)
            .FirstOrDefaultAsync(c => c.Id == entity.Id) ?? throw new ArgumentNullException(nameof(entity));
        finded.Tradename = entity.Tradename;
        finded.KeepAccount = entity.KeepAccount;
        finded.SpecialTaxpayer = entity.SpecialTaxpayer;
        finded.SpecialTaxpayerNumber = entity.SpecialTaxpayerNumber;
        finded.Active = entity.Active;
        finded.UserId = entity.UserId;
        if (entity.Person != null && finded.Person != null)
        {
            finded.Person.DocumentTypeId = entity.Person.DocumentTypeId;
            finded.Person.GenderId = entity.Person.GenderId;
            finded.Person.IdCard = entity.Person.IdCard;
            finded.Person.FirstName = entity.Person.FirstName;
            finded.Person.LastName = entity.Person.LastName;
            finded.Person.SocialReason = entity.Person.SocialReason;
            finded.Person.Birthdate = entity.Person.Birthdate;
            finded.Person.JuridicalPerson = entity.Person.JuridicalPerson;
        }
        await _context.SaveChangesAsync();
        return finded;
    }
}
