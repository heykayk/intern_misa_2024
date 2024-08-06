using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CokCok.Core.Entities
{
    public class Department
    {
        public Guid DepartmentId {  get; set; }
        public string DepartmentName { get; set; }
    }
}
