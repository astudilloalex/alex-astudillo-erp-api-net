﻿using AlexAstudilloERP.Domain.Entities.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Domain.Interfaces.Repositories.Public;

public interface IEstablishmentRepository : INPRepository<Establishment, int>
{
    public Task<Establishment?> FindMainByCompanyId(int companyId);
}
