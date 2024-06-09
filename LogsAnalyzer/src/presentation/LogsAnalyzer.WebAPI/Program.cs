using Minio;
using Microsoft.EntityFrameworkCore;
using LogsAnalyzer.Domain.Interfaces;
using LogsAnalyzer.DataEntityFramework;
using LogsAnalyzer.DataEntityFramework.Repositories;
using LogsAnalyzer.LogFileReaderServices;
using LogsAnalyzer.LogFilterServices;
using Minio.DataModel.Args;
using System.Reactive.Linq;
using System.Net;
using System.Runtime.Intrinsics.X86;
using System.Security.AccessControl;
using Minio.DataModel;
using Minio.DataModel.Result;

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
            builder.Services.AddScoped(typeof(IRepositoryEF<>), typeof(EFRepository<>));
            builder.Services.AddScoped<ILogRepository, LogRepository>();
            //builder.Services.AddHostedService<LogFileProcessor>();
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
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;

                var endpoint = "192.168.1.6:9000";
                var accessKey = "pkz2VomYVTqpZC1YMDgT";
                var secretKey = "rpzwVeSJMbxpOdOnXGDQSsIABnCfP5gsuNZtDoih";
                var bucketName = "miniologs";
                var path = "C:\\Users\\Христина\\Desktop\\minio";


                var minioClient = new MinioClient()
                    .WithEndpoint(endpoint)
                    .WithCredentials(accessKey, secretKey)
                    .Build();

                var args = new BucketExistsArgs()
                    .WithBucket(bucketName);


                var list = await minioClient.ListBucketsAsync();
          
                foreach (var bucket in list.Buckets)
                {
                    Console.WriteLine($"{bucket.Name} {bucket.CreationDateDateTime}");
                }
                
                
                if (await minioClient.BucketExistsAsync(args))
                {
                    try
                    {
                        
                        //var getListBucketsTask = await minioClient.ListBucketsAsync().ConfigureAwait(false);

                        // Iterate over the list of buckets.
                        //foreach (var bucket in getListBucketsTask.Buckets)
                        //{
                          //  Console.WriteLine(bucket.Name + " " + bucket.CreationDateDateTime);
                        //}

                     

                        await minioClient.MakeBucketAsync(
                            new MakeBucketArgs()
                                .WithBucket("test"));
                      
                        Console.WriteLine($"Created bucket {bucketName}");

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    //Console.WriteLine($"Бакет {bucketName}  найден.");
                }




                //foreach (var bucket in list.Buckets)
                //{
                //    Console.WriteLine($"{bucket.Name} {bucket.CreationDateDateTime}");
                //}
                //Console.WriteLine();


                // string fileName = "C:\\Users\\Христина\\Desktop\\minio\\File.log";

                // var arg = new ListObjectsArgs()
                //.WithBucket(bucketName)
                //.WithRecursive(true);
                // var a = await minioClient.ListObjectsAsync(arg);
                // Console.WriteLine($"Downloaded the file {fileName} from bucket {bucketName}");
                // //Console.WriteLine(a.ToString());



                //var a = await minioClient.ListObjectsAsync(listargs);

                //if (a is null)
                //{
                //    Console.WriteLine("Нет файла");
                //}
                //else Console.WriteLine("Есть файла");


                //// Получаем список объектов в бакете
                //var objects = await minioClient.ListObjectsAsync(listargs);

                //// Скачиваем каждый объект в папку
                //foreach (var obj in objects.Key)
                //{
                //    Console.WriteLine(obj.ToString());
                //    //string fileName = obj.ToString();
                //    //string filePath = Path.Combine(path, fileName);

                //    //// Создаем директорию, если она не существует
                //    //Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                //    //var ar = new GetObjectArgs().WithBucket(bucketName).WithFile(fileName); 
                //    //// Скачиваем объект
                //    //await minioClient.GetObjectAsync(ar);
                //    //Console.WriteLine($"Файл {fileName} скачан.");
                //}

                //Console.WriteLine("Все файлы скачаны.");


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






