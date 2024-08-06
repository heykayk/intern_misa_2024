using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CokCok.Core.DTOs
{
    public class ServiceResponse
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public Object Message { get; set; }
        public List<String> Errors { get; set; } = new List<string>();

    }
}
