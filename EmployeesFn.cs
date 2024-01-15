using System.Net;
using Employees.Service;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class EmployeesFn
    {
        private readonly ILogger _logger;

        public EmployeesFn(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<EmployeesFn>();
        }

        [Function("EmployeesFn")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            switch (req.Method)
            {
                case "POST":
                    break;
                case "PUT":
                    break;
                case "GET":
                    break;
                case "DELETE":
                    break;
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }
}
