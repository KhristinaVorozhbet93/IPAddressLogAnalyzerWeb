using IPAddressLogAnalyzer.Domain.Entities;
using System.Net;

namespace IPAddressLogAnalyzer.Domain.Interfaces
{
    public interface ILogFilterService
    {
        List<AccesLog> GetIPAddressesWithConfigurations(List<AccesLog> ipAddresses);
        List<AccesLog> GetRangeIPAddresses(List<AccesLog> ipAddresses, IPAddress addressStart, IPAddress addressMask);
        List<AccesLog> GetIPAddressesInTimeInterval(List<AccesLog> ipAddresses, DateTime timeStart, DateTime timeEnd);
        List<AccesLog> GetIPAddressesWithCountTimeRequests(List<AccesLog> ipAddresses);
    }
}
