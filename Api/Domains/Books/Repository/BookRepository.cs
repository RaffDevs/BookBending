using Api.Database.Context;
using Api.Repositories;
using Domain.Entities;
using Domain.Interfaces;

namespace Api.Domains.Books.Repository;

public class BookRepository : Repository<Book>, IBookRepository
{
    private readonly DatabaseContext _context;
    
    public BookRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public Task<Book> SearchBookByIsbn(string isbn)
    {
        throw new NotImplementedException();
    }

    public Task<Book> SearchBookByName(string name)
    {
        throw new NotImplementedException();
    }
}