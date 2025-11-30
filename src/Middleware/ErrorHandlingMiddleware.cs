using System.Net;
using System.Text.Json;

namespace UrlShortenerApi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                context.Response.ContentType = "application/json";

                var (statusCode, responseObj) = MapExpectionToProblem(ex);

                context.Response.StatusCode = (int)statusCode;

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                await context.Response.WriteAsync(JsonSerializer.Serialize(responseObj, options));
            }
        }

        private static  (HttpStatusCode statusCode, object response) MapExpectionToProblem(Exception ex)
        {
            if(ex is AppException appEx)
            {
                return  (appEx.StatusCode, new {error = appEx.Message});
            }

            return (HttpStatusCode.InternalServerError, new {error = "Internal sever error"});
        }
    }
}