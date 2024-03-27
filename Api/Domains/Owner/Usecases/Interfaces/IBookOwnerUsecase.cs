using Api.Usecases;
using Domain;
using Domain.Entities;
using Shared.DTO;

namespace Api.Domains.Owner.Usecases.Interfaces;

public interface IBookOwnerUsecase : IUsecases<BookOwner, BookOwnerDTO>
{
}