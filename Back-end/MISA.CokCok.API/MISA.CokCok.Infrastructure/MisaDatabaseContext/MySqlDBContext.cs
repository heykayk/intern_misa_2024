using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.CokCok.Infrastructure.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CokCok.Infrastructure.MisaDatabaseContext
{
    public class MySqlDBContext : IMisaDBContext
    {
        public IDbConnection Connection {  get;}

        public MySqlDBContext(IConfiguration configuration)
        {
            Connection = new MySqlConnection(configuration.GetConnectionString("Database1"));
        }

        public IDbTransaction transaction => throw new NotImplementedException();

        public int Delete<T>(string id)
        {   
            var className = typeof(T).Name;
            var sql = $"DELETE FROM {className} WHERE {className}Id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var res = Connection.Execute(sql, parameters);
            return res;
        }

        public int DeleteAny<T>(Guid[] ids)
        {
            var className = typeof(T).Name;
            var res = 0;
            var sql = $"DELETE FROM {className} WHERE {className}Id IN (@ids)";
            var parameters = new DynamicParameters();
            var idsArray = "";
            foreach (var id in ids)
            {
                idsArray += $"{id}, ";
            }
            idsArray.Substring(0, idsArray.Length - 1);
            parameters.Add("@ids", idsArray);
            res = Connection.Execute(sql, parameters);
            return res;
        }

        public IEnumerable<T> Get<T>()
        {
            var classname = typeof(T).Name;
            var sql = $"SELECT * FROM {classname}";
            var data = Connection.Query<T>(sql);
            return data.ToList();
        }

        public T Get<T>(string id)
        {
            var className = typeof(T).Name; 
            var sql = $"SELECT * FROM {className} WHERE {className}Id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var data = Connection.QueryFirstOrDefault<T>(sql, parameters);
            return data;
        }

        public int Insert<T>(T entity)
        {
            var className = typeof(T).Name;
            var propListName = "";
            var propListValue = "";
            // lấy ra tất cả các props của entity
            var props = entity.GetType().GetProperties();
            var parameters = new DynamicParameters();
            // duyệt từng props
            foreach (var prop in props)
            {
                // lấy ra tên của prop
                var propname = prop.Name; // EmployeeId
                var val = prop.GetValue(entity);

                // lấy ra values của prop
                propListName += $"{propname},";
                propListValue += $"@{propname},";
                parameters.Add($"@{propname}", val);
            }
            propListName =  propListName.Substring(0, propListName.Length - 1);
            propListValue = propListValue.Substring(0, propListValue.Length - 1);

            // Build câu lệnh sql
            var sqlInsert = $"INSERT {className}({propListName}) VALUES ({propListValue});";
            // thực thi
            var res = Connection.Execute(sqlInsert, parameters);
            return res;
        }

        public int Update<T>(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
