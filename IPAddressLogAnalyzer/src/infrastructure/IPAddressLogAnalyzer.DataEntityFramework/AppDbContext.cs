using IPAddressLogAnalyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SocialNetwork.DataEntityFramework
{
    public class AppDbContext : DbContext
    {
        DbSet<LogRecord> AccesLogs => Set<LogRecord>();
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

    }
}
