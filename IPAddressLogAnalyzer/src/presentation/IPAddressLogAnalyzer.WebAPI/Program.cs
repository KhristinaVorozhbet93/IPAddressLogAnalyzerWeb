using Minio;
using SocialNetwork.DataEntityFramework;
using Microsoft.EntityFrameworkCore;
using IPAddressLogAnalyzer.Domain.Interfaces;
using IPAddressLogAnalyzer.FileReaderService;
using IPAddressLogAnalyzer.FilterService;
using System.Linq;
using Minio.DataModel.Args;
using Minio.Exceptions;
using Minio.DataModel;
using System;
using System.Reactive.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.AccessControl;

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

            builder.Services.AddHostedService<LogFileProcessor>();
            //builder.Services.AddHostedService<LogMinioProcessor>();


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
                var endpoint = "192.168.1.6:9000";
                var accessKey = "EeoUjMZBBNh2WNwXB3CE";
                var secretKey = "xkBppr2vnEA6QJwpUG0hJnGEmNbMJGc3lk4PaPh4";
                var bucketName = "miniologs";

                var minioClient = new MinioClient()
                    .WithEndpoint(endpoint)
                    .WithCredentials(accessKey, secretKey);

                var args = new BucketExistsArgs()
                    .WithBucket(bucketName);
                if (await minioClient.BucketExistsAsync(args))
                {
                    //здесь скачиваем все файлы
                }
               
                //далее мы читаем эти файлы
                //и названия помещает в Hashset
               

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






