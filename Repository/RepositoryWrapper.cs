using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private DataContext dataContext;
        private IUserRepository user;

        public RepositoryWrapper(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IRepositoryBase<T> Set<T>() where T : class =>
            new RepositoryBase<T>(dataContext);

        public IUserRepository User
        {
            get
            {
                if(user == null)
                    user = new UserRepository(dataContext);
                return user;
            }
        }

    }
}
