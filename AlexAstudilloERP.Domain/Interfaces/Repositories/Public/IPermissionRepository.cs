using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Enums.Public;
using EFCommonCRUD.Interfaces;

namespace AlexAstudilloERP.Domain.Interfaces.Repositories.Public;

public interface IPermissionRepository : INPRepository<Permission, short>
{
    public Task<IPage<Permission>> FindAllAsync(IPageable pageable, int companyId, long userId);

    public Task<bool> HasEstablishmentPermission(long userId, int establishmentId, PermissionEnum permission);

    public Task<bool> HasPermission(long userId, int companyId, PermissionEnum permission);
}
