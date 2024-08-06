using MISA.CokCok.Core.DTOs;
using MISA.CokCok.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CokCok.Core.Interfaces.IServices
{
    public interface IEmployeeService : IBaseService<Employee>
    {
        Object updateService(Employee employee);
    }
}
