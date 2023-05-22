using static EFCommonCRUD.Interfaces.IPagingAndSortingRepository;

namespace EFCommonCRUD.Interfaces;

/// <summary>
/// Extension of <see cref="IPagingAndSortingRepository{T, ID}"/> to abstract.
/// </summary>
/// <typeparam name="T">The type of entity</typeparam>
/// <typeparam name="ID">The unique identifier type of entity.</typeparam>
public interface INPRepository<T, ID> : IPagingAndSortingRepository<T, ID> where T : class
{
}
