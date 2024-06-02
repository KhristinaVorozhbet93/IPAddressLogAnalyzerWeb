using IPAddressLogAnalyzer.Domain.Entities;

namespace IPAddressLogAnalyzer.Domain.Interfaces
{
    public interface ILogReaderService
    {
        Task<List<AccesLog>> ReadFromFileAsync(CancellationToken cancellationToken);
    }
}
