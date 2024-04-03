using Api.Database.Context;
using Api.Repositories;
using Domain.Entities;
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

    public async Task<BookCase?> GetBooksInBookcase(int id)
    {
        var bookCase = await _context.BookCases
            .Include(bc => bc.Books)
            .FirstOrDefaultAsync(bc => bc.Id == id);

        return bookCase;
    }
}