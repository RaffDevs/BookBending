using Domain.Interfaces;

namespace Api.Repositories.Interfaces;

public interface IUnitOfWork<T>
{
     IRepository<T> Repository { get; }
     public Task Commit();
}