﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CokCok.Core.CustomVadilate
{
    public class DateGreatThanToday : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value == null)
            {
                return null; 
            }
            var dateValue = (DateTime)value;
            DateTime date;
            if (DateTime.TryParse(value.ToString(), out date))
            {
                // So sánh với ngày hiện tại
                var todayDate = DateTime.Now;
                if (todayDate < date)
                {
                    return new ValidationResult(ErrorMessage);
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                return new ValidationResult("Ngày tháng không hợp lệ!");
            }
        }

    }
}
