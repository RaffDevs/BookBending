using AutoMapper;
using Domain.Entities;
using Shared.DTO;

namespace Api.Domains.Owner.Mapper;

public class BookOwnerDTOMapperProfile : Profile
{
    public BookOwnerDTOMapperProfile()
    {
        CreateMap<BookOwnerDTO, BookOwner>().ReverseMap();
    }
}