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

    public new async ValueTask<Customer> SaveAsync(Customer entity)
    {
        // Start transaction.
        using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                // Find a person by id card.
                Person? person = await _context.People.FirstOrDefaultAsync(p => p.IdCard.Equals(entity.Person!.IdCard));
                if (person != null)
                {
                    //person.DocumentTypeId = entity.Person!.DocumentTypeId;
                    person.Birthdate = entity.Person!.Birthdate;
                    person.FirstName = entity.Person!.FirstName;
                    person.LastName = entity.Person!.LastName;
                    person.SocialReason = entity.Person!.SocialReason;
                    // Set null person entity and set the unique identifier.
                    entity.Person = null;
                    entity.PersonId = person.Id;
                    await _context.SaveChangesAsync();
                }
                Customer? customer = await _context.Customers.FirstOrDefaultAsync(c => c.PersonId == entity.PersonId);
                if (customer == null)
                {
                    await _context.Customers.AddAsync(entity);
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        return entity;
    }

    public Task<Customer?> FindByIdCard(string idCard)
    {
        return _context.Customers.AsNoTracking()
            .Include(c => c.Person)
            .FirstOrDefaultAsync(c => c.Person!.IdCard.Equals(idCard));
    }

    public Task<Customer?> FindByIdCardAndCompanyId(int companyId, string idCard)
    {
        //return _context.Customers.AsNoTracking()
        //    .Include(c => c.Person)
        //    .FirstOrDefaultAsync(c => c.Person!.IdCard.Equals(idCard) && c.CompanyCustomers.Select(cc => cc.CompanyId).Contains(companyId));
        throw new NotImplementedException();
    }
}
