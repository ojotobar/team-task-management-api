using System.Linq.Expressions;

namespace Contracts
{
    public interface IRepositoryBase<T>
    {
        Task CreateAsync(T entity);
        void Update(T entity);
        void SoftDelete(T entity);
        void Delete(T entity);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task<T?> FindOneAsync(Expression<Func<T, bool>> expression, bool trackChanges = true);
        IQueryable<T> FindMany(Expression<Func<T, bool>> expression, bool trackChanges = false);
        IQueryable<T> FindMany(bool trackChanges = false);
        Task CreateRangeAsync(List<T> entities);
    }
}