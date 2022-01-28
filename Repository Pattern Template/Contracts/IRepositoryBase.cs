using System.Linq.Expressions;

namespace Contracts
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        Task<T?> FindById(Guid id);
        Task<T?> Find(Expression<Func<T, bool>> expression);
        Task Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task CreateMany(List<T> entities);
        void UpdateMany(List<T> entities);
        void DeleteMany(List<T> entities);
    }
}