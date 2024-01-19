using System.Net;
using System.Text.Json;
using EmployeeLogs.Service;
using Database.Entities.EmployeeLogs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class EmployeeLogsFn
    {
        private readonly ILogger _logger;
        private readonly EmployeeLogsService employeeLogsService;

        public EmployeeLogsFn(ILoggerFactory loggerFactory, EmployeeLogsService employeeLogsService)
        {
            _logger = loggerFactory.CreateLogger<EmployeeLogsFn>();
            this.employeeLogsService = employeeLogsService;
        }

        [Function("EmployeeLogsFn")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);

            StreamReader reader = new StreamReader(req.Body, System.Text.Encoding.UTF8);
            var json = reader.ReadToEnd();
            var employeeId = JsonSerializer.Deserialize<TimeLog>(json);
            var getTime = employeeLogsService.GetTime(employeeId.EmployeeId);
            response.WriteAsJsonAsync(getTime);
            return response;
        }
    }
}
