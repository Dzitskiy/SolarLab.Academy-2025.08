using System.Net;
using System.Net.Http.Json;
using Articles.Api.Tests.Stubs;
using Articles.Contracts.Articles;
using Articles.Contracts.Errors;
using Shouldly;

namespace Articles.Api.Tests.Tests;

public class ArticleTests(TestWebAppFactory app) : IClassFixture<TestWebAppFactory>
{
    /// <summary>
    /// GET Articles должен вернуть статью по существующему идентификатору.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetById_Should_Return_Success()
    {
        // arrange
        var httpClient = app.CreateClient();
        var id = ArticleRepositoryStub.TestId;

        // act
        var response = await httpClient.GetAsync($"api/Articles/{id}", CancellationToken.None);
        var dto = await response.Content.ReadFromJsonAsync<ArticleDto>(CancellationToken.None);

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        dto.ShouldNotBeNull();
        dto.Id.ToString().ShouldBe(id);
        dto.Title.ShouldBe(ArticleRepositoryStub.TestTitle);
        dto.Description.ShouldBe(ArticleRepositoryStub.TestDescription);
    }

    /// <summary>
    /// GET Articles должен вернуть 404 по несуществующему идентификатору.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task GetById_NotExists_Should_Return_NotFound()
    {
        // arrange
        var httpClient = app.CreateClient();
        var id = Guid.NewGuid();

        // act
        var response = await httpClient.GetAsync($"api/Articles/{id}", CancellationToken.None);
        var dto = await response.Content.ReadFromJsonAsync<ErrorDto>(CancellationToken.None);

        // assert
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        dto.ShouldNotBeNull();
        dto.Message.ShouldBe($"Сущность с идентификатором {id} не была найдена.");
    }
}