using MISA.CokCok.Core.Entities;
using MISA.CokCok.Core.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using System.Data;
using MISA.CokCok.Infrastructure.Interfaces;

namespace MISA.CokCok.Infrastructure.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IMisaDBContext dbcontext) : base(dbcontext)
        {
        }

        public bool CheckEmployeeCodeDuplicate(string EmployeeCode)
        {
            var sql = "SELECT EmployeeCode FROM Employee e WHERE e.EmployeeCode = @EmployeeCode";
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeCode", EmployeeCode);
            var res = _dbContext.Connection.QueryFirstOrDefault(sql, parameters); 
            return res != null;
        }
    }
}
