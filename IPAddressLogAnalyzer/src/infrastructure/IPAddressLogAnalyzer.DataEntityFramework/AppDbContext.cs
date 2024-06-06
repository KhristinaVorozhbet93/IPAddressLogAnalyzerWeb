using IPAddressLogAnalyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SocialNetwork.DataEntityFramework
{
    public class AppDbContext : DbContext
    {
        DbSet<LogRecord> LogRecords => Set<LogRecord>();
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

    }
}
