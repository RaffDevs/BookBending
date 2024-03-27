using Api.Domains.Owner.Usecases.Interfaces;
using Api.Repositories.Interfaces;
using Api.Usecases;
using AutoMapper;
using Domain.Entities;
using Shared.DTO;

namespace Api.Domains.Owner.Usecases;

public class BookOwnerUsecase : Usecases<BookOwner, BookOwnerDTO>, IBookOwnerUsecase
{
    public BookOwnerUsecase(IMapper mapper, IUnitOfWork<BookOwner> uof) : base(mapper, uof)
    {
    }
}