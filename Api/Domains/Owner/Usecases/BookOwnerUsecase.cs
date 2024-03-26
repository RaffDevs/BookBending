using Api.Domains.Owner.Usecases.Interfaces;
using Api.Repositories.Interfaces;
using Api.Usecases;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Shared.DTO;

namespace Api.Domains.Owner.Usecases;

public class BookOwnerUsecase : Usecases<BookOwner, BookOwnerDTO>, IBookOwnerUsecase
{
    public BookOwnerUsecase(IRepository<BookOwner> repository, IMapper mapper, IUnitOfWork<BookOwner> uof) : base(repository,
        mapper, uof)
    {
    }
}