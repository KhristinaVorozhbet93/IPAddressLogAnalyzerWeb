namespace IPAddressLogAnalyzer.Configurations.Interfaces
{
    public interface IConfigurationParser
    {
        IPConfiguration ParseIPConfigurationData
            (string fileLog, string fileOutput, string timeStartString,
            string timeEndString, string? addressStart, string? addressMask);
    }
}
