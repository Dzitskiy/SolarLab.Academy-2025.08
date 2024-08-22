using AutoMapper;
using SolarLab.Academy.AppServices.Repository;
using SolarLab.Academy.AppServices.Services;
using SolarLab.Academy.AppServices.WeatherForecast.Services;
using SolarLab.Academy.DataAccess.Context.Repository;
using SolarLab.Academy.DataAccess.MapProfile;
using SolarLab.Academy.Infrastructure;
using SolarLab.Academy.Infrastructure.Repository;

namespace SolarLab.Academy.Api
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


            //builder.Services.AddTransient<IWeatherForecastService, WeatherForecastService>();
            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile(new AdvertProfile());
            });
            builder.Services.AddSingleton(mapperConfiguration.CreateMapper());
            builder.Services.AddDbContext<BoardDbContext>();
            builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddTransient<IAdvertRepository, AdvertRepository>();
            builder.Services.AddTransient<IAdvertService, AdvertService>();




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

            app.Run();
        }
    }
}
