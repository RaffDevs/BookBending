using Api.Domains.BookCollection.Usecases.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;

namespace Api.Domains.BookCollection.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookCaseController : ControllerBase
    {
        private readonly IBookCaseUsecase _usecase;
        private readonly IMapper _mapper;

        public BookCaseController(IBookCaseUsecase usecase, IMapper mapper)
        {
            _usecase = usecase;
            _mapper = mapper;
        }
        
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookCaseById(int id)
        {
            var bookcase = await _usecase.GetById(id);
            return Ok(bookcase);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookCases(string ownerName)
        {
            var bookCases = await _usecase.GetAllBookCase(ownerName);
            return Ok(bookCases);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookCase(BookCaseDTO data)
        {
            var bookCase = await _usecase.Create(data);
            return Ok(_mapper.Map<BookCaseDTO>(bookCase));
        }

        [HttpPut("id:int")]
        public async Task<IActionResult> UpdateBookCase(int id, BookCaseDTO data)
        {
            //TODO(Update if bookcase belongs to current user)
            var bookCase = await _usecase.Update(id, data);
            return Ok(_mapper.Map<BookCaseDTO>(data));
        }

        [HttpDelete("id:int")]
        public async Task<IActionResult> Delete(int id)
        {
            //TODO(Delete if bookcase belongs to current user)
            await _usecase.Delete(id);
            return NoContent();
        }
        
    }
}
