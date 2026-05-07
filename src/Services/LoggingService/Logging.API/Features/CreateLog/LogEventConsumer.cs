using BuildingBlocks.DTOs;
using Logging.API.Data;
using Logging.API.Domain;
using Mapster;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Logging.API.Features.CreateLog;

public class LogEventConsumer : IConsumer<LogEntryDto>
{
    private readonly LoggingDbContext _context;
    private readonly ILogger<LogEventConsumer> _logger;

    public LogEventConsumer(LoggingDbContext context, ILogger<LogEventConsumer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<LogEntryDto> context)
    {
        var logDto = context.Message;
        
        Console.WriteLine($">>> [MESSAGE RECEIVED] {logDto.Message}");

        try
        {
            var logEntry = logDto.Adapt<LogEntry>();
            _context.Logs.Add(logEntry);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation(">>> [SUCCESS] Log saved to DB via RabbitMQ: {Msg}", logDto.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ">>> [ERROR] Failed to save log via RabbitMQ");
        }
    }
}
