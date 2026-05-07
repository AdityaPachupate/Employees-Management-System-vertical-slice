using Logging.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace Logging.API.Data;

public class LoggingDbContext : DbContext
{
    public LoggingDbContext(DbContextOptions<LoggingDbContext> options) : base(options)
    {
    }

    public DbSet<LogEntry> Logs => Set<LogEntry>();
}
