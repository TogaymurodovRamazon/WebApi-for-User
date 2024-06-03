using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi_for_User.AppDBContext;
using WebApi_for_User.IRepository;
using WebApi_for_User.Models;

namespace WebApi_for_User.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : User
    {
        private readonly UserContext _userContext;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(UserContext userContext)
        {
            _userContext = userContext;
            _dbSet = _userContext.Set<T>();
        }
        public async ValueTask<T> CreateAsync(T entity) =>
            (await _userContext.AddAsync(entity)).Entity;

        public async ValueTask<bool> DeleteAsync(Expression<Func<T, bool>> expression)
        {
           var entity=await GetAsync(expression);
            if(entity==null)
                return false;
            _dbSet.Remove(entity);
            return true;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            IQueryable<T> query = expression is null? _dbSet: _dbSet.Where(expression);
            if (includes != null)
                foreach (var all in includes)
                    if (!string.IsNullOrEmpty(all))
                        query = query.Include(all);
            return query;
        }

        public async ValueTask<T> GetAsync(Expression<Func<T, bool>> expression, string[] includes = null) =>
            await _dbSet.Where(expression).FirstOrDefaultAsync();

        public async ValueTask SaveChangesAsync()=>
            await _userContext.SaveChangesAsync();

        public T Update(T entity) =>
            _dbSet.Update(entity).Entity;
    }
}
