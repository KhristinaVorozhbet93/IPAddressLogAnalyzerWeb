using IPAddressLogAnalyzer.Domain.Entities;
using IPAddressLogAnalyzer.Domain.Interfaces;
using SocialNetwork.DataEntityFramework;

namespace IPAddressLogAnalyzer.DataEntityFramework.Repositories
{
    public class LogRepository : EFRepository<LogRecord>, ILogRepository
    {
        public LogRepository(AppDbContext appDbContext) : base(appDbContext){}
    }
}
