using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace AlexAstudilloERP.Infrastructure.Repositories.Public;

public class CustomerRepository : NPPostgreSQLRepository<Customer, long>, ICustomerRepository
{
    private readonly PostgreSQLContext _context;

    public CustomerRepository(PostgreSQLContext context) : base(context)
    {
        _context = context;
    }

    public new ValueTask<Customer> SaveAsync(Customer entity)
    {
        throw new NotImplementedException();
    }

    public Task<Customer?> FindByIdCard(string idCard)
    {
        return _context.Customers.AsNoTracking()
            .Include(c => c.Person)
            .FirstOrDefaultAsync(c => c.Person!.IdCard.Equals(idCard));
    }

    public Task<Customer?> FindByIdCardAndCompanyId(int companyId, string idCard)
    {
        return _context.Customers.AsNoTracking()
            .Include(c => c.Person)
            .FirstOrDefaultAsync(c => c.Person!.IdCard.Equals(idCard) && c.Companies.Select(com => com.Id).Contains(companyId));
    }
}
