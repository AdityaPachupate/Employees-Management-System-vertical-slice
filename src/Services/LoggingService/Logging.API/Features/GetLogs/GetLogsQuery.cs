using BuildingBlocks.CQRS;
using BuildingBlocks.DTOs;

namespace Logging.API.Features.GetLogs;

public record GetLogsQuery(string? ServiceName, string? LogLevel, DateTime? From, DateTime? To) : IQuery<IEnumerable<LogEntryDto>>;
