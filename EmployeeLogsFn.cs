using System.Net;
using System.Text.Json;
using EmployeeLogs.Service;
using Database.Entities.EmployeeLogs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Google.Protobuf.WellKnownTypes;

namespace Company.Function
{
    public class EmployeeLogsFn
    {
        private readonly ILogger _logger;
        private readonly EmployeeLogsService employeeLogsService = new EmployeeLogsService(null);
        
        public EmployeeLogsFn(ILoggerFactory loggerFactory, EmployeeLogsService employeeLogsService)
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
                    var logs = employeeLogsService.Get();
                    response.WriteAsJsonAsync(logs);
                    break;
            }
            return response;
        }
    }
}
