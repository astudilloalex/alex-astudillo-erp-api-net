using AlexAstudilloERP.Domain.Entities.Public;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Public;

public interface IRoleService
{
    public Task<Role> Add(Role role, string token);
    public Task<Role> Update(Role role, string token);
}
