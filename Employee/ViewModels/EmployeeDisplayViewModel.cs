using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.ViewModels
{
    public class EmployeeDisplayViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Province { get; set; }
        public string Telephone { get; set; }
        public string PostalCode { get; set; }
        public string Salary { get; set; }
    }
}
