using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WarshipsApp.Exceptions
{
    public class ExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = exception switch
            {
                InvalidOperationException => CreateProblemDetails(exception, httpContext, HttpStatusCode.BadRequest),
                ArgumentException or ArgumentOutOfRangeException => CreateProblemDetails(exception, httpContext, HttpStatusCode.BadRequest),
                _ => CreateProblemDetails(exception, httpContext, HttpStatusCode.InternalServerError)
            };

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);
            return true;
        }

        private static ProblemDetails CreateProblemDetails(Exception exception, HttpContext httpContext, HttpStatusCode statusCode)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred",
                Detail = exception.Message,
            };

            httpContext.Response.StatusCode = (int)statusCode;

            return problemDetails;
        }
    }
}
