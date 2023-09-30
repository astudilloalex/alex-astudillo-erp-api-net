using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Enums.Custom;
using AlexAstudilloERP.Domain.Enums.Public;
using AlexAstudilloERP.Domain.Exceptions.Forbidden;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Custom;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Application.Services.Public;

public class EstablishmentService : IEstablishmentService
{
    private readonly IEstablishmentRepository _repository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly ISetData _setData;
    private readonly IValidateData _validateData;

    public EstablishmentService(IEstablishmentRepository repository,
        IPermissionRepository permissionRepository, ISetData setData,
        IValidateData validateData)
    {
        _repository = repository;
        _permissionRepository = permissionRepository;
        _setData = setData;
        _validateData = validateData;
    }

    public async Task<Establishment> Add(Establishment establishment, string token)
    {
        //long userId = _tokenService.GetUserId(token);
        //establishment.UserId = userId;
        //bool permitted = await _permissionRepository.HasPermission(userId, establishment.CompanyId, PermissionEnum.EstablishmentCreate);
        //if (!permitted) throw new ForbiddenException(ExceptionEnum.Forbidden);
        //_setData.SetEstablishmentData(establishment);
        //await _validateData.ValidateEstablishment(establishment: establishment, update: false);
        //if (establishment.Address != null)
        //{
        //    _setData.SetAddressData(establishment.Address);
        //    await _validateData.ValidateAddress(address: establishment.Address, update: false);
        //}
        return await _repository.SaveAsync(establishment);
    }

    public async Task<IPage<Establishment>> FindByCompanyId(IPageable pageable, int companyId, string token)
    {
        //long userId = _tokenService.GetUserId(token);
        bool permitted = await _permissionRepository.HasPermission(1, companyId, PermissionEnum.EstablishmentList);
        if (!permitted) throw new ForbiddenException(ExceptionEnum.Forbidden);
        return await _repository.FindByCompanyId(pageable, companyId);
    }

    public async Task<Establishment?> GetByCode(string code, string token)
    {
        //long userId = _tokenService.GetUserId(token);
        Establishment? establishment = await _repository.FindByCode(code);
        if (establishment == null) return null;
        bool permitted = await _permissionRepository.HasEstablishmentPermission(1L, establishment.Id, PermissionEnum.EstablishmentGet);
        if (!permitted) throw new ForbiddenException(ExceptionEnum.Forbidden);
        return establishment;
    }

    public async Task<Establishment> SetMain(int id, string token)
    {
        //long userId = _tokenService.GetUserId(token);
        bool permitted = await _permissionRepository.HasEstablishmentPermission(1, id, PermissionEnum.EstablishmentUpdate);
        if (!permitted) throw new ForbiddenException(ExceptionEnum.Forbidden);
        return await _repository.SetMain(id, 1);
    }

    public async Task<Establishment> Update(Establishment establishment, string token)
    {
        //long userId = _tokenService.GetUserId(token);
        //establishment.UserId = userId;
        bool permitted = await _permissionRepository.HasEstablishmentPermission(1, establishment.Id, PermissionEnum.EstablishmentUpdate);
        if (!permitted) throw new ForbiddenException(ExceptionEnum.Forbidden);
        _setData.SetEstablishmentData(establishment, update: true);
        //await _validateData.ValidateEstablishment(establishment: establishment, update: true);
        return await _repository.UpdateAsync(establishment);
    }
}
