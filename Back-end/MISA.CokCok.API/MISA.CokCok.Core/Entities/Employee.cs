using MISA.CokCok.Core.CustomVadilate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CokCok.Core.Entities
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? PositionId { get; set; }

        [Required(ErrorMessage = "Mã nhân viên không được để trống!")]
        public string EmployeeCode { get; set; }

        [Required(ErrorMessage = "Họ tên nhân viên không được để trống!")]
        public string FullName     { get; set; }

        [DateGreatThanToday(ErrorMessage = "Ngày sinh không được lớn hơn ngày hiện tại!")]
        public DateTime? DateOfBirth {  get; set; }
        public int? Sex { get; set; }
        public string? IdentificationNumber { get; set; }

        [DateGreatThanToday(ErrorMessage = "Ngày cấp không được lớn hơn ngày hiện tại!")]
        public DateTime? IssueDate {  get; set; }
        public string? PlaceOfIssue {  get; set; }
        public string? Address { get; set; }
        public string? MobilePhone { get; set; }
        public string? LandlinePhone { get; set; }

        [EmailAddress(ErrorMessage ="Email không đúng định dạng!")]
        public string? Email { get; set; }
        public string? BankAccount { get; set; }
        public string? BankName { get; set; }
        public string? Brach {  get; set; }
    }
}
