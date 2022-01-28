using Contracts;
using Entities;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;

        public UnitOfWork(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task CompleteAsync()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
