using Articles.AppServices.Contexts.Articles.Builder;
using Articles.AppServices.Contexts.Articles.Repository;
using Articles.AppServices.Contexts.Articles.Services;
using Articles.Contracts.Articles;
using AutoMapper;
using Moq;

namespace Articles.Unit.Tests;

public class ArticleServiceTests
{
    [Fact]
    public async Task GetByIdAsync_Should_Call_RepositoryMethodAsync()
    {
        // Arrange
        Mock<IArticleRepository> articleRepositoryMock = new Mock<IArticleRepository>();
        Mock<IArticlePredicateBuilder> predicateBuilderMock = new Mock<IArticlePredicateBuilder>();
        Mock<IMapper> mapperMock = new Mock<IMapper>();
        var sut = new ArticleService(articleRepositoryMock.Object, predicateBuilderMock.Object, mapperMock.Object);
        var tokenSource = new CancellationTokenSource();
        var token = tokenSource.Token;
        var guid = Guid.NewGuid();
        var article = new ArticleDto();
        articleRepositoryMock
            .Setup(x => x.GetByIdAsync(guid, token))
            .ReturnsAsync(article);

        // Act
        var result = await sut.GetByIdAsync(guid, token);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(article, result);
        articleRepositoryMock.Verify(x => x.GetByIdAsync(guid, token), Times.Once);
    }
}