using LogsAnalyzer.Domain.Entities;

namespace LogsAnalyzer.Domain.Interfaces
{
    public interface ILogReaderService
    {
        Task ReadFromFileAsync(string path, CancellationToken cancellationToken);
    }
}
