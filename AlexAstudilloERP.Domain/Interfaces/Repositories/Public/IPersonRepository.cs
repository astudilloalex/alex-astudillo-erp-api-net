﻿using AlexAstudilloERP.Domain.Entities.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Domain.Interfaces.Repositories.Public;

public interface IPersonRepository : INPRepository<Person, long>
{
    public Task<bool> ExistsIdCard(string idCard);
    public Task<Person?> FindByIdCard(string idCard);
}
