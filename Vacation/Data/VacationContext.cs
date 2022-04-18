using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Vacation.Data;

namespace Vacation
{
    class VacationContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<VacationApply> Vacations { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data source = DESKTOP-31R4UH0;Initial Catalog=EmployeeVacation;Integrated Security = True;");
        }
    }
}
