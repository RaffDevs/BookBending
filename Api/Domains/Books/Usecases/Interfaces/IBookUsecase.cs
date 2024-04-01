using Domain.Entities;
using Shared.DTO;

namespace Api.Domains.Books.Usecases.Interfaces;

public interface IBookUsecase
{
    public Task<IEnumerable<Book>> GetAll();
    public Task<Book> GetById(int id);
    public Task<Book> Create(BookDTO data);
    public Task<Book> Update(int id, BookDTO data);
    public Task Delete(int id);
}