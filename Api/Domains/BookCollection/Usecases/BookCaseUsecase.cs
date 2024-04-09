using Api.Domains.BookCollection.Usecases.Interfaces;
using Api.Domains.Owner.Usecases.Interfaces;
using Api.Repositories.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Shared.DTO;
using Shared.Errors;

namespace Api.Domains.BookCollection.Usecases;

public class BookCaseUsecase : IBookCaseUsecase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper; 
    private readonly IBookCaseRepository _repository;
    private readonly IBookOwnerUsecase _bookOwnerUsecase;

    public BookCaseUsecase(IMapper mapper, IUnitOfWork uof, IBookOwnerUsecase bookOwnerUsecase)
    {
        _uof = uof;
        _mapper = mapper;
        _repository = uof.BookCaseRepository;
        _bookOwnerUsecase = bookOwnerUsecase;
    }
    
    public async Task<IEnumerable<BookCase>> GetAll(string ownerName)
    {
        var bookcases = await _repository.GetAllBy(bc => bc.Owner.UserName == ownerName);
        return bookcases;
    }

    public async Task<BookCase> GetById(int id)
    {
        var bookCase = await _repository.GetBooksInBookcase(id);

        if (bookCase is null)
        {
            throw NotFoundError.Builder("No book cases has found!", null);
        }

        return bookCase;
    }

    public async Task<BookCase> Create(BookCaseDTO data, string ownerName)
    {
        await _bookOwnerUsecase.GetByName(ownerName);
        var bookCase = await _repository.Create(_mapper.Map<BookCase>(data));
        await _uof.Commit();
        return bookCase;
    }

    public async Task<BookCase> Update(BookCaseDTO data, int id, string ownerName)
    {
        await _bookOwnerUsecase.GetByName(ownerName);
        var bookCase = await _repository.GetById(id);

        if (bookCase is null)
        {
            throw NotFoundError.Builder("No book cases has found!", null);
        }

        var result = _repository.Update(_mapper.Map<BookCase>(data));
        await _uof.Commit();
        return result;
    }

    public async Task Delete(int id, string ownerName)
    {
        await _bookOwnerUsecase.GetByName(ownerName);

        var bookCase = await _repository.GetById(id);
        
        if (bookCase is null)
        {
            throw NotFoundError.Builder("No book cases has found!", null);
        }

        _repository.Delete(bookCase);
        await _uof.Commit();
    }
}