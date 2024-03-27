using System.Linq.Expressions;
using Api.Repositories.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Shared.Errors;

namespace Api.Usecases;

public class Usecases<T,M> : IUsecases<T,M>
{

    private readonly IMapper _mapper;
    private readonly IUnitOfWork<T> _uof;

    public Usecases(IMapper mapper, IUnitOfWork<T> uof)
    {
        _mapper = mapper;
        _uof = uof;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _uof.Repository.GetAll();
    }

    public async Task<T> GetById(int id)
    {
        var item = await _uof.Repository.GetById(id);

        if (item is null)
        {
            throw NotFoundError.Builder("No records found for this id!", null);
        }

        return item;
    }

    public async Task<T?> GetBy(Expression<Func<T, bool>> predicate)
    {
        var item = await _uof.Repository.GetBy(predicate);
        
        if (item is null)
        {
            throw NotFoundError.Builder("No records found!", null);
        }

        return item;
    }

    public async Task<T> Create(M data)
    {
        var item = _mapper.Map<T>(data);
        var result = await _uof.Repository.Create(item);
        await _uof.Commit();
        return result;
    }

    public async Task<T> Update(int id, M data)
    {
        var item = await _uof.Repository.GetById(id);

        if (item is null)
        {
            throw NotFoundError.Builder("No records found for this id!", null);
        }

        var result = _uof.Repository.Update(id, _mapper.Map<T>(data));
        await _uof.Commit();
        return result;
    }

    public async Task Delete(int id)
    {
        var item = await _uof.Repository.GetById(id);

        if (item is null)
        {
            throw NotFoundError.Builder("No records found for this id!", null);
        }

        _uof.Repository.Delete(item);
        await _uof.Commit();
    }
}