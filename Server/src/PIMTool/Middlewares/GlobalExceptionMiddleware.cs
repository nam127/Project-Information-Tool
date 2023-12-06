using System.ComponentModel;
using System.Net;
using System.Text.Json;
using PIMTool.Core.Exceptions;

namespace PIMTool.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, "Unexpected exception");
                // context.Response.ContentType = "application/json";
                // context.Response.StatusCode = 500;

                // await context.Response.WriteAsync(new
                // {
                //     Code = 500,
                //     Message = ex.Message
                // }.ToString() ?? string.Empty);
                await HandleExceptionAsync(context, ex);
            }   
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode status;
            var stackTrace = string.Empty;
            string message = "";

            var exceptionType = ex.GetType();

            switch (exceptionType)
            {
                case Type _ when exceptionType == typeof(DupplicateProjectNumberException):
                    message = ex.Message;
                    status = HttpStatusCode.Conflict;
                    stackTrace = ex.StackTrace;
                    break;
                case Type _ when exceptionType == typeof(ProjectNotFoundException):
                    message = ex.Message;
                    status = HttpStatusCode.NotFound;
                    stackTrace = ex.StackTrace;
                    break;
                case Type _ when exceptionType == typeof(BusinessException):
                    message = ex.Message;
                    status = HttpStatusCode.BadRequest;
                    stackTrace = ex.StackTrace;
                    break;
                case Type _ when exceptionType == typeof(ConcurrencyUpdateException):
                    message = ex.Message;
                    status = HttpStatusCode.Conflict;
                    stackTrace = ex.StackTrace;
                    break;
                default:
                    message = ex.Message;
                    status = HttpStatusCode.InternalServerError;
                    stackTrace = ex.StackTrace;
                    break;
            }

            var exceptionResult = JsonSerializer.Serialize(new { error = message, stackTrace });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            return context.Response.WriteAsync(exceptionResult);

        }
    }
}