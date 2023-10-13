﻿using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;

namespace AlexAstudilloERP.Application.Services.Public;

public class MenuService : IMenuService
{
    private readonly IMenuRepository _repository;

    public MenuService(IMenuRepository repository)
    {
        _repository = repository;
    }

    public Task<List<Menu>> GetByUserCodeAndCompanyCodeAsync(string userCode, string companyCode)
    {
        return _repository.FindByUserCodeAndCompanyCodeAsync(userCode, companyCode);
    }

    public Task<List<Menu>> GetParentsAsync(string userCode, string companyCode)
    {
        if (string.IsNullOrEmpty(companyCode))
        {
            return _repository.FindParentsAsync(userCode, companyCode, true);
        }
        return _repository.FindParentsAsync(userCode, companyCode);
    }
}
