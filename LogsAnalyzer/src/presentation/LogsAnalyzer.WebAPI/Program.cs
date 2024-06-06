using Minio;
using Microsoft.EntityFrameworkCore;
using LogsAnalyzer.Domain.Interfaces;
using LogsAnalyzer.DataEntityFramework;
using LogsAnalyzer.DataEntityFramework.Repositories;
using LogsAnalyzer.LogFileReaderServices;
using LogsAnalyzer.LogFilterServices;
using LogsAnalyzer.Domain.Services;

namespace LogsAnalyzer.WebAPI
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
                    $"Server={postgresConfig.ServerName};" +
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

            builder.Services.AddOptions<LogFileProcessorSettings>()
                .BindConfiguration("LogFileProccesorSettings")
                .ValidateDataAnnotations()
                .ValidateOnStart();

            //builder.Services.AddOptions<LogMinioSettings>()
            //    .BindConfiguration("LogMinioSettings")
            //    .ValidateDataAnnotations()
            //    .ValidateOnStart();

            builder.Services.AddScoped<ILogFilterService, LogFilterService>();
            builder.Services.AddScoped<ILogReaderService, LogFileReaderService>();
            builder.Services.AddScoped(typeof(IRepositoryEF<>), typeof(EFRepository<>));
            builder.Services.AddScoped<ILogRepository, LogRepository>();
            builder.Services.AddHostedService<LogFileProcessor>();
            //builder.Services.AddHostedService<LogMinioProcessor>();
            builder.Services.AddScoped<LogRecordService>();

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
                //var endpoint = "192.168.1.6:9000";
                //var accessKey = "";
                //var secretKey = "";
                //var bucketName = "miniologs";

                //var minioClient = new MinioClient()
                //    .WithEndpoint(endpoint)
                //    .WithCredentials(accessKey, secretKey);

                //var args = new BucketExistsArgs()
                //    .WithBucket(bucketName);
                //if (await minioClient.BucketExistsAsync(args))
                //{
                //    //здесь скачиваем все файлы
                //}

                //далее мы читаем эти файлы
            });


            app.MapGet("/get_status", async () =>
            {


            });

            app.MapPost("/stop_handler", async () =>
            {


            });

            app.MapPost("/start_handler", async () =>
            {


            });

            app.Run();
        }
    }
}






