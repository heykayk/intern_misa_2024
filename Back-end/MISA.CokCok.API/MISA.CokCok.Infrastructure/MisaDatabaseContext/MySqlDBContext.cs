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
        public IDbConnection Connection { get; }

        public MySqlDBContext(IConfiguration configuration)
        {
            Connection = new MySqlConnection(configuration.GetConnectionString("Database1"));
        }

        public IDbTransaction transaction => throw new NotImplementedException();

        // Xóa một bản ghi dựa trên ID
        // Author: Ngô Minh Hiếu
        public int Delete<T>(string id)
        {
            var className = typeof(T).Name; // Tên của bảng tương ứng với loại T
            var sql = $"DELETE FROM {className} WHERE {className}Id = @id"; // Câu lệnh SQL xóa
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var res = Connection.Execute(sql, parameters); // Thực thi câu lệnh SQL
            return res;
        }

        // Xóa nhiều bản ghi dựa trên danh sách ID
        // Author: Ngô Minh Hiếu
        public int DeleteAny<T>(Guid[] ids)
        {
            var className = typeof(T).Name; // Tên của bảng tương ứng với loại T
            var res = 0;
            var sql = $"DELETE FROM {className} WHERE {className}Id IN (@ids)"; // Câu lệnh SQL xóa nhiều bản ghi
            var parameters = new DynamicParameters();
            // Chuyển đổi danh sách ID thành chuỗi
            var idsArray = string.Join(", ", ids);
            parameters.Add("@ids", idsArray);
            res = Connection.Execute(sql, parameters); // Thực thi câu lệnh SQL
            return res;
        }

        // Lấy tất cả bản ghi từ bảng
        // Author: Ngô Minh Hiếu
        public IEnumerable<T> Get<T>()
        {
            var classname = typeof(T).Name; // Tên của bảng tương ứng với loại T
            var sql = $"SELECT * FROM {classname}"; // Câu lệnh SQL lấy tất cả bản ghi
            if (classname == "Employee") // Nếu bảng là "Employee", sắp xếp theo EmployeeCode giảm dần
            {
                sql = $"SELECT * FROM {classname} ORDER BY {classname}Code DESC";
            }
            var data = Connection.Query<T>(sql); // Thực thi câu lệnh SQL và lấy dữ liệu
            return data.ToList();
        }

        // Lấy bản ghi dựa trên ID
        // Author: Ngô Minh Hiếu
        public T Get<T>(string id)
        {
            var className = typeof(T).Name; // Tên của bảng tương ứng với loại T
            var sql = $"SELECT * FROM {className} WHERE {className}Id = @id"; // Câu lệnh SQL lấy bản ghi theo ID
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var data = Connection.QueryFirstOrDefault<T>(sql, parameters); // Thực thi câu lệnh SQL và lấy dữ liệu
            return data;
        }

        // Thêm một bản ghi mới vào bảng
        // Author: Ngô Minh Hiếu
        public int Insert<T>(T entity)
        {
            var className = typeof(T).Name; // Tên của bảng tương ứng với loại T
            var propListName = ""; // Danh sách tên thuộc tính
            var propListValue = ""; // Danh sách giá trị thuộc tính
            // Lấy tất cả các thuộc tính của đối tượng
            var props = entity.GetType().GetProperties();
            var parameters = new DynamicParameters();
            // Duyệt qua từng thuộc tính
            foreach (var prop in props)
            {
                var propname = prop.Name; // Tên thuộc tính (ví dụ: EmployeeId)
                var val = prop.GetValue(entity); // Giá trị thuộc tính

                // Xây dựng danh sách tên thuộc tính và giá trị
                propListName += $"{propname},";
                propListValue += $"@{propname},";
                parameters.Add($"@{propname}", val); // Thêm tham số vào DynamicParameters
            }
            // Loại bỏ dấu phẩy cuối cùng
            propListName = propListName.TrimEnd(',');
            propListValue = propListValue.TrimEnd(',');

            // Xây dựng câu lệnh SQL chèn dữ liệu
            var sqlInsert = $"INSERT INTO {className}({propListName}) VALUES ({propListValue});";
            // Thực thi câu lệnh SQL
            var res = Connection.Execute(sqlInsert, parameters);
            return res;
        }

        // Cập nhật bản ghi dựa trên đối tượng
        // Author: Ngô Minh Hiếu
        public int Update<T>(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
            }

            var className = typeof(T).Name; // Tên của bảng tương ứng với loại T
            var setClause = ""; // Phần SET của câu lệnh SQL
            var keyPropertyName = className + "Id"; // Tên thuộc tính khóa chính
            var keyPropertyValue = ""; // Giá trị thuộc tính khóa chính

            // Lấy tất cả các thuộc tính của đối tượng
            var props = entity.GetType().GetProperties();
            var parameters = new DynamicParameters();

            // Duyệt qua từng thuộc tính
            foreach (var prop in props)
            {
                var propName = prop.Name;
                var val = prop.GetValue(entity);

                if (propName == keyPropertyName)
                {
                    keyPropertyValue = val?.ToString() ?? ""; // Lưu giá trị khóa chính
                }

                // Xây dựng phần SET, bỏ qua thuộc tính khóa chính
                if (propName != keyPropertyName)
                {
                    setClause += $"{propName} = @{propName}, ";
                    parameters.Add($"@{propName}", val);
                }
            }

            // Kiểm tra xem keyPropertyValue có hợp lệ không
            if (string.IsNullOrWhiteSpace(keyPropertyValue))
            {
                throw new InvalidOperationException("Key property value cannot be null or empty.");
            }

            // Xóa dấu phẩy cuối cùng trong setClause nếu có
            setClause = setClause.TrimEnd(',', ' ');

            // Xây dựng câu lệnh SQL
            var sql = $"UPDATE {className} SET {setClause} WHERE {keyPropertyName} = @{keyPropertyName}";
            parameters.Add($"@{keyPropertyName}", keyPropertyValue);
            // Thực thi câu lệnh SQL
            var res = Connection.Execute(sql, parameters);
            return res;
        }

        // Kiểm tra xem mã nhân viên có bị trùng không
        // Author: Ngô Minh Hiếu
        public bool CheckEmployeeCodeDuplicate<T>(string id)
        {
            var className = typeof(T).Name; // Tên của bảng tương ứng với loại T
            var sql = $"SELECT {className}Code FROM {className} WHERE {className}Code = @id"; // Câu lệnh SQL kiểm tra mã trùng
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var res = Connection.QueryFirstOrDefault(sql, parameters); // Thực thi câu lệnh SQL và lấy dữ liệu
            return (res != null); // Trả về true nếu có bản ghi, false nếu không
        }
    }
}
