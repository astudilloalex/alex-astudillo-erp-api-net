namespace EFCommonCRUD.Interfaces;

/// <summary>
/// Interface for generic CRUD operations on a repository for a specific type.
/// </summary>
/// <typeparam name="T">The entity type.</typeparam>
/// <typeparam name="ID">The type of the entity identifier.</typeparam>
public interface ICrudRepository<T, ID> where T : class
{
    /// <summary>
    /// Returns the number of entities available.
    /// </summary>
    /// <returns>The number of entities.</returns>
    public long Count();

    /// <summary>
    /// Returns async the number of entities available.
    /// </summary>
    /// <returns>The number of entities.</returns>
    public Task<long> CountAsync();

    /// <summary>
    /// Deletes a given entity.
    /// </summary>
    /// <param name="entity">Must not be null.</param>
    /// <returns>The number of affected rows</returns>
    public int Delete(T entity);

    /// <summary>
    /// Deletes all entities in the database.
    /// </summary>
    /// <returns>The number of affected rows.</returns>
    public int DeleteAll();

    /// <summary>
    /// Deletes the given entities.
    /// </summary>
    /// <param name="entities">Must not be <c>null</c>. Must not contain <c>null</c> elements.</param>
    /// <returns>The number of affected rows.</returns>
    public int DeleteAll(IEnumerable<T> entities);

    /// <summary>
    /// Deletes all entities in the database async.
    /// </summary>
    /// <returns>The number of affected rows.</returns>
    public Task<int> DeleteAllAsync();

    /// <summary>
    /// Deletes async the given entities.
    /// </summary>
    /// <param name="entities">Must not be <c>null</c>. Must not contain <c>null</c> elements.</param>
    /// <returns>The number of affected rows</returns>
    public Task<int> DeleteAllAsync(IEnumerable<T> entities);

    /// <summary>
    /// Deletes async a given entity.
    /// </summary>
    /// <param name="entity">Must not be null.</param>
    /// <returns>The number of affected rows</returns>
    public Task<int> DeleteAsync(T entity);

    /// <summary>
    /// Deletes all instances of the type <c>T</c> with the given IDs.
    /// Entities that aren't found in the persistence store are silently ignored.
    /// 
    /// Prefer <c>DeleteAll</c> method for better performance.
    /// </summary>
    /// <param name="ids">Must not be null and must not contain null elements.</param>
    /// <returns>The number of affected rows</returns>
    public int DeleteAllById(IEnumerable<ID> ids);

    /// <summary>
    /// Deletes all instances of the type <c>T</c> with the given IDs async.
    /// Entities that aren't found in the persistence store are silently ignored.
    /// <para>
    /// Use <see cref="DeleteAllAsync(IEnumerable{T})"/> for better performance.
    /// </para>
    /// </summary>
    /// <param name="ids">Must not be null and must not contain null elements.</param>
    /// <returns>The number of affected rows</returns>
    public Task<int> DeleteAllByIdAsync(IEnumerable<ID> ids);

    /// <summary>
    /// Deletes the entity with the given id.
    /// If the entity is not found in the persistence store it is silently ignored.
    /// </summary>
    /// <param name="id">Must not be null.</param>
    /// <returns>The number of affected rows</returns>
    public int DeleteById(ID id);

    /// <summary>
    /// Deletes the entity with the given id async.
    /// If the entity is not found in the persistence store it is silently ignored.
    /// </summary>
    /// <param name="id">Must not be null.</param>
    /// <returns>The number of affected rows</returns>
    public Task<int> DeleteByIdAsync(ID id);

    /// <summary>
    /// Returns whether an entity with the given id exists.
    /// </summary>
    /// <param name="id">Must not be null.</param>
    /// <returns>If an entity with the given id exists, false otherwise.</returns>
    public bool ExistsById(ID id);

    /// <summary>
    /// Returns whether an entity with the given id exists async.
    /// </summary>
    /// <param name="id">Must not be null.</param>
    /// <returns>If an entity with the given id exists, false otherwise.</returns>
    public Task<bool> ExistsByIdAsync(ID id);

    /// <summary>
    /// Returns all instances of the type.
    /// </summary>
    /// <returns>All entities.</returns>
    public IEnumerable<T> FindAll();

    /// <summary>
    /// Returns async all instances of the type.
    /// </summary>
    /// <returns>All entities.</returns>
    public Task<IEnumerable<T>> FindAllAsync();

    /// <summary>
    /// Returns all instances of the type <c>T</c> with the given IDs.
    /// <para>If some or all ids are not found, no entities are returned for these IDs.</para>
    /// <b>
    /// Note that the order of elements in the result is not guaranteed. 
    /// </b>
    /// <para>For large lists of data this function can affect performance.</para>
    /// </summary>
    /// <param name="ids">Must not be <c>null</c> nor contain any <c>null</c> values</param>
    /// <returns>Guaranteed to be not <c>null</c>. The size can be equal or less than the number of given <c>ids</c></returns>
    public IEnumerable<T> FindAllById(IEnumerable<ID> ids);

    /// <summary>
    /// Returns async all instances of the type <c>T</c> with the given IDs.
    /// <para>If some or all ids are not found, no entities are returned for these IDs.</para>
    /// <b>Note that the order of elements in the result is not guaranteed.</b>
    /// <para>For large lists of data this function can affect performance.</para>
    /// </summary>
    /// <param name="ids">Must not be <c>null</c> nor contain any <c>null</c> values</param>
    /// <returns>Guaranteed to be not <c>null</c>. The size can be equal or less than the number of given <c>ids</c></returns>
    public Task<IEnumerable<T>> FindAllByIdAsync(IEnumerable<ID> ids);

    /// <summary>
    /// Retrieves an entity by its id.
    /// </summary>
    /// <param name="id">Must not be null.</param>
    /// <returns>The entity with the given id or null if not exist.</returns>
    public T? FindById(ID id);

    /// <summary>
    /// Retrieves an entity by its id async.
    /// </summary>
    /// <param name="id">Must not be null.</param>
    /// <returns>The entity with the given id or null if not exist.</returns>
    public ValueTask<T?> FindByIdAsync(ID id);

    /// <summary>
    /// Saves a given entity. Use the returned instance for further operations as the save operation might have changed the
    /// entity instance completely.
    /// </summary>
    /// <param name="entity">Must not be null.</param>
    /// <returns>The saved entity; will never be null.</returns>
    public T Save(T entity);

    /// <summary>
    /// Saves a given entity async. Use the returned instance for further operations as the save operation might have changed the
    /// entity instance completely.
    /// </summary>
    /// <param name="entity">Must not be null.</param>
    /// <returns>The saved entity async; will never be null.</returns>
    public ValueTask<T> SaveAsync(T entity);

    /// <summary>
    /// Saves all given entities.
    /// </summary>
    /// <param name="entities">Must not be null or contain null.</param>
    /// <returns>
    ///     The saved entities; will never be null. The returned List will have the same size
    ///     as the List passed as an argument.
    /// </returns>
    public IEnumerable<T> SaveAll(IEnumerable<T> entities);

    /// <summary>
    /// Saves all given entities async.
    /// </summary>
    /// <param name="entities">Must not be null or contain null.</param>
    /// <returns>
    ///     The saved entities async; will never be null. The returned List will have the same size
    ///     as the List passed as an argument.
    /// </returns>
    public Task<IEnumerable<T>> SaveAllAsync(IEnumerable<T> entities);

    /// <summary>
    /// Update a given entity. Use the returned instance for further operations as the update operation might have changed the
    /// entity instance completely.
    /// </summary>
    /// <param name="entity">Must not be null.</param>
    /// <returns>The updated entity; will never be null.</returns>
    public T Update(T entity);

    /// <summary>
    /// Update async a given entity. Use the returned instance for further operations as the update operation might have changed the
    /// entity instance completely.
    /// </summary>
    /// <param name="entity">Must not be null.</param>
    /// <returns>The updated entity; will never be null.</returns>
    public ValueTask<T> UpdateAsync(T entity);

    /// <summary>
    /// Update all given entities.
    /// </summary>
    /// <param name="entities">Must not be null or contain null.</param>
    /// <returns>
    ///     The updated entities; will never be null. The returned List will have the same size
    ///     as the List passed as an argument.
    /// </returns>
    public IEnumerable<T> UpdateAll(IEnumerable<T> entities);

    /// <summary>
    /// Update async all given entities.
    /// </summary>
    /// <param name="entities">Must not be null and not contain null elements</param>
    /// <returns>
    ///     The updated entities; will never be null. The returned List will have the same size
    ///     as the List passed as an argument.
    /// </returns>
    public Task<IEnumerable<T>> UpdateAllAsync(IEnumerable<T> entities);
}
