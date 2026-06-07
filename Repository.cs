using GenericOps.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace GenericOps
{
    public class Repository<T>(ApplicationContext _Context) : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbset = _Context.Set<T>();
        Func<int, int> funcdelegate = (changes) => changes;

        public async Task<int> DeleteEntity(T entity)
        {
          // T? entity = _dbset.Where<T>(filter).FirstOrDefault();
            Func<int, int> funcDelete;
            EntityEntry<T>? entityEntry;

            entityEntry = _dbset.Remove(entity);
            funcDelete = (param) => ((int)entityEntry.State);

            return await _Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll(Func<T, bool>? predicate = null)
        {
            IEnumerable<T> query = _dbset;
            if (predicate is not null)
            {
                query = query.Where(predicate).ToList();
            }
            return query;
        }

        public async Task<TResult?>
            GetByIdAsync<TResult>(Expression<Func<T, bool>>? predicate = null,
                                                         Expression<Func<T, TResult>>? selector = null)
        {
            IQueryable<T> query = _dbset;
            if (predicate is not null)
            {
                query = query.Where(predicate);
            }

            return await query.Select(selector).FirstOrDefaultAsync();
        }



        public async Task<int> PostEntity(T entity)
        {
            await _dbset.AddAsync(entity);
            int postResult = funcdelegate(await _Context.SaveChangesAsync());
            return postResult;
        }



        public async Task<int> UpdateEntity(Func<T, bool> filter, Action<T> updateAction)
        {
            T? entity = _dbset.AsEnumerable().FirstOrDefault(filter);
            updateAction(entity);
            _Context.Entry(entity).State = EntityState.Modified;
            return await _Context.SaveChangesAsync();
        }

    }
}
