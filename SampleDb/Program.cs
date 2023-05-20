using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SampleDb.Managers.Db;
using SampleDb.Models.Contexts;
using SampleDb.Repositories.Db;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SampleDb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
           
            services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.AllowTrailingCommas = true;
                        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                        options.JsonSerializerOptions.WriteIndented = true;
                        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped<IDbRepository, EFRepository>();
            services.AddScoped<IDbParametersCreator, NgParametersCreator>();
            services.AddScoped(typeof(DbContext), typeof(DataContext));

            services.AddDbContext<DataContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetSection("DbConnections:Postgre").Value);
                options.UseSnakeCaseNamingConvention();
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}