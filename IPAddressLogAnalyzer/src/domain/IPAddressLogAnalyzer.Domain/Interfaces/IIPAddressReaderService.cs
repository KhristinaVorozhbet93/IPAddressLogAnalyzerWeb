using System.Net;

namespace IPAddressLogAnalyzer.Domain.Interfaces
{
    public interface IIPAddressReaderService
    {
        Task<Dictionary<IPAddress, int>> ReadAsync(CancellationToken cancellationToken);
    }
}
