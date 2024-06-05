using IPAddressLogAnalyzer.Domain.Entities;

namespace IPAddressLogAnalyzer.Domain.Interfaces
{
    public interface ILogReaderService
    {
        Task<List<AccesLog>> ReadFromFiletoListAsync(string path,CancellationToken cancellationToken);
    }
}
