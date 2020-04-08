using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.ViewModels
{
    public class PagedModel
    {
        public List<EmployeeDisplayViewModel> employeeDisplays { get; set; }
        public int totalPage { get; set; }
    }
}
