using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        private readonly AppDbContext _context;
        public RepositoryBase(AppDbContext context) =>
            _context = context;

        public async Task CreateAsync(T entity) =>
            await _context.Set<T>().AddAsync(entity);

        public void Delete(T entity) =>
            _context.Set<T>().Remove(entity);

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate) =>
            await _context.Set<T>().AnyAsync(predicate);

        public IQueryable<T> FindMany(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
            trackChanges ? 
                _context.Set<T>().Where(expression) : 
                _context.Set<T>().AsNoTracking().Where(expression);

        public IQueryable<T> FindMany(bool trackChanges = false) =>
            trackChanges ? 
                _context.Set<T>() : 
                _context.Set<T>().AsNoTracking();

        public async Task<T?> FindOneAsync(Expression<Func<T, bool>> expression, bool trackChanges = true) =>
            trackChanges ? 
                await _context.Set<T>().Where(expression).FirstOrDefaultAsync() : 
                await _context.Set<T>().AsNoTracking().Where(expression).FirstOrDefaultAsync();

        public void SoftDelete(T entity)
        {
            entity.IsDeprecated = true;
            Update(entity);
        }

        public void Update(T entity) =>
            _context.Set<T>().Update(entity);
    }
}
