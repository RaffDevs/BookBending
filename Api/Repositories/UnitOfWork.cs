using Api.Database.Context;
using Api.Domains.Owner.Repository;
using Api.Repositories.Interfaces;
using Domain.Interfaces;

namespace Api.Repositories;

public class UnitOfWork<T> : IUnitOfWork<T> where T: class
{
    private readonly DatabaseContext _context;

    public UnitOfWork(DatabaseContext context, IRepository<T> repository)
    {
        _context = context;
        Repository = repository;
    }

    public IRepository<T> Repository { get; }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }
}