namespace Raff.Exceptions;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

public sealed class RaffExceptionHandler(
    IProblemDetailsService problemDetailsService,
    ILogger<RaffExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (context.Response.HasStarted)
        {
            logger.LogWarning("Response has already started, skipping exception handler.");
            return false;
        }

        if (exception is OperationCanceledException)
        {
            logger.LogInformation("Request was cancelled by the client.");
            return true;
        }

        var (statusCode, title) = exception switch
        {
            KeyNotFoundException => (StatusCodes.Status404NotFound, "Resource not found."),
            BadHttpRequestException => (StatusCodes.Status400BadRequest, "Invalid request."),
            ArgumentException => (StatusCodes.Status400BadRequest, "Invalid argument."),
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized."),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
        };

        if (statusCode >= StatusCodes.Status500InternalServerError)
        {
            logger.LogError(exception, "Unhandled server error occurred: {Message}", exception.Message);
        }
        else
        {
            logger.LogWarning("Handled client error ({StatusCode}): {Message}", statusCode, exception.Message);
        }

        context.Response.StatusCode = statusCode;

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = context,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = exception.Message,
                Instance = context.Request.Path
            }
        });
    }
}
