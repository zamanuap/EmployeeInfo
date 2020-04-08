using System;
using System.Collections.Generic;
using System.Text;

namespace Poco
{
    public class ProvincePoco
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<EmployeePoco> EmployeePocos { get; set; }
    }
}
