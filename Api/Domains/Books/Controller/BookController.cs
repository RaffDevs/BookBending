using Api.Domains.Books.Usecases.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Shared.Errors;

namespace Api.Domains.Books.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBookUsecase _bookUsecase;

        public BookController(IMapper mapper, IBookUsecase bookUsecase)
        {
            _mapper = mapper;
            _bookUsecase = bookUsecase;
        }

        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookUsecase.GetAll();
            return Ok(_mapper.Map<IEnumerable<BookDTO>>(books));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _bookUsecase.GetById(id);
            return Ok(_mapper.Map<BookDTO>(book));
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Create(BookDTO data)
        {
            var book = await _bookUsecase.Create(data);
            return Ok(_mapper.Map<BookDTO>(book));
        }
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, BookDTO data)
        {
            if (id != data.Id)
            {
                throw NotFoundError.Builder("Mismatch id content", null);
            }
            var book = await _bookUsecase.Update(id, data);
            return Ok(_mapper.Map<BookDTO>(book));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookUsecase.Delete(id);
            return NoContent();
        }
    }
}
