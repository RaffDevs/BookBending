using Api.Database.Context;
using Api.Repositories;
using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Domains.BookCollection.Repository;

public class BookCaseRepository : Repository<BookCase>, IBookCaseRepository
{
    private readonly DatabaseContext _context;
    
    public BookCaseRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BookCase>> GetBookCasesByOwner(string ownerName)
    {
        var bookCases = await _context.BookCases
            .Where(bc => bc.Owner.UserName == ownerName)
            .Include(bc => bc.Books)
            .ToListAsync();

        return bookCases;
    }
}