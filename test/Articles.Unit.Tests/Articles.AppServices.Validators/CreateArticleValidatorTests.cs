using Articles.AppServices.Validators;
using Articles.Contracts.Articles;
using AutoFixture;
using FluentAssertions;

namespace Articles.Unit.Tests.Articles.AppServices.Validators;

/// <summary>
/// Тесты для <see cref="CreateArticleValidator"/>
/// </summary>
public class CreateArticleValidatorTests
{
    private readonly CreateArticleValidator _validator;
    private readonly Fixture _fixture;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateExcelForDebtorAllowancesValidatorTests"/> class.
    /// </summary>
    public CreateArticleValidatorTests()
    {
        _validator = new CreateArticleValidator();
        _fixture = new Fixture();
    }

    /// <summary>
    /// Проверяем, при валидной модели должно быть все ок.
    /// </summary>
    [Fact]
    public void ValidDto_Success()
    {
        // Arrange
        var dto = _fixture
            .Build<CreateArticleDto>()
            .With(x => x.Title, "Title")
            .With(x => x.Description, "Description")
            .With(x => x.UserName, "UserName")
            .With(x => x.CreatedAt, DateTime.Now)
            .Create();

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    /// <summary>
    /// Проверяем, при невалидной модели должна быть ошибка
    /// </summary>
    [Fact]
    public void ValidDto_Failure()
    {
        // Arrange
        var dto = _fixture
            .Build<CreateArticleDto>()
            .Without(x => x.Title)
            .With(x => x.Description, "Description")
            .With(x => x.UserName, "UserName")
            .With(x => x.CreatedAt, DateTime.Now)
            .Create();

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(2);
        result.Errors.Should().Contain(x => x.ErrorMessage == "Не указан заголовок.");
    }

    /// <summary>
    /// Проверяем, при невалидной модели должна быть ошибка
    /// </summary>
    [Fact]
    public void ValidDto_ChecKTitle()
    {
        // Arrange
        var dto = _fixture
            .Build<CreateArticleDto>()
            .With(x => x.Description, "Description")
            .With(x => x.UserName, "UserName")
            .With(x => x.CreatedAt, DateTime.Now)
            .Create();
        dto.Title = string.Empty;

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(2);
        result.Errors.Should().Contain(x => x.ErrorMessage == "Не указан заголовок.");
    }
}
