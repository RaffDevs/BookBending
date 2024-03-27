using Api.Database.Context;
using Api.Repositories.Interfaces;
using Domain;
using Domain.Entities;
using Domain.Interfaces;
using Shared.Errors;

namespace Api.Repositories;

public class UnitOfWork<T> : IUnitOfWork<T> where T : class
{
    private IBookOwnerRepository _bookOwnerRepository { get; }

    private IBookCaseRepository _bookCaseRepository { get; }

    // private IBookRepository _bookRepository { get; }
    private DatabaseContext _context { get; }

    public UnitOfWork(IBookOwnerRepository bookOwnerRepository, IBookCaseRepository bookCaseRepository,
        DatabaseContext context)
    {
        _bookOwnerRepository = bookOwnerRepository;
        _bookCaseRepository = bookCaseRepository;
        // _bookRepository = bookRepository;
        _context = context;
    }

    public IRepository<T> Repository
    {
        get
        {
            Type typeT = typeof(T);

            if (typeT == typeof(BookOwner))
            {
                return (IRepository<T>)_bookOwnerRepository;
            }

            if (typeT == typeof(BookCase))
            {
                return (IRepository<T>)_bookCaseRepository;
            }

            if (typeT == typeof(Book))
            {
                throw new NotImplementedException();
            }

            throw InternalServerError.Builder(null, new Exception("Repository not implemented!"));
        }
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }
}