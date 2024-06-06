using LogsAnalyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogsAnalyzer.DataEntityFramework
{
    public class AppDbContext : DbContext
    {
        DbSet<LogRecord> Logs => Set<LogRecord>();
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

    }
}
