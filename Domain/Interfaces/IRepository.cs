using System.Linq.Expressions;

namespace Domain.Interfaces;

public interface IRepository<T>
{
    public Task<IEnumerable<T>> GetAll();
    public Task<T?> GetById(int id);
    public Task<T?> GetBy(Expression<Func<T, bool>> predicate);
    public Task<T> Create(T data);
    public T Update(int id, T data);
    public void Delete(T data);
}