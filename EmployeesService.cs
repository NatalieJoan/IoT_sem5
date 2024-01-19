using System;
using System.Collections.Generic;
using System.Linq;

namespace Employees.Service
{
    public class EmployeesService
    {
        private List<Employee> employees { get; } = new List<Employee>();
        private List<TimeLog> timeLogs { get; } = new List<TimeLog>();

        public Employee Add(string firstName, string lastName)
        {
            var employee = new Employee
            {
                _firstName = firstName,
                _lastName = lastName,
                Id = employees.Count + 1
            };
            employees.Add(employee);
            return employee;
        }

       public Employee Update(int id, string firstName, string lastName)
       {
            var employee = employees.First(w => w.Id == id);
            employee._firstName = firstName;
            employee._lastName = lastName;

            return employee;
       }
       
       public Employee GetTime(int employeeId)
       {
            var employee = employees.FirstOrDefault(w => w.Id == employeeId);
            if (employee != null)
            {
                var logs = timeLogs.Where(t => t.EmployeeId == employeeId).ToList();
                foreach (var log in logs)
                {
                    Console.WriteLine($"Employee {employee._firstName} {employee._lastName}: LogInTime={log.LogInTime}, LogOutTime={log.LogOutTime ?? DateTime.Now}");
                }
            }
            else
            {
                Console.WriteLine($"Employee with ID {employeeId} not found.");
            }
            return employee;
        }
         public void LogIn(int employeeId)
        {
            var employee = employees.FirstOrDefault(e => e.Id == employeeId);
            if (employee != null)
            {
                var log = new TimeLog
                {
                    EmployeeId = employeeId,
                    LogInTime = DateTime.Now
                };
                timeLogs.Add(log);
                Console.WriteLine($"Employee {employee._firstName} {employee._lastName} logged in at {log.LogInTime}");
            }
            else
            {
                Console.WriteLine($"Employee with ID {employeeId} not found.");
            }
        }
         public void LogOut(int employeeId)
        {
            var log = timeLogs.FirstOrDefault(t => t.EmployeeId == employeeId && t.LogOutTime == null);
            if (log != null)
            {
                log.LogOutTime = DateTime.Now;
                Console.WriteLine($"Employee with ID {employeeId} logged out at {log.LogOutTime}");
            }
            else
            {
                Console.WriteLine($"No active login found for employee with ID {employeeId}.");
            }
        }

        public IEnumerable<Employee> Get()
        {
            return employees;
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string _firstName { get; set; }
        public string _lastName { get; set; }
    }
            public class TimeLog
        {
            public int EmployeeId { get; set; }
            public DateTime LogInTime { get; set; }
            public DateTime? LogOutTime { get; set; }
        }
}

    //    public void Delete(int id)
    //    {
    //         var employee = employees.First(w => w.Id == id);
    //         employees.Remove(employee);
    //    }

    //    public Employee Find(int id)
    //    {
    //         return employees.First(w => w.Id == id);
    //    }