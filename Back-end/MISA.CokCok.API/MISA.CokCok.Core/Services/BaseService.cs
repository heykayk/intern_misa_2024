using MISA.CokCok.Core.DTOs;
using MISA.CokCok.Core.Interfaces.IRepositories;
using MISA.CokCok.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CokCok.Core.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        IBaseRepository<T> repository;

        public BaseService(IBaseRepository<T> repository) 
        {
            this.repository = repository; 
        }
        public ServiceResponse InsertService(T entity)
        {
            var res = repository.Insert(entity);
            return new ServiceResponse
            {
                Success = true,
                Message = res
            };
        }
    }
}
