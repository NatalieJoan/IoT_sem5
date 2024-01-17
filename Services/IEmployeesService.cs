using Database.Entities.Employee;

namespace Services.Employee
{
    public interface IEmployeesService
    {
        EmployeeEntity FindEmployee(int id);

        IEnumerable<EmployeeEntity> GetEmployees();

        EmployeeEntity AddEmployee(EmployeeEntity employee);
    }
}