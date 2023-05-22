using AlexAstudilloERP.Infrastructure.Connections;
using EFCommonCRUD.Interfaces;
using EFCommonCRUD.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AlexAstudilloERP.Infrastructure.Repositories
{
    public class NPPostgreSQLRepository<T, ID> : INPRepository<T, ID> where T : class
    {
        private readonly PostgreSQLContext _context;

        public NPPostgreSQLRepository(PostgreSQLContext context)
        {
            _context = context;
        }

        public long Count()
        {
            return _context.Set<T>().AsNoTracking().LongCount();
        }

        public Task<long> CountAsync()
        {
            return _context.Set<T>().AsNoTracking().LongCountAsync();
        }

        public int Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChanges();
        }

        public int DeleteAll()
        {
            return _context.Set<T>().AsNoTracking().ExecuteDelete();
        }

        public int DeleteAll(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            return _context.SaveChanges();
        }

        public Task<int> DeleteAllAsync()
        {
            return _context.Set<T>().AsNoTracking().ExecuteDeleteAsync();
        }

        public Task<int> DeleteAllAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            return _context.SaveChangesAsync();
        }

        public int DeleteAllById(IEnumerable<ID> ids)
        {
            int affectedRows = 0;
            foreach (ID id in ids)
            {
                T? t = _context.Set<T>().Find(id);
                if (t != null) _context.Remove(t);
                affectedRows += _context.SaveChanges();
            }
            return affectedRows;
        }

        public async Task<int> DeleteAllByIdAsync(IEnumerable<ID> ids)
        {
            int affectedRows = 0;
            foreach (ID id in ids)
            {
                T? t = await _context.Set<T>().FindAsync(id);
                if (t != null) _context.Remove(t);
                affectedRows += await _context!.SaveChangesAsync();

            }
            return affectedRows;
        }

        public Task<int> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChangesAsync();
        }

        public int DeleteById(ID id)
        {
            T? t = _context.Set<T>().Find(id);
            if (t != null) _context.Remove(t);
            return _context.SaveChanges();
        }

        public async Task<int> DeleteByIdAsync(ID id)
        {
            int affectedRows = 0;
            T? t = await _context.Set<T>().FindAsync(id);
            if (t != null) _context.Remove(t);
            affectedRows += await _context.SaveChangesAsync();
            return affectedRows;
        }

        public bool ExistsById(ID id)
        {
            T? t = _context.Set<T>().Find(id);
            _context.SaveChanges();
            return t != null;
        }

        public async Task<bool> ExistsByIdAsync(ID id)
        {
            T? t = await _context.Set<T>().FindAsync(id);
            await _context.SaveChangesAsync();
            return t != null;
        }

        public IPage<T> FindAll(IPageable pageable)
        {
            int skip = Convert.ToInt32(pageable.GetOffset());
            int pageSize = pageable.GetPageSize();
            List<T> data = new();
            if (pageable.GetSort().IsSorted())
            {
                Sort sort = pageable.GetSort();
                List<Order> orders = sort.GetOrders();
                DbSet<T> set = _context.Set<T>();
                foreach (Order order in orders)
                {
                    if (order.IsAscending())
                    {
                        set.OrderBy(t => t.GetType().GetProperty(order.GetProperty()));
                    }
                    else
                    {
                        set.OrderByDescending(t => t.GetType().GetProperty(order.GetProperty()));
                    }
                }
                data.AddRange(set.AsNoTracking().Skip(skip).Take(pageSize).ToList());
            }
            else
            {
                data.AddRange(_context.Set<T>().AsNoTracking().Skip(skip).Take(pageSize).ToList());
            }
            return new Page<T>(data, pageable, Count());
        }

        public IEnumerable<T> FindAll()
        {
            return _context.Set<T>().AsNoTracking().ToList();
        }

        public async Task<IPage<T>> FindAllAsync(IPageable pageable)
        {
            // Get the offset and page size.
            int skip = Convert.ToInt32(pageable.GetOffset());
            int pageSize = pageable.GetPageSize();
            List<T> data = new();
            // Get sort from pageable.
            if (pageable.GetSort().IsSorted())
            {
                Sort sort = pageable.GetSort();
                List<Order> orders = sort.GetOrders();
                DbSet<T> set = _context.Set<T>();
                foreach (Order order in orders)
                {
                    if (order.IsAscending())
                    {
                        set.OrderBy(t => t.GetType().GetProperty(order.GetProperty()));
                    }
                    else
                    {
                        set.OrderByDescending(t => t.GetType().GetProperty(order.GetProperty()));
                    }
                }
                data.AddRange(await set.AsNoTracking().Skip(skip).Take(pageSize).ToListAsync());
            }
            else
            {
                data.AddRange(await _context.Set<T>().AsNoTracking().Skip(skip).Take(pageSize).ToListAsync());
            }
            return new Page<T>(data, pageable, await CountAsync());
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public IEnumerable<T> FindAllById(IEnumerable<ID> ids)
        {
            return _context.Set<T>().AsNoTracking().AsEnumerable().Where(t =>
            {
                PropertyInfo property = t.GetType().GetProperties().First(prop =>
                {
                    return prop.PropertyType == ids.First()!.GetType() || prop.Name.Equals("Id");
                });
                return ids.Contains((ID?)property.GetValue(t, null));
            }).ToList();
        }

        public Task<IEnumerable<T>> FindAllByIdAsync(IEnumerable<ID> ids)
        {
            throw new NotImplementedException();
        }

        public T? FindById(ID id)
        {
            T? entity = _context.Set<T>().Find(id);
            _context.SaveChanges();
            return entity;
        }

        public async ValueTask<T?> FindByIdAsync(ID id)
        {
            T? entity = await _context.Set<T>().FindAsync(id);
            await _context.SaveChangesAsync();
            return entity;
        }

        public T Save(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public IEnumerable<T> SaveAll(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            _context.SaveChanges();
            return entities;
        }

        public async Task<IEnumerable<T>> SaveAllAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async ValueTask<T> SaveAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public IEnumerable<T> UpdateAll(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            _context.SaveChanges();
            return entities;
        }

        public async Task<IEnumerable<T>> UpdateAllAsync(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async ValueTask<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
