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

        public byte[] ExportFile()
        {
            var csvBuilder = new StringBuilder();
            var props = typeof(T).GetProperties();
            var titileColumn = "";
            foreach (var prop in props)
            {
                titileColumn += $"{prop.Name},";
            }
            titileColumn = titileColumn.Substring(0, titileColumn.Length - 1);
            csvBuilder.AppendLine(titileColumn);

            var datas = repository.Get();
            foreach (var data in datas)
            {
                var rowData = "";
                foreach (var prop in props)
                {
                    rowData += $"{prop.GetValue(data)},";
                }
                rowData = rowData.Trim().Substring(0, rowData.Length - 1);
                csvBuilder.AppendLine(rowData);
            }
            
            return Encoding.UTF8.GetBytes(csvBuilder.ToString());
        }

        public ServiceResponse InsertService(T entity)
        {
            var clasName = typeof(T).Name;
            var props = entity.GetType().GetProperties();
            var index = 0;
            var classCode = "";
            foreach (var prop in props)
            {
                if (prop.Name == $"{clasName}Code")
                {
                    classCode = prop.GetValue(entity).ToString();
                    index++;
                }
                if(prop.Name == $"{clasName}Id")
                {
                    index++;
                    prop.SetValue(entity, Guid.NewGuid());
                }
                if (index > 1) break;
            }

            var isDuplicate = repository.CheckEmployeeCodeDuplicate(classCode.ToString());

            if (isDuplicate)
            {
                var message = new ServiceResponse
                {
                    Success = false,
                    StatusCode = 400,
                };
                message.Errors.Add($"{clasName}Code đã bị trùng!");
            }

            var res = repository.Insert(entity);
            return new ServiceResponse
            {
                Success = true,
                StatusCode = 200,
                Message = res
            };
        }

        public ServiceResponse UpdateService(T entity)
        {
            var message = new ServiceResponse();
            var className = typeof(T).Name;
            if (entity == null)
            {
                message.Success = false;
                message.StatusCode = 400;
                message.Errors.Add($"{className} is null!");
                return message;
            }
            var res = repository.Update(entity);
            return new ServiceResponse
            {
                Success = true,
                StatusCode = 200,
                Message = res
            };
        }
    }

}
