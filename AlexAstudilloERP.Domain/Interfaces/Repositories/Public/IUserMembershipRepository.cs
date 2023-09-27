using AlexAstudilloERP.Domain.Entities.Public;

namespace AlexAstudilloERP.Domain.Interfaces.Repositories.Public;

public interface IUserMembershipRepository
{
    public Task<List<UserMembership>> FindByUserCode(string userCode);
}
