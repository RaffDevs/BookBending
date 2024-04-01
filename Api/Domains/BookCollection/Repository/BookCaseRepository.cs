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
}