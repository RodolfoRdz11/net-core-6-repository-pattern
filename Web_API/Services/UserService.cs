using AutoMapper;
using Contracts;

namespace Web_API.Services
{
    public interface IUserService : IServiceBase<IUserRepository, User, UserRequest, UserResponse>
    {

    }

    public class UserService : ServiceBase<IUserRepository, User, UserRequest, UserResponse>, IUserService
    {
        public UserService(IRepositoryWrapper repository, IMapper mapper, IUnitOfWork unitOfWork) : base(repository, mapper, unitOfWork)
        {

        }
    }
}
