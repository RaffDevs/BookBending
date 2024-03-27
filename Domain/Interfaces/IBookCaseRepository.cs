namespace Domain.Interfaces;

public interface IBookCaseRepository : IRepository<BookCase>
{
    public Task<IEnumerable<BookCase>> GetBookCasesByOwner(string ownerName);
}