using System.Net;

namespace UrlShortenerApi.Helpers
{
    public class AppException : Exception
    {
        public HttpStatusCode StatusCode {get;}

        public AppException(string message,HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}