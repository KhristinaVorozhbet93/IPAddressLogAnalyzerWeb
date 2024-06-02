using IPAddressLogAnalyzer.Configurations;
using IPAddressLogAnalyzer.Domain.Interfaces;
using IPAddressLogAnalyzer.Configurations.Interfaces;
using Minio;
using Microsoft.AspNetCore.DataProtection;

namespace IPAddressLogAnalyzer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddOptions<IPConfiguration>()
                .BindConfiguration("IPConfiguration")
                .ValidateDataAnnotations()
                .ValidateOnStart();

            builder.Services.AddScoped<IConfigurationParser, ConfigurationParser>();
            //builder.Services.AddScoped<IIPAddressFilterService, IPAddressFilterService>();

            //     builder.Services.AddScoped<IConfigurationsProvider>(provider =>
            //new FileConfigurationsProvider(ipConfigSection, provider.GetRequiredService<IConfigurationParser>()));
            //builder.Services.AddScoped<IPService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapPost("/file", async() => {
                var endpoint = "http://192.168.1.6:9000";
                var accessKey = "";
                var secretKey = ""; 
        
                var minioClient = new MinioClient()
                .WithEndpoint(endpoint)
                .WithCredentials(accessKey, secretKey);

                var getListBucketsTask = await minioClient.ListBucketsAsync().ConfigureAwait(false);

                foreach (var bucket in getListBucketsTask.Buckets)
                {
                    Console.WriteLine(bucket.Name + " " + bucket.CreationDateDateTime);
                }
            });


            app.Run();
        }
    }
}






