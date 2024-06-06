using System.ComponentModel.DataAnnotations;

namespace LogsAnalyzer.WebAPI
{
    public class LogMinioSettings
    {
        [Required]
        public string Endpoint { get; set; }

        [Required]
        public string AccesKey { get; set; }

        [Required]
        public string SecretKey { get; set; }

        [Required]
        public string BucketName { get; set; }

        [Required]
        public int TimeInterval { get; set; }

        [Required]
        public int CountFiles { get; set; }
    }
}
