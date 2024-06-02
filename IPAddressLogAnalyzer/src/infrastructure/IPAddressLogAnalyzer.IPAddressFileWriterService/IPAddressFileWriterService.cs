using IPAddressLogAnalyzer.Domain.Interfaces;
using System.Net;

namespace IPAddressLogAnalyzer.IPAddressFileWriterService
{
    public class IPAddressFileWriterService : IIPAddressWriterService
    {
        private readonly string _filePath;
        public IPAddressFileWriterService(string filePath)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(nameof(filePath));
            _filePath = filePath;
        }
        public async Task WriteAsync
            (Dictionary<IPAddress, int> ips, CancellationToken cancellationToken)
        {
            ArgumentException.ThrowIfNullOrEmpty(_filePath);
            ArgumentNullException.ThrowIfNull(ips);
            using StreamWriter writer = new(_filePath, false);
            foreach (var ip in ips)
            {
                await writer.WriteLineAsync($"{ip.Key} {ip.Value}".ToCharArray(), cancellationToken);
            }
        }
    }
}
