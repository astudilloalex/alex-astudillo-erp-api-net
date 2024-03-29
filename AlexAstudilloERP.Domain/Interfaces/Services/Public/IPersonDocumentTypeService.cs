﻿using AlexAstudilloERP.Domain.Entities.Public;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Public;

public interface IPersonDocumentTypeService
{
    public Task<List<PersonDocumentType>> GetAll(bool? onlyActive = null);

    public Task<List<PersonDocumentType>> GetByCountryCodeAsync(string countryCode, bool? onlyActive = null);
}
