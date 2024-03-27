using Api.Domains.Owner.Usecases.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Domains.Owner.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminOnly")]
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
    }
}
