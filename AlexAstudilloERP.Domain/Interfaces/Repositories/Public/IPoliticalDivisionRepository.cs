﻿using AlexAstudilloERP.Domain.Entities.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Domain.Interfaces.Repositories.Public;

public interface IPoliticalDivisionRepository : INPRepository<PoliticalDivision, int>
{
    public Task<List<PoliticalDivision>> FindByParentId(int parentId);
    public Task<List<PoliticalDivision>> FindByTypeId(short typeId);
}
