using Api.Domains.Owner.Usecases.Interfaces;
using Api.Repositories.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Shared.DTO;
using Shared.Errors;

namespace Api.Domains.Owner.Usecases;

public class BookOwnerUsecase : IBookOwnerUsecase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uof;
    private readonly IRepository<BookOwner> _repository;
    public BookOwnerUsecase(IMapper mapper, IUnitOfWork uof)
    {
        _mapper = mapper;
        _uof = uof;
        _repository = uof.BookOwnerRepository;
    }

    public async Task<IEnumerable<BookOwner>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<BookOwner> GetById(int id)
    {
        var bookOwner = await _repository.GetById(id);

        if (bookOwner is null)
        {
            throw NotFoundError.Builder("Owner not found!", null);
        }

        return bookOwner;
    }

    public async Task<BookOwner> GetByName(string name)
    {
        var bookOwner = await _repository.GetBy(bc => bc.UserName == name);
        
        if (bookOwner is null)
        {
            throw NotFoundError.Builder("Owner not found!", null);
        }

        return bookOwner;

    }

    public async Task<BookOwner> Create(BookOwnerDTO data)
    {
        var owner = await _repository.Create(_mapper.Map<BookOwner>(data));
        await _uof.Commit();

        return owner;
    }

    public async Task<BookOwner> Update(int id, BookOwnerDTO data)
    {
        var owner = await _repository.GetById(id);

        if (owner is null)
        {
            throw NotFoundError.Builder("Owner not found!", null);
        }

        var result = _repository.Update(_mapper.Map<BookOwner>(data));
        await _uof.Commit();

        return result;
    }

    public async Task Delete(int id)
    {
        var owner = await _repository.GetById(id);

        if (owner is null)
        {
            throw NotFoundError.Builder("Owner not found!", null);
        }

        _repository.Delete(owner);
        await _uof.Commit();

    }
}