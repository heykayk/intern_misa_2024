using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CokCok.Core.Interfaces.IRepositories;

namespace MISA.CokCok.API.Controllers
{
    // API: https://localhost:7178/api/v1/Positions
    // Author: Ngô Minh Hiếu
    [Route("api/v1/Positions")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private IPositionRepository _positionRepository;

        public PositionsController(IPositionRepository repository)
        {
            _positionRepository = repository;
        }

        // API: GET https://localhost:7178/api/v1/Positions
        // Author: Ngô Minh Hiếu
        [HttpGet]
        public IActionResult Get()
        {
            // Lấy danh sách các vị trí từ repository và trả về với mã trạng thái 200 OK
            return StatusCode(200, _positionRepository.Get());
        }
    }
}
