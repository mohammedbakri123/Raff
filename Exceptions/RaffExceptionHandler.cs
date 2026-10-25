using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

public sealed class RaffExceptionHandler(
    ILogger<RaffExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(
            exception,
            "An unhandled exception occurred.");

        var (status, title) = exception switch
        {
            KeyNotFoundException =>
                (404, "Resource not found."),

            _ =>
                (500, "An unexpected error occurred.")
        };

        context.Response.StatusCode = status;

        await context.Response.WriteAsJsonAsync(
            new ProblemDetails
            {
                Status = status,
                Title = title
            },
            cancellationToken: cancellationToken);

        return true;
    }
}