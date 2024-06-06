using IPAddressLogAnalyzer.Domain.Entities;

namespace IPAddressLogAnalyzer.Domain.Interfaces
{
    public interface ILogRepository : IRepositoryEF<LogRecord>
    {    
    }
}
