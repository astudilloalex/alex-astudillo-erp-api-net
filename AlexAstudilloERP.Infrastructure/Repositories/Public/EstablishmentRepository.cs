using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Infrastructure.Connections;
using EFCommonCRUD.Interfaces;
using EFCommonCRUD.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AlexAstudilloERP.Infrastructure.Repositories.Public;

public class EstablishmentRepository : NPPostgreSQLRepository<Establishment, int>, IEstablishmentRepository
{
    private readonly PostgreSQLContext _context;

    public EstablishmentRepository(PostgreSQLContext context) : base(context)
    {
        _context = context;
    }

    public Task<Establishment?> FindByCode(string code)
    {
        return _context.Establishments.AsNoTracking()
            .Include(e => e.Address!.PoliticalDivision!.Country)
            .FirstOrDefaultAsync(e => e.Code!.Equals(code));
    }

    public async Task<IPage<Establishment>> FindByCompanyId(IPageable pageable, int companyId)
    {
        int offset = Convert.ToInt32(pageable.GetOffset());
        List<Establishment> data = await _context.Establishments.AsNoTracking()
            .Where(e => e.CompanyId == companyId)
            .OrderBy(e => e.Name)
            .Skip(offset)
            .Take(pageable.GetPageSize())
            .ToListAsync();
        return new Page<Establishment>(data, pageable, await _context.Establishments.AsNoTracking().LongCountAsync(e => e.CompanyId == companyId));
    }

    public Task<Establishment?> FindMainByCompanyId(int companyId)
    {
        return _context.Establishments.AsNoTracking()
            .FirstOrDefaultAsync(e => e.Main && e.CompanyId == companyId);
    }

    public async Task<Establishment> SetMain(int id, long userId)
    {
        Establishment? finded;
        using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                int companyId = await _context.Establishments.AsNoTracking().Where(e => e.Id == id).Select(e => e.CompanyId).FirstOrDefaultAsync();
                await _context.Establishments.Where(e => e.CompanyId == companyId).ExecuteUpdateAsync(e => e.SetProperty(p => p.Main, p => false));
                finded = await _context.Establishments.FirstOrDefaultAsync(e => e.Id == id) ?? throw new ArgumentNullException(nameof(id));
                finded.Main = true;
                finded.UserId = userId;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        return finded;
    }

    public new async ValueTask<Establishment> UpdateAsync(Establishment establishment)
    {
        Establishment? finded = await _context.Establishments.FirstOrDefaultAsync(e => e.Id == establishment.Id) ?? throw new ArgumentNullException(nameof(establishment));
        finded.Name = establishment.Name;
        finded.Active = establishment.Active;
        finded.Description = establishment.Description;
        finded.UserId = establishment.UserId;
        await _context.SaveChangesAsync();
        return finded;
    }
}
