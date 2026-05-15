using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using SimpleAuth.Api.Responses;
using System.Net;
using System.Text.Json;

namespace SimpleAuth.Api.Handler
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string message = "An unexpected error occurred";
            object? errors = null;

            if(exception is ValidationException valEx )
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                message = "Validation Failed";
                errors = valEx.Errors.GroupBy(e => e.PropertyName)
                    .ToDictionary(e => JsonNamingPolicy.CamelCase.ConvertName(e.Key), e => e.Select(ve => ve.ErrorMessage).ToArray());
            }
            else if(exception is ArgumentException argEx)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                message = "Invalid argument";
                errors = argEx.Message;
            }
            else if(exception is UnauthorizedAccessException authEx)
            {
                statusCode = (int)HttpStatusCode.Unauthorized;
                message = "Unauthorized access. Please log in to continue";
            }
            else
            {
                _logger.LogError(exception, "Unhandled exception occurred at {Path} | Method: {Method} | Message: {ErrorMessage}",
                    httpContext.Request.Path,
                    httpContext.Request.Method,
                    exception.Message);
            }

            var response = ApiResponse<object>.Failure(errors!, message);

            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            return true;
        }
    }
}
