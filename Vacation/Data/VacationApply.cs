using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Vacation.Data
{
    public class VacationApply
    {
        [Key]
        public int VactionID { get; set; }
        
        public string Vacationtype { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public DateTime Date { get; set; }

        public int EmployeeID { get; set; }
        public Employee Employee;
    }
}
