using System.Net;

namespace IPAddressLogAnalyzer.Domain.Interfaces
{
    public interface IIPAddressWriterService
    {
        Task WriteAsync(Dictionary<IPAddress, int> ips, CancellationToken cancellationToken);
    }
}
