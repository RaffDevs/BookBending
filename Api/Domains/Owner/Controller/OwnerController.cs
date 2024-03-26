using Api.Domains.Owner.Usecases.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;

namespace Api.Domains.Owner.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IBookOwnerUsecase _usecase;

        public OwnerController(IBookOwnerUsecase usecase)
        {
            _usecase = usecase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _usecase.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookOwnerDTO data)
        {
            var result = await _usecase.Create(data);
            return Ok(result);
        }
    }
}
