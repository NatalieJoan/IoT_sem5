namespace Employees.Service
{
    public class EmployeesService
    {
        private List<Employee> employees { get; } = new List<Employee>();

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

       public void Delete(int id)
       {
            var employee = employees.First(w => w.Id == id);
            employees.Remove(employee);
       }

       public Employee Find(int id)
       {
            return employees.First(w => w.Id == id);
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
}