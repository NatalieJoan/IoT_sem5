using Microsoft.AspNetCore.Mvc;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using Microsoft.Extensions.Logging;
using Services.Employee;
using Database.Entities.Employee;

namespace Controllers.Employees
{
    [ApiController]
    [Route("employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> logger;
        private readonly IEmployeesService employeesService;
        public EmployeesController(ILogger<EmployeesController> logger, IEmployeesService employeesService)
        {
            this.logger = logger;
            this.employeesService = employeesService;
        }

        [HttpGet]
        public IEnumerable<EmployeeEntity> GetEmployees()
        {
            return employeesService.GetEmployees();
        }

        [HttpGet("{id}")]
        public EmployeeEntity FindEmployee([FromRoute] int id)
        {
            return employeesService.FindEmployee(id);
        }

        [HttpPost]
        public EmployeeEntity AddEmployee([FromBody] EmployeeEntity employee)
        {
            return employeesService.AddEmployee(employee);
        }
    }
}