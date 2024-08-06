using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CokCok.Core.Entities
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }

        [Required(ErrorMessage = "Không được để trống!")]
        public string EmployeeCode { get; set; }

        [Required(ErrorMessage = "Không được để trống!")]
        public string FullName     { get; set; }
        public DateTime? DateOfBirth {  get; set; }
        public int Sex { get; set; }
        public string IdentificationNumber { get; set; }
        public DateTime? IssueDate {  get; set; }
        public string PlaceOfIssue {  get; set; }
        public string Address { get; set; }
        public string MobilePhone { get; set; }
        public string LandlinePhone { get; set; }

        [EmailAddress(ErrorMessage ="Email không đúng định dạng!")]
        public string Email { get; set; }
        public string BankAccount { get; set; }
        public string BankName { get; set; }
        public string Brach {  get; set; }
    }
}
