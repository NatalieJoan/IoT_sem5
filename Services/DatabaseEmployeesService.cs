using Services.Employee;
using Database.Entities.Employee;
using Database.Employee;

namespace Database.Services
{
    public class DatabaseEmployeesService : IEmployeesService
    {
        private readonly EmployeesDb db;

        public DatabaseEmployeesService(EmployeesDb db)
        {
            this.db = db;
        }
        public EmployeeEntity AddEmployee(EmployeeEntity employee)
        {
            var entity = new EmployeeEntity
            {
                _firstName = employee._firstName,
                _lastName = employee._lastName,
            };
            db.Employees.Add(entity);
            db.SaveChanges();
            employee.Id = entity.Id;
            return employee;
        }

        public EmployeeEntity FindEmployee(int id)
        {
            var employee = db.Employees.First(w => w.Id == id);

            return employee;
        }

        public IEnumerable<EmployeeEntity> GetEmployees()
        {
            var peopleList = db.Employees.Select(s => MapToDTO(s));

            return peopleList;
        }
        public EmployeeEntity MapToDTO(EmployeeEntity entity)
        {
            return new EmployeeEntity
            {
                _firstName = entity._firstName,
                Id = entity.Id,
                _lastName = entity._lastName
            };
        }
    }
}