using IPAddressLogAnalyzer.Domain.Entities;

namespace IPAddressLogAnalyzer.Domain.Interfaces
{
    public interface ILogReaderService
    {
        Task<List<LogRecord>> ReadFromFiletoListAsync(string path,CancellationToken cancellationToken);
    }
}
