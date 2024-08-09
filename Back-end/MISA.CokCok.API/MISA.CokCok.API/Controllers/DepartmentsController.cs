using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CokCok.Core.Interfaces.IRepositories;

namespace MISA.CokCok.API.Controllers
{
    // API: https://localhost:7178/api/v1/Departments
    // Author: Ngô Minh Hiếu
    [Route("api/v1/Departments")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private IDepartmentRepository _DepartmentRepository;

        public DepartmentsController(IDepartmentRepository repository)
        {
            _DepartmentRepository = repository;
        }

        // API: GET https://localhost:7178/api/v1/Departments
        // Author: Ngô Minh Hiếu
        [HttpGet]
        public IActionResult Get()
        {
            // Lấy danh sách các phòng ban từ repository và trả về với mã trạng thái 200 OK
            return StatusCode(200, _DepartmentRepository.Get());
        }
    }
}
