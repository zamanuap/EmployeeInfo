using Employee.ViewModels.CustomValidation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Poco.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.ViewModels
{
    public class EmployeeEditViewModel
    {
        public Guid Id { get; set; }
        //[RegularExpression(@"^[\P{M}\p{M}]{2,}$", ErrorMessage = "Must be at least 2 characters")]
        [NameCustomValidation("Name must be at leat 2 characters")]
        public string Name { get; set; }
        [Display(Name ="Province")]
        [Required]
        public int SelectedProvinceId { get; set; }
        public IEnumerable<SelectListItem> ProvinceNames { get; set; }
        [RegularExpression(@"^((\([1-9]{3}\)\s?)|([1-9]{3}\-))[1-9]{3}\-[1-9]{4}$", ErrorMessage = "Wrong format")]
        public string Telephone { get; set; }
        [RegularExpression(@"^[A-Z]{1}[1-9]{1}[A-Z]{1}\s?[1-9]{1}[A-Z]{1}[1-9]{1}$", ErrorMessage = "Wrong format")]
        public string PostalCode { get; set; }
        [SalaryCustomValidation("SelectedProvinceId", "Invalid format")]
        public string Salary { get; set; }
        [NotMapped]
        public Byte[] TimeStamp { get; set; }
    }
}
