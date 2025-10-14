using Articles.Infrastructure.ComponentRegistrar;
using Articles.Infrastructure.DataAccess;
using Articles.Infrastructure.Middlewares;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSerilog(conf => conf
    .ReadFrom.Configuration(builder.Configuration)
    );
builder.Services.RegisterAppServices();
builder.Services.RegisterRepositories();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionString")));
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddFluentValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddResponseCaching();

builder.Services.AddMemoryCache(options => 
{
    options.SizeLimit = 1024;
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddStackExchangeRedisCache(options =>
{ 
    options.Configuration = builder.Configuration.GetConnectionString("Redis");

});


var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();

//}

//app.UseResponseCaching();

//app.Use(async (conext, next) =>
//{ 
//    conext.Response.GetTypedHeaders().CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
//    {
//        //Private = false,
//        Public = true,
//        MaxAge = TimeSpan.FromSeconds(10)
//    };
//await next();
//});

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program {}