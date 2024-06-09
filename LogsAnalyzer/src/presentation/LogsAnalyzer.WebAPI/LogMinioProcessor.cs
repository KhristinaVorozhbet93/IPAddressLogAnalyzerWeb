using LogsAnalyzer.Domain.Interfaces;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace LogsAnalyzer.WebAPI
{
    public class LogMinioProcessor : BackgroundService
    {
        private readonly ILogger<LogMinioProcessor> _logger;
        private readonly IOptions<LogMinioSettings> _options;
        private readonly IServiceProvider _serviceProvider;
        private readonly HashSet<string> _processedFiles = new HashSet<string>();

        public LogMinioProcessor(ILogger<LogMinioProcessor> logger,
            IOptions<LogMinioSettings> options,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _options = options;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //можно доавбить minio через внедрение зависимостей

                var endpoint = _options.Value.Endpoint;
                var accessKey = _options.Value.AccesKey;
                var secretKey = _options.Value.SecretKey;
                var bucketName = _options.Value.BucketName;

                var minioClient = new MinioClient()
                    .WithEndpoint(endpoint)
                    .WithCredentials(accessKey, secretKey);


                //скачиваем все файлы
                //то нам нужно запоминать дату последнего обращения
                //удалять файлы которые обработали можно
                //получать файлы после указанной даты

                //гугл, how to use minio api 

                var location = "us-east-1";
                var objectName = "golden-oldies.zip";
                var filePath = "C:\\Users\\username\\Downloads\\golden_oldies.mp3";
                var contentType = "application/zip";

                try
                {

                    //Make a bucket on the server, if not already present.
                    //вот здесь получаем файлы из minio


                    var logFiles = Directory.GetFiles("_options.Value.DirectoryPath", "*.log", SearchOption.TopDirectoryOnly);

                    foreach (var logFile in logFiles.OrderBy(f => f).Take(_options.Value.CountFiles))
                    {
                        if (!_processedFiles.Contains(logFile))
                        {
                            await ReadAndProcessFileAsync(logFile, stoppingToken);
                            _processedFiles.Add(logFile);
                            _logger.LogInformation("Файл обработан: {logFile}", logFile);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка при обработке файлов логов");
                }

                await Task.Delay(TimeSpan.FromSeconds(_options.Value.TimeInterval), stoppingToken);

            }
        }

        private async Task ReadAndProcessFileAsync(string logFile, CancellationToken stoppingToken)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            var localServiceProvider = scope.ServiceProvider;
            var logReaderService = localServiceProvider.GetRequiredService<ILogReaderService>();

            try
            {
                await logReaderService.ReadFromFileAsync(logFile, stoppingToken);
                //здесь будет запись в бд
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при чтении файла: {logFile}", logFile);
            }
        }
    }
}
