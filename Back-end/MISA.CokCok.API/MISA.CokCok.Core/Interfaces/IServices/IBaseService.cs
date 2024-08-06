using MISA.CokCok.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CokCok.Core.Interfaces.IServices
{
    public interface IBaseService<T> where T : class
    {
        ServiceResponse InsertService(T entity);
    }
}
