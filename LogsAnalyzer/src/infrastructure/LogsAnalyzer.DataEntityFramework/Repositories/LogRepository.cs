using LogsAnalyzer.Domain.Entities;
using LogsAnalyzer.Domain.Interfaces;

namespace LogsAnalyzer.DataEntityFramework.Repositories
{
    public class LogRepository : EFRepository<LogRecord>, ILogRepository
    {
        public LogRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}
