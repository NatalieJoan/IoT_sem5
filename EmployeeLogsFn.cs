using System.Net;
using System.Text.Json;
using Employees.Service;
using Database.Entities.EmployeeLogs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Google.Protobuf.WellKnownTypes;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;

namespace Company.Function
{
    public class EmployeeLogsFn
    {
        
        private readonly ILogger _logger;
        private readonly EmployeesService employeeLogsService = new EmployeesService();
        
        public EmployeeLogsFn(ILoggerFactory loggerFactory, EmployeesService employeeLogsService)
        {
            _logger = loggerFactory.CreateLogger<EmployeeLogsFn>();
            this.employeeLogsService = employeeLogsService;
        }

        [Function("EmployeeLogsFn")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "post", "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var response = req.CreateResponse(HttpStatusCode.OK);
            switch (req.Method)

            {
                case "POST":
                    StreamReader reader = new StreamReader(req.Body, System.Text.Encoding.UTF8);
                    var json = reader.ReadToEnd();
                    var timelog = JsonSerializer.Deserialize<TimeLog>(json);
                    var res = employeeLogsService.LogTime(timelog.EmployeeId);
                    response.WriteAsJsonAsync(res);
                    break;
                case "GET":
                    var logs = employeeLogsService.GetTimeLog();
                    response.WriteAsJsonAsync(logs);
                    break;
            }
            return response;
        }
    }
}
