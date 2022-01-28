namespace Contracts
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
