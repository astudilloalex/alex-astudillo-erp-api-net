using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace AlexAstudilloERP.Infrastructure.Repositories.Public;

public class EmailRepository : NPPostgreSQLRepository<Email, int>, IEmailRepository
{
    private readonly PostgreSQLContext _context;

    public EmailRepository(PostgreSQLContext context) : base(context)
    {
        _context = context;
    }

    public Task<bool> ExistsMail(string mail)
    {
        //return _context.Emails.AsNoTracking().AnyAsync(e => e.Mail.Equals(mail));
        throw new NotImplementedException();
    }

    public Task<Email?> FindByMail(string mail)
    {
        //return _context.Emails.AsNoTracking().FirstOrDefaultAsync(e => e.Mail.Equals(mail));
        throw new NotImplementedException();
    }
}
