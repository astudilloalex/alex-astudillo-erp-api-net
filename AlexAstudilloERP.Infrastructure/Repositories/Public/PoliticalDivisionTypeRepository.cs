using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Infrastructure.Connections;

namespace AlexAstudilloERP.Infrastructure.Repositories.Public;

public class PoliticalDivisionTypeRepository : NPPostgreSQLRepository<PoliticalDivisionType, short>, IPoliticalDivisionTypeRepository
{
    public PoliticalDivisionTypeRepository(PostgreSQLContext context) : base(context)
    {
    }
}
