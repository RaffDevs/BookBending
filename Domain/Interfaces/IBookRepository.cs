using Domain.Entities;

namespace Domain.Interfaces;

public interface IBookRepository : IRepository<Book>
{
    public Task<Book> SearchBookByIsbn(string isbn);
    public Task<Book> SearchBookByName(string name);
}