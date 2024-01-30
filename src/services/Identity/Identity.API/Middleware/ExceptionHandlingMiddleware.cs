using Identity.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace Identity.API.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public ExceptionHandlingMiddleware()
        {

        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch(Exception e)
            {
                //_logger.LogError(e, e.Message);

                await HandleExceptionAsync(context, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status = GetStatusCode(exception);
            string message = exception.Message;
            string? stackTrace = exception.StackTrace;

            var exceptionResult = JsonSerializer.Serialize(new { error = message, stackTrace });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            if(status == HttpStatusCode.InternalServerError)
            {
                //_logger.LogError(exception, message);
            }

            return context.Response.WriteAsync(exceptionResult);
        }

        private static HttpStatusCode GetStatusCode(Exception exception) => exception switch
        {
            BadRequestException => HttpStatusCode.BadRequest,
            NotFoundException => HttpStatusCode.NotFound,
            NotImplementedException => HttpStatusCode.NotImplemented,
            InvalidOperationException => HttpStatusCode.InternalServerError,
            UnauthorizedAccessException => HttpStatusCode.Unauthorized,
            _ => HttpStatusCode.InternalServerError,
        };
    }
}
