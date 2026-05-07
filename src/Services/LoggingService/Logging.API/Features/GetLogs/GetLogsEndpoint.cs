using BuildingBlocks.DTOs;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Logging.API.Features.GetLogs;

public static class GetLogsEndpoint
{
    public static void MapGetLogsEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/logs", async (
            [FromQuery] string? serviceName,
            [FromQuery] string? level,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to,
            ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetLogsQuery(serviceName, level, from, to);
            var result = await sender.Send(query, cancellationToken);
            return Results.Ok(result);
        })
        .WithName("GetLogs")
        .WithTags("Logs")
        .Produces<IEnumerable<LogEntryDto>>(StatusCodes.Status200OK)
        .WithSummary("Get Logs")
        .WithDescription("Retrieves logs with optional filtering.");
    }
}
