using AlexAstudilloERP.Domain.Entities.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Infrastructure.Connections;

namespace AlexAstudilloERP.Infrastructure.Repositories.Public;

public class EstablishmentRepository : NPPostgreSQLRepository<Establishment, int>, IEstablishmentRepository
{
    public EstablishmentRepository(PostgreSQLContext context) : base(context)
    {
    }
}
