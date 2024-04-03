using Api.Database.Context;
using Api.Domains.BookCollection.Repository;
using Api.Domains.Books.Repository;
using Api.Domains.Owner.Repository;
using Api.Repositories.Interfaces;
using Domain.Interfaces;

namespace Api.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IBookOwnerRepository _bookOwnerRepository { get; }

    private IBookCaseRepository _bookCaseRepository { get; }

    private IBookRepository _bookRepository { get; }
    private DatabaseContext _context { get; }

    public UnitOfWork(IBookOwnerRepository bookOwnerRepository, IBookCaseRepository bookCaseRepository,
        IBookRepository bookRepository,
        DatabaseContext context)
    {
        _bookOwnerRepository = bookOwnerRepository;
        _bookCaseRepository = bookCaseRepository;
        _bookRepository = bookRepository;
        _context = context;
    }

    public IBookOwnerRepository BookOwnerRepository => _bookOwnerRepository ?? new BookOwnerRepository(_context);
    public IBookCaseRepository BookCaseRepository => _bookCaseRepository ?? new BookCaseRepository(_context);
    public IBookRepository BookRepository => _bookRepository ?? new BookRepository(_context);

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }
}