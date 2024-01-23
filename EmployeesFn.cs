using System.Net;
using System.Text.Json;
using Employees.Service;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class EmployeesFn
    {
        private readonly ILogger _logger;
        private readonly EmployeesService employeesService;
        public EmployeesFn(ILoggerFactory loggerFactory, EmployeesService employeesService)
        {
            _logger = loggerFactory.CreateLogger<EmployeesFn>();
            this.employeesService = employeesService;
        }

        [Function("EmployeesFn")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);

            switch (req.Method)
            {
                case "POST":
                    StreamReader reader = new StreamReader(req.Body, System.Text.Encoding.UTF8);
                    var json = reader.ReadToEnd();
                    var employee = JsonSerializer.Deserialize<Employee>(json);
                    var res = employeesService.Add(employee._firstName, employee._lastName);
                    response.WriteAsJsonAsync(res);
                    break;
                case "PUT":
                    StreamReader putReader = new StreamReader(req.Body, System.Text.Encoding.UTF8);
                    var putJson = putReader.ReadToEnd();
                    var putEmployee = JsonSerializer.Deserialize<Employee>(putJson);
                    var putRes = employeesService.Update(putEmployee.Id, putEmployee._firstName, putEmployee._lastName, putEmployee._timeWorking);
                    response.WriteAsJsonAsync(putRes);
                    break;
                case "GET":
                    var employees = employeesService.Get();
                    response.WriteAsJsonAsync(employees);
                    break;

                // case "DELETE":
                //     StreamReader deleteReader = new StreamReader(req.Body, System.Text.Encoding.UTF8);
                //     var deleteJson = deleteReader.ReadToEnd();
                //     var hireEmployee = JsonSerializer.Deserialize<Employee>(deleteJson);
                //     employeesService.Delete(hireEmployee.Id);
                //     break;
            }

            return response;
        }
    }
}
