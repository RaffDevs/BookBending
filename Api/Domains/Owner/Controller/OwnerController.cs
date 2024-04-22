using Api.Domains.Owner.Usecases.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetAllOwners()
        {
            var result = await _usecase.GetAll();
            return Ok(result);
        }

        [HttpGet("{ownerName:alpha}")]
        [Authorize(Policy = "UserOnly")]
        public async Task<IActionResult> GetOwner(string ownerName)
        {
            var result = await _usecase.GetByName(ownerName);
            return Ok(result);
        }
    }
}
