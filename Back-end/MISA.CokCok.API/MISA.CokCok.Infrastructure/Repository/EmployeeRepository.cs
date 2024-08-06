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

        public string getEmployeeLastest()
        {
            var sql = "SELECT EmployeeCode FROM Employee ORDER BY EmployeeCode DESC LIMIT 1";
            var res = _dbContext.Connection.QueryFirstOrDefault<string>(sql);
            return res != null ? NewEmployeeCode(res) : "NV-000001";
        }

        private string NewEmployeeCode(string originalString)
        {

            // Tách phần số từ chuỗi
            string prefix = originalString.Substring(0, 3); // "NV-"
            string numberPart = originalString.Substring(3); // "000001"

            // Chuyển phần số sang số nguyên
            int number = int.Parse(numberPart);

            // Tăng giá trị số lên 1
            number++;

            // Định dạng lại thành chuỗi với số mới
            string newNumberPart = number.ToString("D6"); // Đảm bảo có 6 chữ số với các số 0 dẫn đầu
            string newString = prefix + newNumberPart;

            return newString;
        }
    }
}
