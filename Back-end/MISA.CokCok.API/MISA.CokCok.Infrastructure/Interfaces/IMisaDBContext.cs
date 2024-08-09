using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CokCok.Infrastructure.Interfaces
{
    public interface IMisaDBContext
    {
        IDbConnection Connection { get; }
        IDbTransaction transaction { get; }
        IEnumerable<T> Get<T>();
        T? Get<T>(String id);
        int Insert<T>(T entity);
        int Update<T>(T entity);
        int Delete<T>(string id);
        int DeleteAny<T>(Guid[] ids);
        bool CheckEmployeeCodeDuplicate<T>(string id);
    }
}
