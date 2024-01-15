using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;

namespace Company.Function
{

public static HttpResponseMessage Run(HttpRequestMessage req, TraceWriter log)
{
    var response = new HttpResponseMessage(HttpStatusCode.OK);
    var stream = new FileStream(@"index.html", FileMode.Open);
    response.Content = new StreamContent(stream);
    response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
    return response;
}
}