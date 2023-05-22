using AlexAstudilloERP.Domain.Entities.Common;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Common;
using AlexAstudilloERP.Infrastructure.Connections;

namespace AlexAstudilloERP.Infrastructure.Repositories.Common;

public class DatabaseTableRepository : NPPostgreSQLRepository<DatabaseTable, short>, IDatabaseTableRepository
{
    public DatabaseTableRepository(PostgreSQLContext context) : base(context)
    {
    }
}
