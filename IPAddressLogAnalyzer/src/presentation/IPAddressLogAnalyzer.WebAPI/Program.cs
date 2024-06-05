using Minio;
using SocialNetwork.DataEntityFramework;
using Microsoft.EntityFrameworkCore;
using IPAddressLogAnalyzer.Domain.Interfaces;
using IPAddressLogAnalyzer.FileReaderService;
using IPAddressLogAnalyzer.FilterService;
using System.Linq;

namespace IPAddressLogAnalyzer.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var postgresConfig = builder.Configuration
                .GetRequiredSection("PostgresConfig")
                .Get<PostgresConfig>();
            if (postgresConfig is null)
            {
                throw new InvalidOperationException("PostgresConfig is not configured");
            }

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    $"Host={postgresConfig.ServerName};" +
                    $"Port={postgresConfig.Port};" +
                    $"Database={postgresConfig.DatabaseName};" +
                    $"Username={postgresConfig.UserName};" +
                    $"Password={postgresConfig.Password};"));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddOptions<IPConfiguration>()
                .BindConfiguration("IPConfiguration")
                .ValidateDataAnnotations()
                .ValidateOnStart();

            builder.Services.AddOptions<LogFileProccesorSettings>()
                .BindConfiguration("LogFileProccesorSettings")
                .ValidateDataAnnotations()
                .ValidateOnStart();

            
            builder.Services.AddScoped<ILogFilterService, LogFilterService>();
            builder.Services.AddScoped<ILogReaderService, LogFileReaderService>();
            builder.Services.AddHostedService<LogFileProcessor>();

            //builder.Services.AddScoped<IPService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapGet("/file", async (ILogReaderService service) =>
            {
                var endpoint = @"192.168.1.6:9000/buckets";
                var accessKey = "";
                var secretKey = "";

                var minioClient = new MinioClient()
                .WithEndpoint(endpoint)
                .WithCredentials(accessKey, secretKey);

                //var getListBucketsTask = await minioClient.ListBucketsAsync().ConfigureAwait(false);

                //if (getListBucketsTask.Buckets is not null)
                //{
                //    foreach (var bucket in getListBucketsTask.Buckets)
                //    {
                //        if (bucket is not null)
                //            Console.WriteLine(bucket.Name + " " + bucket.CreationDateDateTime);
                //        else Console.WriteLine("Пусто");
                //    }
                //}
                //else
                //{
                //    Console.WriteLine("Пусто");
                //}

                string path = "C:\\Users\\Христина\\Desktop\\Логи\\";
                var logFiles = Directory.GetFiles(path, "*.log", SearchOption.TopDirectoryOnly);

                foreach (var logFile in logFiles.OrderBy(f => f).Take(3))
                {
                    Console.WriteLine(logFile);
                }
            });

            app.Run();
        }
    }
}






