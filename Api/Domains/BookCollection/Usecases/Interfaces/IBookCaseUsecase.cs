using Domain;
using Domain.Entities;
using Shared.DTO;

namespace Api.Domains.BookCollection.Usecases.Interfaces;

public interface IBookCaseUsecase
{
    public Task<IEnumerable<BookCase>> GetAll(string ownerName);
    public Task<BookCase> GetById(int id);
    public Task<BookCase> Create(BookCaseDTO data, string ownerName);
    public Task<BookCase> Update(BookCaseDTO data, int id, string ownerName);
    public Task Delete(int id, string ownerName);
    
}