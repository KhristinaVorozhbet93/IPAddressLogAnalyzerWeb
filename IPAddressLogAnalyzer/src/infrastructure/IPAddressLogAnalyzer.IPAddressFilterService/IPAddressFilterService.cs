using IPAddressLogAnalyzer.Configurations;
using IPAddressLogAnalyzer.Domain.Entities;
using IPAddressLogAnalyzer.Domain.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
namespace IPAddressLogAnalyzer.IPAddressFilterService
{
    public class IPAddressFilterService : IIPAddressFilterService
    {
        private readonly DateTime _timeStart;
        private readonly DateTime _timeEnd;
        private readonly string? _addressStart;
        private readonly string? _addressMask;
        public IPAddressFilterService(IOptions<IPConfiguration> options)
        {
            _timeStart = options.Value.TimeStart;
            _timeEnd = options.Value.TimeEnd;
            _addressStart = options.Value.AddressStart;
            _addressMask = options.Value.AddressMask;
        }

        public Dictionary<IPAddress, int> GetIPAddressesWithConfigurations(List<IP> ipAddresses)
        {
            ipAddresses.Sort();
            var timeAddresses = GetIPAddressesInTimeInterval(ipAddresses, _timeStart, _timeEnd);

            var countTimeRequestIPAddresses = GetIPAddressesWithCountTimeRequests(timeAddresses);

            if (!string.IsNullOrEmpty(_addressStart) && !string.IsNullOrEmpty(_addressMask))
            {
                var filtredAddresses = GetRangeIPAddresses
                    (countTimeRequestIPAddresses, IPAddress.Parse(_addressStart), IPAddress.Parse(_addressMask));
                return filtredAddresses;
            }
            return countTimeRequestIPAddresses;
        }

        public Dictionary<IPAddress, int> GetRangeIPAddresses(Dictionary<IPAddress, int> ipAddresses, IPAddress addressStart, IPAddress addressMask)
        {
            Dictionary<IPAddress, int> filteredIPAddresses = new Dictionary<IPAddress, int>();
            foreach (var ip in ipAddresses)
            {
                if (IsIPAddressInRange
                    (ip.Key, addressStart, addressMask))
                {
                    filteredIPAddresses.Add(ip.Key, ip.Value);
                }
            }
            return filteredIPAddresses;
        }
        public List<IP> GetIPAddressesInTimeInterval(List<IP> ipAddresses, DateTime timeStart, DateTime timeEnd)
        {
            return ipAddresses.Where(ip =>
                    ip.TimeRequest <= timeEnd &&
                    ip.TimeRequest >= timeStart)
                    .ToList();
        }
        public Dictionary<IPAddress, int> GetIPAddressesWithCountTimeRequests(List<IP> ipAddresses)
        {
            return ipAddresses
                    .GroupBy(ip => ip.Address)
                    .ToDictionary(group => group.Key, group => group.Count());
        }

        private bool IsIPAddressInRange(IPAddress ipAddress, IPAddress addressStart, IPAddress addressMask)
        {
            byte[] ipBytes = ipAddress.GetAddressBytes();
            byte[] startBytes = addressStart.GetAddressBytes();
            byte[] maskBytes = addressMask.GetAddressBytes();

            if (ipBytes.Length != startBytes.Length || startBytes.Length != maskBytes.Length)
            {
                return false;
            }

            for (int i = 0; i < ipBytes.Length; i++)
            {
                if ((ipBytes[i] & maskBytes[i]) < (startBytes[i] & maskBytes[i]))
                {
                    return false;
                }

                if ((ipBytes[i] & maskBytes[i]) > (startBytes[i] & maskBytes[i]) + (255 - maskBytes[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}