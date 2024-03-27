using AutoMapper;
using Domain;
using Shared.DTO;

namespace Api.Domains.BookCollection.Mapper;

public class BookCaseDTOMapperProfile : Profile
{
    public BookCaseDTOMapperProfile()
    {
        CreateMap<BookCaseDTO, BookCase>().ReverseMap();
    }
}