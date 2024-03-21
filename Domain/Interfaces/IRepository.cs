namespace Domain.Interfaces;

public interface IRepository<T> where T : class
{
    public Task<IEnumerable<T>> GetAll();
    public Task<T> GetById();
    public Task<T> Create(T data);
    public Task<T> Update(int id, T data);
    public Task Delete(T data);
}