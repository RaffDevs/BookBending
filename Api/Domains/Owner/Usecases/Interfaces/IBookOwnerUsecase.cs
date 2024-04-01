using Domain;
using Domain.Entities;
using Shared.DTO;

namespace Api.Domains.Owner.Usecases.Interfaces;

public interface IBookOwnerUsecase
{
    public Task<IEnumerable<BookOwner>> GetAll();
    public Task<BookOwner> GetById(int id);
    public Task<BookOwner> GetByName(string name);
    public Task<BookOwner> Create(BookOwnerDTO data);
    public Task<BookOwner> Update(int id, BookOwnerDTO data);
    public Task Delete(int id);
}