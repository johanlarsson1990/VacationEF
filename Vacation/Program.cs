using System;
using Vacation.Data;
using System.Linq;

namespace Vacation
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new VacationContext())
            {
                //FirstEntity(db);
                string selectedInput = StartUp();

                switch (selectedInput)
                {
                    case "1":
                        Vacation(db);
                        break;
                    case "2":
                        SearchEmployee(db);
                        break;
                    case "3":
                        Administration(db);
                        break;
                    default:
                        Console.WriteLine("Wrong Input");
                        selectedInput = StartUp();
                        break;
                }

                Console.ReadKey();
            }
        }

        private static void Administration(VacationContext db)
        {
            Console.WriteLine("Write desired month 'MM'");
            var monthInput = Console.ReadLine();
            //Hämtar månader
            var monthQuery = from m in db.Vacations
                             where m.StartDate.Contains(monthInput)
                             orderby m.Date
                             select m;
            //Hämtar ledigheter
            var results = from table1 in db.Vacations
                          join table2 in db.Employees
                          on table1.EmployeeID equals table2.EmployeeID into joined

                          from table3 in joined.DefaultIfEmpty()
                          where table1.EmployeeID.Equals(table3.EmployeeID) && table1.StartDate.Contains(monthInput)
                          select table3;
            Console.Clear();
            foreach (var item in results)
            {
                Console.WriteLine(item.FullName);
            }
            DateTime thisDate1 = new DateTime(0001, Int32.Parse(monthInput), 01);
            Console.WriteLine(thisDate1.ToString("MMM"));
            foreach (var item in monthQuery)
            {
                Console.WriteLine(item.Date + " " + item.StartDate + " " + item.EndDate + " " + item.Vacationtype);
            }
        }

        private static void SearchEmployee(VacationContext db)
        {
            Console.WriteLine("Write desired employees name");
            var empolyeeInput = Console.ReadLine();
            var searchQuery = from s in db.Employees
                              where s.FullName.Contains(empolyeeInput)
                              orderby s.FullName
                              select s;
            var vacationQuery = from v in db.Vacations
                                where v.Employee.FullName.Contains(empolyeeInput)
                                orderby v.Date
                                select v;


            foreach (var item in searchQuery)
            {
                Console.WriteLine(item.FullName);
            }
            foreach (var vacay in vacationQuery)
            {
                Console.WriteLine(vacay.Date + " " + vacay.StartDate + " - " + vacay.EndDate + " - " + vacay.Vacationtype);
            }
        }

        private static string StartUp()
        {
            Console.Clear();
            Console.WriteLine("Select an option: \n" +
                              "1. To fill out vacation application. \n" +
                              "2. Search for an employee's vecation \n" +
                              "3. for Administration search");
            var selectedInput = Console.ReadLine().ToString();
            return selectedInput;
        }

        private static void Vacation(VacationContext db)
        {
            Console.WriteLine("Fill out your full name");
            var nameInput = Console.ReadLine().ToString();
            Console.WriteLine("Your name is: " + nameInput + ". Write your desired vacation start date dd-mm-yyyy");
            var startInput = Console.ReadLine().ToString();
            Console.WriteLine("Your start date you wish too have is: " + startInput);
            Console.WriteLine("What will be your end date dd-mm-yyyy?");
            var endInput = Console.ReadLine().ToString();
            Console.WriteLine("Your desired end date is: " + endInput);
            Console.WriteLine("What kind of vacation are you applying for?");
            var typeInput = Console.ReadLine().ToString();
            Console.WriteLine("Name: " + nameInput + " Start: " + startInput + " End: " + endInput + " Vacation type: " + typeInput);
            Console.WriteLine("Is this correct?");

            DateTime localdate = DateTime.Now;
            Console.WriteLine(localdate);
            Console.WriteLine("Press Space to confirm, any other key to restart");
            ReadSpecificKey(ConsoleKey.Spacebar, nameInput, typeInput, startInput, endInput,localdate,db);
        }

        private static void ReadSpecificKey(ConsoleKey keyin, string name, string type, string start, string end, DateTime date, VacationContext db)
        {
            ConsoleKey key;
            Console.WriteLine($"The console app is waiting for you to press the {keyin} button. Press escape to move on at any time.");
            do
            {
                while (!Console.KeyAvailable)
                {
                    //
                }
                // Key is available - read it
                key = Console.ReadKey(true).Key;

                if (key == keyin)
                {
                    Console.WriteLine("Your application is now confirmed");
                    AddVacay(name, type, start, end, date, db);
                }
                else
                {
                    Console.WriteLine("You pressed the wrong key!");
                    StartUp();
                }
            } while (key != ConsoleKey.Escape);


            //call this method like this
            ReadSpecificKey(ConsoleKey.Spacebar, name,type,start,end,date,db);
        }
        private static void FirstEntity(VacationContext db)
        {
            var person1 = new Employee { FullName = "Johan Larsson" };
            db.Employees.Add(person1);

            var person2 = new Employee { FullName = "Ida Johansson" };
            db.Employees.Add(person2);

            var person3 = new Employee { FullName = "Lasse Larsson" };
            db.Employees.Add(person3);

            var person4 = new Employee { FullName = "Lotta Eriksson" };
            db.Employees.Add(person4);
            var vacation1 = new VacationApply { EmployeeID = 2, StartDate = "2021-06-10", EndDate = "2021-07-30", Vacationtype = "Semester" };
            var vacation2 = new VacationApply { EmployeeID = 3, StartDate = "2021-07-01", EndDate = "2021-07-05", Vacationtype = "VAB" };
            var vacation3 = new VacationApply { EmployeeID = 4, StartDate = "2021-07-03", EndDate = "2021-08-15", Vacationtype = "Semester" };
            var vacation4 = new VacationApply { EmployeeID = 5, StartDate = "2021-06-03", EndDate = "2021-09-01", Vacationtype = "Semester + Förälder dagar" };
            db.Vacations.Add(vacation1);
            db.Vacations.Add(vacation2);
            db.Vacations.Add(vacation3);
            db.Vacations.Add(vacation4);
            db.SaveChanges();
        }
        private static void AddVacay(string name, string type, string start, string end, DateTime date, VacationContext db)
        {
            var emp = new Employee { FullName = name };
            db.Employees.Add(emp);
            db.SaveChanges();
            //var idQuery = from i in db.Employees
            //              where i.FullName.Contains(name)
            //              orderby i.FullName
            //              select i;


            var vac = new VacationApply { EmployeeID = emp.EmployeeID, StartDate = start, EndDate = end, Vacationtype = type, Date = date };

            db.Vacations.Add(vac);
            db.SaveChanges();
        }
    }
}
