using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CokCok.Core.Interfaces.IRepositories;

namespace MISA.CokCok.API.Controllers
{
    [Route("api/v1/Departments")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        IDepartmentRepository _DepartmentRepository;

        public DepartmentsController(IDepartmentRepository repository)
        {
            _DepartmentRepository = repository; 
        }

        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(200, _DepartmentRepository.Get());
        }
    }
}
