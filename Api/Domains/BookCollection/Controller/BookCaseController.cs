using Api.Domains.BookCollection.Usecases.Interfaces;
using Api.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> GetBookCaseById(int id, string ownerName)
        {
            var usernameClaim = this.ExtractUsernameClaim();
            this.ValidateUsermameClaim(usernameClaim, ownerName);
            var bookcase = await _usecase.GetById(id);
            return Ok(bookcase);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookCases(string ownerName)
        {
            var usernameClaim = this.ExtractUsernameClaim();
            this.ValidateUsermameClaim(usernameClaim, ownerName);
            var bookCases = await _usecase.GetAll(ownerName);
            return Ok(bookCases);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookCase(BookCaseDTO data)
        {
            var usernameClaim = this.ExtractUsernameClaim();
            this.ValidateUsermameClaim(usernameClaim, data.BookOwner.UserName);
            
            var bookCase = await _usecase.Create(data, usernameClaim);
            return Ok(_mapper.Map<BookCaseDTO>(bookCase));
        }

        [HttpPut("id:int")]
        public async Task<IActionResult> UpdateBookCase(int id, BookCaseDTO data)
        {
            var usernameClaim = this.ExtractUsernameClaim();
            this.ValidateUsermameClaim(usernameClaim, data.BookOwner.UserName);
            var bookCase = await _usecase.Update(data, id, usernameClaim);
            return Ok(_mapper.Map<BookCaseDTO>(bookCase));
        }

        [HttpDelete("id:int")]
        public async Task<IActionResult> Delete(int id)
        {
            var usernameClaim = this.ExtractUsernameClaim();
            await _usecase.Delete(id, usernameClaim);
            return NoContent();
        }
        
    }
}
