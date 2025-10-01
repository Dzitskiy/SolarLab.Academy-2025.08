using Articles.Infrastructure.ComponentRegistrar;
using Articles.Infrastructure.DataAccess;
using Articles.Infrastructure.Middlewares;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSerilog(conf => conf
    .ReadFrom.Configuration(builder.Configuration)
    // .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    // .Enrich.WithEnvironmentName()
    // .Enrich.WithMachineName()
    // .WriteTo.Elasticsearch(
    //     nodeUris: "http://localhost:9200",
    //     indexFormat: "articles-api-{0:yyyy.MM.dd}")
    // .WriteTo.Console()
    );
builder.Services.RegisterAppServices();
builder.Services.RegisterRepositories();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionString")));
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();

//}

app.UseAuthorization();

app.MapControllers();

app.Run();