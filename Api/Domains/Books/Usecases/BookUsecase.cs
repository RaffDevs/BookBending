using Api.Domains.Books.Usecases.Interfaces;
using Api.Repositories.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Shared.DTO;
using Shared.Errors;

namespace Api.Domains.Books.Usecases;

public class BookUsecase : IBookUsecase
{

    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;
    private readonly IBookRepository _repository;

    public BookUsecase(IUnitOfWork uof, IMapper mapper, IBookRepository repository)
    {
        _uof = uof;
        _mapper = mapper;
        _repository = _uof.BookRepository;
    }

    public async Task<IEnumerable<Book>> GetAll()
    {
        var books = await _repository.GetAll();
        return books;
    }

    public async Task<Book> GetById(int id)
    {
        var book = await _repository.GetById(id);

        if (book is null)
        {
            throw NotFoundError.Builder("No books founded!", null);
        }

        return book;
    }

    public async Task<Book> Create(BookDTO data)
    {
        var book = await _repository.Create(_mapper.Map<Book>(data));
        await _uof.Commit();
        return book;
    }

    public async Task<Book> Update(int id, BookDTO data)
    {
        var updateBook = _repository.Update(_mapper.Map<Book>(data));
        await _uof.Commit();

        return updateBook;
    }

    public async Task Delete(int id)
    {
        var book = await _repository.GetById(id);

        if (book is null)
        {
            throw NotFoundError.Builder("No books founded!", null);
        }

        _repository.Delete(book);
        await _uof.Commit();
    }
}