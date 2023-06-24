using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Infrastructure.Connections;

namespace AlexAstudilloERP.Infrastructure.Repositories.Public;

public class RoleRepository : NPPostgreSQLRepository<Role, int>, IRoleRepository
{
    public RoleRepository(PostgreSQLContext context) : base(context)
    {
    }
}
