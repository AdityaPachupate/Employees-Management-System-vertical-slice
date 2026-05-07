using System.ComponentModel.DataAnnotations;

namespace Logging.API.Domain;

public class LogEntry
{
    [Key]
    public int LogId { get; set; }
    public DateTime Timestamp { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public string? CorrelationId { get; set; }
    public string LogLevel { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string? Exception { get; set; }
    public string? UserName { get; set; }
}
