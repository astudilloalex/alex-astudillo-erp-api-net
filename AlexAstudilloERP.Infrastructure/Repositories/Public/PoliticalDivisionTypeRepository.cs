using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Infrastructure.Connections;

namespace AlexAstudilloERP.Infrastructure.Repositories.Public;

public class PoliticalDivisionTypeRepository(PostgreSQLContext context) : NPPostgreSQLRepository<PoliticalDivisionType, short>(context), IPoliticalDivisionTypeRepository
{
}
