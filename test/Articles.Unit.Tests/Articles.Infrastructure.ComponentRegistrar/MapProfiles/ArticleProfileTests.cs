using Articles.Contracts.Articles;
using Articles.Domain.Entities;
using Articles.Infrastructure.ComponentRegistrar.MapProfiles;
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Shouldly;

namespace Articles.Unit.Tests.Articles.Infrastructure.ComponentRegistrar.MapProfiles;

public class ArticleProfileTests
{
    private readonly MapperConfiguration _configurationProvider;

    private IMapper Mapper { get; }
    private Fixture Fixture { get; }

    public ArticleProfileTests()
    {
        _configurationProvider = new MapperConfiguration(delegate (IMapperConfigurationExpression configure)
        {
            configure.AddProfiles(new List<Profile>
            {
                new ArticleProfile()
            });
        });
        Fixture = new Fixture();
        Mapper = _configurationProvider.CreateMapper();
    }

    [Fact]
    public void AutoMapperProfile_CheckConfigurationIsValid()
    {
        _configurationProvider.AssertConfigurationIsValid();
    }

    /// <summary>
    /// Проверка <see cref="AdvertProfile"/>.
    /// </summary>
    [Fact]
    public void AdvertProfile_CreateAdvertRequest_To_Advert()
    {
        // Arrange
        var title = Fixture.Create<string>();
        var description = Fixture.Create<string>();

        var source = Fixture
            .Build<CreateArticleDto>()
            .OmitAutoProperties()
            .With(x => x.Title, title)
            .With(x => x.Description, description)
            .Create();

        // Act
        var result = Mapper.Map<CreateArticleDto, Article>(source);

        // Assert
        result.Should().NotBeNull();
        result.ShouldBeOfType<Article>();
        result.Id.Should().Be(Guid.Empty);
        result.Title.Should().Be(title);
        result.Description.Should().Be(description);
    }
}