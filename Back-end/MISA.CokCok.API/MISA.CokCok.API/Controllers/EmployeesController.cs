using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CokCok.Core.DTOs;
using MISA.CokCok.Core.Entities;
using MISA.CokCok.Core.Interfaces.IRepositories;
using MISA.CokCok.Core.Interfaces.IServices;
using MISA.CokCok.Infrastructure.Repository;
using System.Net;

namespace MISA.CokCok.API.Controllers
{
    // API: https://localhost:7178/api/v1/Employees
    // Author: Ngô Minh Hiếu
    [Route("api/v1/Employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeRepository _employeeRepository;
        private IEmployeeService _employeeService;

        public EmployeesController(IEmployeeRepository repository, IEmployeeService service)
        {
            _employeeRepository = repository;
            _employeeService = service;
        }

        // API: GET https://localhost:7178/api/v1/Employees
        // Author: Ngô Minh Hiếu
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(200, _employeeRepository.Get());
        }

        // API: GET https://localhost:7178/api/v1/Employees/NewEmployee/EmployeeCode
        // Author: Ngô Minh Hiếu
        [HttpGet("NewEmployee/EmployeeCode")]
        public IActionResult GetEmployeeCode()
        {
            var res = _employeeRepository.getEmployeeLastest();
            return StatusCode(200, res);
        }

        // API: GET https://localhost:7178/api/v1/Employees/{id}
        // Author: Ngô Minh Hiếu
        [HttpGet("{id}")]
        public IActionResult Get(String id)
        {
            var employee = _employeeRepository.Get(id);
            return StatusCode(200, employee);
        }

        // API: GET https://localhost:7178/api/v1/Employees/ExportFile
        // Author: Ngô Minh Hiếu
        [HttpGet("ExportFile")]
        public IActionResult GetCSV()
        {
            var res = 1; // Placeholder for actual CSV export logic
            return StatusCode(200, res);
        }

        // API: POST https://localhost:7178/api/v1/Employees/UpdateEmployee
        // Author: Ngô Minh Hiếu
        [HttpPost("UpdateEmployee")]
        public IActionResult Update(Employee employee)
        {
            try
            {
                var result = _employeeService.UpdateService(employee);
                if (result.Success == true)
                {
                    return StatusCode(200, result);
                }
                else
                {
                    return StatusCode(400, result);
                }
            }
            catch (Exception ex)
            {
                var res = new
                {
                    userMgs = "Có lỗi xảy ra",
                    devMsg = "",
                    error = ex.Message
                };

                return StatusCode(500, res);
            }
        }

        // API: POST https://localhost:7178/api/v1/Employees
        // Author: Ngô Minh Hiếu
        [HttpPost]
        public IActionResult Insert(Employee employee)
        {
            try
            {
                var result = _employeeService.InsertService(employee);
                if (result.Success == true)
                {
                    return StatusCode(200, result);
                }
                else
                {
                    return StatusCode(400, result);
                }
            }
            catch (Exception ex)
            {
                var res = new
                {
                    userMgs = "Có lỗi xảy ra",
                    devMsg = "",
                    error = ex.Message
                };

                return StatusCode(500, res);
            }
        }

        // API: DELETE https://localhost:7178/api/v1/Employees/{id}
        // Author: Ngô Minh Hiếu
        [HttpDelete("{id}")]
        public IActionResult Delete(String id)
        {
            try
            {
                var res = _employeeRepository.Delete(id);
                var message = new ServiceResponse
                {
                    StatusCode = 200,
                    Message = "Xóa thành công"
                };

                return StatusCode(200, message);
            }
            catch (Exception ex)
            {
                var res = new
                {
                    userMgs = "Có lỗi xảy ra",
                    devMsg = "",
                    error = ex.Message
                };
                return StatusCode(500, ex.Message);
            }
        }
    }
}
