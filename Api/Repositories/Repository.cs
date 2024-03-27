using System.Linq.Expressions;
using Api.Database.Context;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class Repository<T> : IRepository<T> where T: class
{
    private readonly DatabaseContext _context;

    public Repository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetById(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T?> GetBy(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public async Task<T> Create(T data)
    {
        var result = await _context.Set<T>().AddAsync(data);
        return result.Entity;
    }

    public T Update(int id, T data)
    {
        var result = _context.Set<T>().Update(data);
        return result.Entity;
    }

    public void Delete(T data)
    {
        _context.Set<T>().Remove(data);
    }
}