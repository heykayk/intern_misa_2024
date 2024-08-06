using MISA.CokCok.Core.Entities;
using MISA.CokCok.Core.Interfaces.IRepositories;
using MISA.CokCok.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using MISA.CokCok.Core.DTOs;
using static Dapper.SqlMapper;

namespace MISA.CokCok.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository repository)
        {        
            _employeeRepository = repository;
        }

        public ServiceResponse InsertService(Employee entity)
        {
            var isDuplicate = _employeeRepository.CheckEmployeeCodeDuplicate(entity.EmployeeCode);
            if (isDuplicate)
            {
                var serviceResponse = new ServiceResponse
                {
                    Success = false,
                    StatusCode = (int)System.Net.HttpStatusCode.BadRequest,
                    Message = null
                };
                serviceResponse.Errors.Add("Mã nhân viên đã tồn tại!");
                return serviceResponse;
            }
            SetNewId(entity);
            var res = _employeeRepository.Insert(entity);
            return new ServiceResponse
            {
                Success = true,
                StatusCode = (int) System.Net.HttpStatusCode.OK,
                Message = res
            };
        }

        private void SetNewId(Employee entity) 
        {
            entity.EmployeeId = Guid.NewGuid();
        }

        public ServiceResponse updateService(Employee entity)
        {
            var res = _employeeRepository.Update(entity);
            return new ServiceResponse
            {
                Success = true,
                StatusCode = (int)System.Net.HttpStatusCode.OK,
                Message = res
            };
        }
    }
}
