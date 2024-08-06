using MISA.CokCok.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CokCok.Core.Interfaces.IRepositories
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        bool CheckEmployeeCodeDuplicate(string id);
        string getEmployeeLastest(); 
    }
}
