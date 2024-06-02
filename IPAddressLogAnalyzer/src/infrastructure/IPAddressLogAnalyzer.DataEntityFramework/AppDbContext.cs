using IPAddressLogAnalyzer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SocialNetwork.DataEntityFramework
{
    public class AppDbContext : DbContext
    {
        DbSet<AccesLog> AccesLogs => Set<AccesLog>();
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

    }
}
