using Api.Database.Context;
using Api.Repositories;
using Domain.Entities;
using Domain.Interfaces;

namespace Api.Domains.Owner.Repository;

public class BookOwnerRepository : Repository<BookOwner>, IBookOwnerRepository
{
    public BookOwnerRepository(DatabaseContext context) : base(context)
    {
    }
}