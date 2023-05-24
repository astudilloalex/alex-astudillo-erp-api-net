using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace AlexAstudilloERP.Infrastructure.Repositories.Public;

internal class EmailRepository : NPPostgreSQLRepository<Email, int>, IEmailRepository
{
    private readonly PostgreSQLContext _context;

    public EmailRepository(PostgreSQLContext context) : base(context)
    {
        _context = context;
    }

    public Task<bool> ExistsMail(string mail)
    {
        return _context.Emails.AsNoTracking().AnyAsync(e => e.Mail.Equals(mail));
    }
}
