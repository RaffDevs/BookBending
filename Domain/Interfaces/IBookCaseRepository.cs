using Domain.Entities;

namespace Domain.Interfaces;

public interface IBookCaseRepository : IRepository<BookCase>
{
    public Task<BookCase?> GetBooksInBookcase(int id);

}