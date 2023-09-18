using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Enums.Custom;
using AlexAstudilloERP.Domain.Enums.Public;
using AlexAstudilloERP.Domain.Exceptions.Forbidden;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Custom;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using System.ComponentModel.Design;

namespace AlexAstudilloERP.Application.Services.Public;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly ITokenService _tokenService;

    public CustomerService(ICustomerRepository repository, IPermissionRepository permissionRepository, ITokenService tokenService)
    {
        _repository = repository;
        _permissionRepository = permissionRepository;
        _tokenService = tokenService;
    }

    public async Task<Customer> Add(Customer customer, string token)
    {
        long userId = _tokenService.GetUserId(token);
        //bool permited = await _permissionRepository.HasPermission(userId, customer.CompanyCustomers.First().CompanyId, PermissionEnum.CustomerGet);
        //if (!permited) throw new ForbiddenException(ExceptionEnum.Forbidden);
        throw new NotImplementedException();
    }

    public Task<Customer?> GetByIdCard(string idCard, string token)
    {
        throw new NotImplementedException();
    }

    public async Task<Customer?> GetByIdCardAndCompanyId(int companyId, string idCard, string token)
    {
        long userId = _tokenService.GetUserId(token);
        bool permited = await _permissionRepository.HasPermission(userId, companyId, PermissionEnum.CustomerGet);
        if (!permited) throw new ForbiddenException(ExceptionEnum.Forbidden);
        return await _repository.FindByIdCardAndCompanyId(companyId, idCard);
    }
}
