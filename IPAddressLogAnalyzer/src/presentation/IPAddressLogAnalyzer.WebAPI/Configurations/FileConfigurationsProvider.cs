﻿using IPAddressLogAnalyzer.Configurations.Interfaces;

namespace IPAddressLogAnalyzer.Configurations
{
    public class FileConfigurationsProvider : IConfigurationsProvider
    {
        private readonly IConfigurationSection _ipConfigurationSection;
        private readonly IConfigurationParser _configurationParser;
        public FileConfigurationsProvider(IConfigurationSection ipConfigSection, 
            IConfigurationParser configurationParser)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(ipConfigSection));
            ArgumentException.ThrowIfNullOrEmpty(nameof(configurationParser));
            _ipConfigurationSection = ipConfigSection;
            _configurationParser = configurationParser;
        }

        public IPConfiguration GetIPConfiguration()
        {
            var fileLog = _ipConfigurationSection["file_log"];
            if (string.IsNullOrWhiteSpace(fileLog))
            {
                throw new ArgumentException(nameof(fileLog));
            }
            var fileOutput = _ipConfigurationSection["file_output"];
            if (string.IsNullOrWhiteSpace(fileOutput))
            {
                throw new ArgumentException(nameof(fileOutput));
            }
            var timeStartString = _ipConfigurationSection["time_start"];
            if (string.IsNullOrEmpty(timeStartString))
            {
                throw new ArgumentException(nameof(timeStartString));
            }
            var timeEndString = _ipConfigurationSection["time_end"];
            if (string.IsNullOrWhiteSpace(timeEndString))
            {
                throw new ArgumentException(nameof(timeEndString));
            }
            var addressStart = _ipConfigurationSection["address_start"];
            var addressMask = _ipConfigurationSection["address_mask"];

            return _configurationParser.ParseIPConfigurationData
                (fileLog, fileOutput, timeStartString, timeEndString, addressStart, addressMask);
        }
    }
}
