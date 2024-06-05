using System.ComponentModel.DataAnnotations;

namespace IPAddressLogAnalyzer.WebAPI
{
    public class LogFileProccesorSettings
    {
        [Required]
        public string DirectoryPath { get; set; }

        [Required]
        public int TimeInterval { get; set; }

        [Required]
        public int CountFiles { get; set; }
    }
}
