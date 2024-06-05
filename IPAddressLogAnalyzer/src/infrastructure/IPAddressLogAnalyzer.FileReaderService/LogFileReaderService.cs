using IPAddressLogAnalyzer.Domain.Entities;
using IPAddressLogAnalyzer.Domain.Interfaces;
using System.Globalization;
using System.Net;

namespace IPAddressLogAnalyzer.FileReaderService
{
    public class LogFileReaderService : ILogReaderService
    {
        private readonly ILogFilterService _iPAddressFilterService;
        public LogFileReaderService(ILogFilterService iPAddressFilterService)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(nameof(iPAddressFilterService));
            _iPAddressFilterService = iPAddressFilterService;
        }
        public async Task<List<AccesLog>> ReadFromFiletoListAsync(string filePath, CancellationToken cancellationToken)
        {
            ArgumentException.ThrowIfNullOrEmpty(filePath);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException
                    ($"Файл по заданному пути не обнаружен: {filePath}");
            }

            List<AccesLog> logs = new List<AccesLog>();
            CultureInfo provider = new CultureInfo("ru-RU");
            using (StreamReader reader = new StreamReader(filePath))
            {
                string? line;
                while ((line = await reader.ReadLineAsync(cancellationToken)) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        var parts = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
                        var ipAddress = IPAddress.Parse(parts[0].Trim());
                        var timeRequest = 
                            DateTime.ParseExact($"{parts[1]} {parts[2]}:{parts[3]}:{parts[4]}", "yyyy-MM-dd HH:mm:ss", provider);
                        var countRequest = Convert.ToInt16(parts[5].Trim());
                        var resource = parts[6].Trim();
                        var path = parts[7].Trim();
                        var method = parts[8].Trim();
                        var response = parts[9].Trim();

                        logs.Add(new AccesLog(ipAddress, timeRequest, countRequest, resource, path, method, response));
                    }
                    else
                    {
                        //записываем в ErrorAccesLog
                    }
                }

                var filtredLogs = _iPAddressFilterService.GetIPAddressesWithConfigurations(logs);
                return filtredLogs;
            }
            throw new ArgumentException($"Не удалось найти файл: {filePath}");
        }
    }
}
