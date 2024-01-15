using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http; 

namespace Company.Function{

public static async Task<IActionResult> Run(HttpRequestMessage req, ILogger log)
        {
            try
            {
                var filePath = Path.Combine(Environment.CurrentDirectory, "index.html");

                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    var response = new FileStreamResult(stream, "text/html");
                    return response;
                }
            }
            catch (Exception ex)
            {
                log.LogError($"Error: {ex.Message}");
                return new StatusCodeResult(500); // Internal Server Error
            }
        }
}