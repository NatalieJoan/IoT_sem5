namespace Employees.Service
{
    public class EmployeesService
    {
        public List<Employee> employees { get; } = new List<Employee>();

        public Employee Add(string firstName, string lastName)
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

       public Employee Update(int id, string firstName, string lastName, int timeworking)
       {
            var employee = employees.First(w => w.Id == id);
            employee._firstName = firstName;
            employee._lastName = lastName;
            employee._timeWorking = timeworking;
            return employee;
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

        public int _timeWorking {get; set;}
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