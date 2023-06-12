using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Enums.Custom;
using AlexAstudilloERP.Domain.Enums.Public;
using AlexAstudilloERP.Domain.Exceptions.Forbidden;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Custom;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;

namespace AlexAstudilloERP.Application.Services.Public;

public class EstablishmentService : IEstablishmentService
{
    private readonly ITokenService _tokenService;
    private readonly IEstablishmentRepository _repository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly ISetData _setData;
    private readonly IValidateData _validateData;

    public EstablishmentService(ITokenService tokenService, IEstablishmentRepository repository,
        IPermissionRepository permissionRepository, ISetData setData,
        IValidateData validateData)
    {
        _repository = repository;
        _tokenService = tokenService;
        _permissionRepository = permissionRepository;
        _setData = setData;
        _validateData = validateData;
    }

    public async Task<Establishment> Add(Establishment establishment, string token)
    {
        long userId = _tokenService.GetUserId(token);
        establishment.UserId = userId;
        bool permitted = await _permissionRepository.HasPermission(userId, establishment.CompanyId, PermissionEnum.EstablishmentCreate);
        if (!permitted) throw new ForbiddenException(ExceptionEnum.Forbidden);
        _setData.SetEstablishmentData(establishment);
        await _validateData.ValidateEstablishment(establishment: establishment, update: false);
        if (establishment.Address != null)
        {
            _setData.SetAddressData(establishment.Address);
            await _validateData.ValidateAddress(address: establishment.Address, update: false);
        }
        return await _repository.SaveAsync(establishment);
    }
}
