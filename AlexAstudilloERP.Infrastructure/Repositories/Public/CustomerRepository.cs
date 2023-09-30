using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AlexAstudilloERP.Infrastructure.Repositories.Public;

public class CustomerRepository : NPPostgreSQLRepository<Customer, long>, ICustomerRepository
{
    private readonly PostgreSQLContext _context;

    public CustomerRepository(PostgreSQLContext context) : base(context)
    {
        _context = context;
    }

    public Task<bool> ExistsByCompanyIdAndIdCardAsync(int companyId, string idCard)
    {
        return _context.Customers.AsNoTracking()
            .AnyAsync(c => c.CompanyId == companyId && c.Person!.IdCard.Equals(idCard));
    }

    public new async ValueTask<Customer> SaveAsync(Customer entity)
    {
        await _context.Customers.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public Task<Customer?> FindByIdCard(string idCard)
    {
        return _context.Customers.AsNoTracking()
            .Include(c => c.Person)
            .FirstOrDefaultAsync(c => c.Person!.IdCard.Equals(idCard));
    }

    public Task<Customer?> FindByIdCardAndCompanyIdAsync(int companyId, string idCard)
    {
        //return _context.Customers.AsNoTracking()
        //    .Include(c => c.Person)
        //    .FirstOrDefaultAsync(c => c.Person!.IdCard.Equals(idCard) && c.CompanyCustomers.Select(cc => cc.CompanyId).Contains(companyId));
        throw new NotImplementedException();
    }


}
