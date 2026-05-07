using BuildingBlocks.CQRS;
using BuildingBlocks.DTOs;
using Logging.API.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Logging.API.Features.GetLogs;

public class GetLogsHandler(LoggingDbContext db) : IQueryHandler<GetLogsQuery, IEnumerable<LogEntryDto>>
{
    public async Task<IEnumerable<LogEntryDto>> Handle(GetLogsQuery request, CancellationToken cancellationToken)
    {
        var query = db.Logs.AsQueryable();

        if (!string.IsNullOrEmpty(request.ServiceName))
        {
            query = query.Where(l => l.ServiceName == request.ServiceName);
        }

        if (!string.IsNullOrEmpty(request.LogLevel))
        {
            query = query.Where(l => l.LogLevel == request.LogLevel);
        }

        if (request.From.HasValue)
        {
            query = query.Where(l => l.Timestamp >= request.From.Value);
        }

        if (request.To.HasValue)
        {
            query = query.Where(l => l.Timestamp <= request.To.Value);
        }

        var logs = await query
            .OrderByDescending(l => l.Timestamp)
            .ToListAsync(cancellationToken);

        return logs.Adapt<IEnumerable<LogEntryDto>>();
    }
}
