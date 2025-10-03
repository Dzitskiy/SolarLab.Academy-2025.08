using Articles.AppServices.Contexts.Articles.Builder;
using Articles.AppServices.Contexts.Articles.Repository;
using Articles.AppServices.Contexts.Articles.Services;
using Articles.AppServices.Contexts.Files.Repositories;
using Articles.AppServices.Contexts.Files.Services;
using Articles.AppServices.Validators;
using Articles.Infrastructure.ComponentRegistrar.MapProfiles;
using Articles.Infrastructure.DataAccess.Contexts.Articles.Repositories;
using Articles.Infrastructure.DataAccess.Contexts.Files.Repositories;
using Articles.Infrastructure.DataAccess.Repositories;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace Articles.Infrastructure.ComponentRegistrar;

public static class ComponentRegistrar
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<IFileService, FileService>();
        services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));
        return services;
    }
    
    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddTransient<IArticlePredicateBuilder, ArticlePredicateBuilder>();
        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<IFileRepository, FileRepository>();
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

        return services;
    }

    /// <summary>
    /// Добавить пакет FluentValidation и валидаторы моделей.
    /// </summary>
    /// <param name="services">Список сервисов.</param>
    /// <returns>Список сервисов.</returns>
    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateArticleValidator>();
        services.AddFluentValidationAutoValidation();
        return services;
    }

    private static MapperConfiguration GetMapperConfiguration()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ArticleProfile>();
            cfg.AddProfile<FileProfile>();
        });
        configuration.AssertConfigurationIsValid();
        return configuration;
    }
}