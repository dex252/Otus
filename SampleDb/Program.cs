using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SampleDb.Models.Contexts;
using SampleDb.Repositories.Db;

namespace SampleDb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddMvc();

            services.AddScoped(typeof(IDbRepository<>), typeof(EFRepository<>));
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