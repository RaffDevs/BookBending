using Api.Domains.BookCollection.Usecases.Interfaces;
using Api.Repositories.Interfaces;
using Api.Usecases;
using AutoMapper;
using Domain;
using Domain.Interfaces;
using Shared.DTO;

namespace Api.Domains.BookCollection.Usecases;

public class BookCaseUsecase : Usecases<BookCase, BookCaseDTO>, IBookCaseUsecase
{
    private readonly IUnitOfWork<BookCase> _uof;
    private readonly IBookCaseRepository _repository;

    public BookCaseUsecase(IMapper mapper, IUnitOfWork<BookCase> uof) : base(mapper, uof)
    {
        _uof = uof;
        _repository = (IBookCaseRepository)uof.Repository;
    }

    public async Task<IEnumerable<BookCase>> GetAllBookCase(string ownerName)
    {
        var bookcases = await _repository.GetBookCasesByOwner(ownerName);
        return bookcases;
    }
}