using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Poco
{
    public class EmployeePoco
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int ProvinceId { get; set; }
        public string Telephone { get; set; }
        public string PostalCode { get; set; }
        public string Salary { get; set; }
        [Timestamp]
        public Byte[] TimeStamp { get; set; }
        public virtual ProvincePoco ProvincePoco { get; set; }

    }
}
