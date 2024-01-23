namespace Employees.Service
{
    public class EmployeesService
    {
        public List<Employee> employees { get; } = new List<Employee>();
        private List<TimeLog> EmployeeLogs { get; } = new List<TimeLog>();
        public Employee AddEmployee(string firstName, string lastName)
        {
            var employee = new Employee
            {
                _firstName = firstName,
                _lastName = lastName,
                Id = employees.Count + 1,
                _timeWorking = 0
            };
            employees.Add(employee);
            return employee;
        }

       public Employee UpdateEmployee(int id, string firstName, string lastName, int timeworking)
       {
            var employee = employees.First(w => w.Id == id);
            employee._firstName = firstName;
            employee._lastName = lastName;
            employee._timeWorking = timeworking;
            return employee;
       }
        public IEnumerable<Employee> GetEmployee()
        {
            return employees;
        }
        //time logs

        public TimeLog LogTime(int employeeId)
        {
            var currentTime = DateTime.Now;
            var ifExists = EmployeeLogs
                .Where(t => t.EmployeeId == employeeId)
                .Select(t => t.EmployeeId)
                .FirstOrDefault();

            if (ifExists == 0)
            {
                var timelog = AddToTimestamps(employeeId, currentTime);
                return timelog;
            }
            else
            {
                UpdateEmployeeTotalHours(employeeId, (int)(currentTime - GetTime(ifExists)).TotalSeconds);
                RemoveFromTimestamps(employeeId);
                return null;
            }
        }
        public DateTime GetTime(int employeeId)
        {
            var timestamp = EmployeeLogs
                .Where(t => t.EmployeeId == employeeId)
                .Select(t => t.Timestamp)
                .FirstOrDefault();
            return timestamp;
        }
        public IEnumerable<TimeLog> GetTimeLog()
        {
            return EmployeeLogs;
        }
        public TimeLog AddToTimestamps(int employeeId, DateTime timestamp)
        {
            var curtimestamp = new TimeLog
            {
                EmployeeId = employeeId,
                Timestamp = timestamp,
                EmployeeLogsId = EmployeeLogs.Count + 1
            };
            EmployeeLogs.Add(curtimestamp);
            return curtimestamp;
        }

        public void UpdateEmployeeTotalHours(int employeeId, int duration)
        {
            var employeefound = employees.FirstOrDefault(t => t.Id == employeeId);
            if (employeefound != null){
                UpdateEmployee(employeefound.Id,employeefound._firstName, employeefound._lastName,employeefound._timeWorking + duration);
            }
            
        }

        public void RemoveFromTimestamps(int employeeId)
        {
            EmployeeLogs.RemoveAll(t => t.EmployeeId == employeeId);
        }


    }
    // timelogs

    
        
    public class Employee
    {
        public int Id { get; set; }
        public string _firstName { get; set; }
        public string _lastName { get; set; }

        public int _timeWorking {get; set;}
    }
      public class TimeLog
    {
        public int EmployeeId { get; set; } = 0;
        public int EmployeeLogsId { get; set; } = 0;
        public DateTime Timestamp { get; set; } = DateTime.MinValue; 
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