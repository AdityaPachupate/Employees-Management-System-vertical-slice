using BuildingBlocks.DTOs;
using Logging.API.Data;
using Logging.API.Domain;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Logging.API.Features.CreateLog;

public static class CreateLogEndpoint
{
    public static void MapCreateLogEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/logs", async (LogEntryDto logDto, LoggingDbContext dbContext, CancellationToken cancellationToken) =>
        {
            var logEntry = logDto.Adapt<LogEntry>();
            dbContext.Logs.Add(logEntry);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Results.Ok();
        })
        .WithName("CreateLog")
        .WithTags("Logs")
        .WithSummary("Create Log")
        .WithDescription("Creates a new log entry via HTTP.");
    }
}
