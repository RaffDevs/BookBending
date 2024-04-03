using AutoMapper;
using Domain.Entities;
using Shared.DTO;

namespace Api.Domains.Books.Mapper;

public class BookDTOMapperProfile : Profile
{
    public BookDTOMapperProfile()
    {
        CreateMap<BookDTO, Book>().ReverseMap();
    }
}