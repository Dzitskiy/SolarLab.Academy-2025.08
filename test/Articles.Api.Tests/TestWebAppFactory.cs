using Articles.Api.Tests.Stubs;
using Articles.AppServices.Contexts.Articles.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace Articles.Api.Tests;

/// <summary>
/// Тестовое Web API приложение на основе тестируемого.
/// </summary>
public class TestWebAppFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // подменить IArticleRepository
            var articleRepository = services.FirstOrDefault(x => x.ServiceType == typeof(IArticleRepository));
            services.Remove(articleRepository);

            services.AddScoped<IArticleRepository, ArticleRepositoryStub>();

            // подменить IDistributedCache
            var distributedCaches = services.Where(x => x.ServiceType == typeof(IDistributedCache)).ToArray();
            foreach (var distributedCache in distributedCaches)
            {
                services.Remove(distributedCache);
            }

            services.AddDistributedMemoryCache();
        });

        base.ConfigureWebHost(builder);
    }
}