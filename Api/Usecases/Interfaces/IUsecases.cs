namespace Api.Usecases;

public interface IUsecases<T, M>
{
    public Task<IEnumerable<T>> GetAll();
    public Task<T> GetById(int id);
    public Task<T> Create(M data);
    public Task<T> Update(int id, M data);
    public Task Delete(int id);

}