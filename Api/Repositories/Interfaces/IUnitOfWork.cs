using Domain.Interfaces;

namespace Api.Repositories.Interfaces;

public interface IUnitOfWork
{
     IBookOwnerRepository BookOwnerRepository { get; }
     IBookCaseRepository BookCaseRepository { get; }
     
     // IBookRepository BookRepository { get;  }
     
     public Task Commit();
}