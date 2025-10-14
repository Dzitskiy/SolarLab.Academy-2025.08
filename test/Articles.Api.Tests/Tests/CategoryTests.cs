using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Articles.Api.Tests.Tests;

public class CategoryTests(TestWebAppFactory appFactory) : IClassFixture<TestWebAppFactory>
{
    /// <summary>
    /// GET Category/now/cahsed/distributed должен вернуть значение из кеша
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetDistributedCashed_Should_Return_CachedValue_Success()
    {
        //arrange

        // создать HttpClient для API
        var client = appFactory.CreateClient();

        // получить сервис для DI контейнера приложения
        var distributedCache = appFactory.Services.GetService<IDistributedCache>();

        // записать в кеш тестовое значение
        var testDateTime = new DateTime(1915, 6, 15, 11, 30, 25);
        await distributedCache.SetStringAsync("CurrentTime_Distributed",
            JsonSerializer.Serialize(testDateTime));

        // act
        var response = await client.GetAsync("api/Category/now/cahsed/distributed", CancellationToken.None);
        var result = await response.Content.ReadFromJsonAsync<DateTime>(CancellationToken.None);

        // arrange
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        result.ShouldBe(testDateTime);
    }
}