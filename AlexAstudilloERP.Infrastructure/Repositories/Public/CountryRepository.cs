﻿using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace AlexAstudilloERP.Infrastructure.Repositories.Public;

public class CountryRepository(PostgreSQLContext context) : NPPostgreSQLRepository<Country, short>(context), ICountryRepository
{
    private readonly PostgreSQLContext _context = context;

    public Task<Country?> FindByCode(string code)
    {
        return _context.Countries.AsNoTracking().FirstOrDefaultAsync(c => c.Code.Equals(code));
    }

    public Task<string?> FindCodeByPersonDocumentTypeId(short documentTypeId)
    {
        return _context.Countries.AsNoTracking()
            .Where(c => c.PersonDocumentTypes.Select(pdt => pdt.Id).Contains(documentTypeId))
            .Select(c => c.Code)
            .FirstOrDefaultAsync();
    }
}
