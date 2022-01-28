namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IRepositoryBase<T> Set<T>() where T : class;
        IUserRepository User { get; }
    }
}
