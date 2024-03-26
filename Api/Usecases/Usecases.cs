using Api.Repositories.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Shared.Errors;

namespace Api.Usecases;

public class Usecases<T,M> : IUsecases<T,M>
{

    private readonly IRepository<T> _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork<T> _uof;

    public Usecases(IRepository<T> repository, IMapper mapper, IUnitOfWork<T> uof)
    {
        _repository = repository;
        _mapper = mapper;
        _uof = uof;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<T> GetById(int id)
    {
        var item = await _repository.GetById(id);

        if (item is null)
        {
            throw NotFoundError.Builder("No records found for this id!", null);
        }

        return item;
    }

    public async Task<T> Create(M data)
    {
        var item = _mapper.Map<T>(data);
        var result = await _repository.Create(item);
        await _uof.Commit();
        return result;
    }

    public async Task<T> Update(int id, M data)
    {
        var item = await _repository.GetById(id);

        if (item is null)
        {
            throw NotFoundError.Builder("No records found for this id!", null);
        }

        var result = _repository.Update(id, _mapper.Map<T>(data));
        await _uof.Commit();
        return result;
    }

    public async Task Delete(int id)
    {
        var item = await _repository.GetById(id);

        if (item is null)
        {
            throw NotFoundError.Builder("No records found for this id!", null);
        }

        _repository.Delete(item);
        await _uof.Commit();
    }
}