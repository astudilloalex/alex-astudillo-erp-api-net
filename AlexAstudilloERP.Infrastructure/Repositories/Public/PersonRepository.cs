using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace AlexAstudilloERP.Infrastructure.Repositories.Public;

public class PersonRepository : NPPostgreSQLRepository<Person, long>, IPersonRepository
{
    private readonly PostgreSQLContext _context;

    public PersonRepository(PostgreSQLContext context) : base(context)
    {
        _context = context;
    }

    public Task<bool> ExistsIdCard(string idCard)
    {
        return _context.People.AsNoTracking().AnyAsync(p => p.IdCard.Equals(idCard));
    }

    public Task<Person?> FindByIdCard(string idCard)
    {
        return _context.People.AsNoTracking().FirstOrDefaultAsync(p => p.IdCard.Equals(idCard));
    }

    public Task<string?> FindCodeByIdCard(string idCard)
    {
        return _context.People.AsNoTracking()
            .Where(p => p.IdCard.Equals(idCard))
            .Select(p => p.Code)
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Save or update a person
    /// <para>If id card exist update the person, otherwise create a new person.</para>
    /// </summary>
    /// <param name="person">The person to add or update.</param>
    /// <returns>The person updated.</returns>
    public async Task<Person> SaveOrUpdate(Person person)
    {
        Person? finded = await _context.People.FirstOrDefaultAsync(p => p.IdCard.Equals(person.IdCard));
        if (finded != null)
        {
            finded.Birthdate = person.Birthdate;
            finded.FirstName = person.FirstName;
            finded.LastName = person.LastName;
            finded.GenderId = person.GenderId;
            finded.JuridicalPerson = person.JuridicalPerson;
            finded.PersonDocumentTypeId = person.PersonDocumentTypeId;
            finded.SocialReason = person.SocialReason;
            await _context.SaveChangesAsync();
            return finded;
        }
        else
        {
            await _context.AddAsync(person);
            await _context.SaveChangesAsync();
            return person;
        }
    }
}
