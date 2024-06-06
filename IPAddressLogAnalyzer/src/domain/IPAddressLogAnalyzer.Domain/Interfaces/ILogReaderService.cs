using LogsAnalyzer.Domain.Entities;

namespace LogsAnalyzer.Domain.Interfaces
{
    public interface ILogReaderService
    {
        Task<List<LogRecord>> ReadFromFiletoListAsync(string path, CancellationToken cancellationToken);
    }
}
