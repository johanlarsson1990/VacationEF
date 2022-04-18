using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Vacation.Data
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        public string FullName { get; set; }
       
    }
}
