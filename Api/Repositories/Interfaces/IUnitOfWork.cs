using Domain.Interfaces;

namespace Api.Repositories.Interfaces;

public interface IUnitOfWork<T>
{
    public IRepository<T> Repository { get; }

     public Task Commit();
}