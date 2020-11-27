using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularCRUDOperation.Models
{
    public class EmployeeModel
    {
        public int empid { get; set; }
        public string empname { get; set; }
        
        public string empaddress { get; set; }
        public string empcity { get; set; }
    }
}