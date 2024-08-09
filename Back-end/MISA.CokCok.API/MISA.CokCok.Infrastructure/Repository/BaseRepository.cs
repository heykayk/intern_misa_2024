using MISA.CokCok.Core;
using MISA.CokCok.Core.Interfaces.IRepositories;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MISA.CokCok.Infrastructure.Interfaces;

namespace MISA.CokCok.Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected IMisaDBContext _dbContext;
        public BaseRepository(IMisaDBContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public bool CheckEmployeeCodeDuplicate(string id)
        {
            var res = _dbContext.CheckEmployeeCodeDuplicate<T>(id);
            return res;
        }

        public int Delete(string id)
        {
           var res = _dbContext.Delete<T>(id);
            return res;
        }

        public int DeleteAny(Guid[] ids)
        {
            var res = _dbContext.DeleteAny<T>(ids); 
            return res;
        }

        public List<T> Get()
        {
            var res = _dbContext.Get<T>();
            return res.ToList();
        }

        public T Get(string id)
        {
            var res = _dbContext.Get<T>(id);
            return res;
        }

        public int Insert(T entity)
        {
            var res = (_dbContext.Insert<T>(entity));
            return res;
        }

        public int Update(T entity)
        {
            var res = _dbContext.Update<T>(entity); 
            return res;
        }
    }
}
