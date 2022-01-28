using System.Linq.Expressions;
using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DataContext repositoryContext { get; set; }

        public RepositoryBase(DataContext dataContext)
        {
            repositoryContext = dataContext;
        }

        public async Task Create(T entity) => 
            await repositoryContext.Set<T>().AddAsync(entity);

        public async Task CreateMany(List<T> entities) => 
            await repositoryContext.Set<T>().AddRangeAsync(entities);        

        public void Delete(T entity) => 
            repositoryContext.Set<T>().Remove(entity);

        public void DeleteMany(List<T> entities) => 
            repositoryContext.Set<T>().RemoveRange(entities);

        public IQueryable<T> FindAll() => 
            repositoryContext.Set<T>().AsNoTracking();
        
        public async Task<T> FindById(Guid id) => 
            await repositoryContext.Set<T>().FindAsync(id);

        public void Update(T entity) => 
            repositoryContext.Set<T>().Update(entity);

        public void UpdateMany(List<T> entities) => 
            repositoryContext.Set<T>().UpdateRange(entities);

        public async Task<T> Find(Expression<Func<T, bool>> expression) => 
            await repositoryContext.Set<T>().Where(expression).AsNoTracking().FirstOrDefaultAsync();

    }
}