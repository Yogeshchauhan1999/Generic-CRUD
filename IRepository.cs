using System.Linq.Expressions;

namespace GenericOps
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(Func<T, bool> predicate = null);
        Task<int> PostEntity(T entity);

        Task<TResult> GetByIdAsync<TResult>(
       Expression<Func<T, bool>>? predicate = null,
       Expression<Func<T, TResult>>? selector = null
       );

        Task<int> UpdateEntity(Func<T,bool> filter,Action<T> updateAction);

        Task<int> DeleteEntity(T entity);
    }
}
