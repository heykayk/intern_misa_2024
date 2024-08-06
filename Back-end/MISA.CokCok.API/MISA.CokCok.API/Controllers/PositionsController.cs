using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CokCok.Core.Interfaces.IRepositories;

namespace MISA.CokCok.API.Controllers
{
    [Route("api/v1/Positions")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private IPositionRepository _positionRepository;

        public PositionsController(IPositionRepository repository)
        {
            _positionRepository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(200, _positionRepository.Get());
        }
    }
}
