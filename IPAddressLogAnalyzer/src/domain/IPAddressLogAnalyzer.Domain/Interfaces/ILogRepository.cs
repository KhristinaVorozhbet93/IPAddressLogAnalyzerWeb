using LogsAnalyzer.Domain.Entities;

namespace LogsAnalyzer.Domain.Interfaces
{
    public interface ILogRepository : IRepositoryEF<LogRecord>
    {
    }
}
