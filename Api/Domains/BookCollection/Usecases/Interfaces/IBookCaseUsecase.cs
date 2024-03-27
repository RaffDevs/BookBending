using Api.Usecases;
using Domain;
using Shared.DTO;

namespace Api.Domains.BookCollection.Usecases.Interfaces;

public interface IBookCaseUsecase : IUsecases<BookCase, BookCaseDTO>
{
    public Task<IEnumerable<BookCase>> GetAllBookCase(string ownerName);
}