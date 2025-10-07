using Articles.Contracts.Articles;
using FluentValidation;

namespace Articles.AppServices.Validators;

/// <summary>
/// Валидатор модели <see cref="CreateArticleDto"/>.
/// </summary>
public class CreateArticleValidator : AbstractValidator<CreateArticleDto>
{
    public CreateArticleValidator()
    {
        RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("Не указан заголовок.");
        RuleFor(x => x.Title).Length(3, 50).WithMessage("Заголовок должен иметь длину от 3 до 50.");

        RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Не указано описание.");
        RuleFor(x => x.Description).Length(10, 200).WithMessage("Описание должно иметь длину от 10 до 200.");

        RuleFor(x => x.UserName).NotNull().NotEmpty().WithMessage("Не указано имя пользователя.");
        RuleFor(x => x.UserName).Length(3, 20).WithMessage("Имя пользователя должно иметь длину от 3 до 20.");
        RuleFor(x => x.UserName).Matches("^[^!@#]*$").WithMessage("Имя пользователя не должно содержать символы !@#.");

        RuleFor(x => x.CreatedAt).LessThan(x => DateTime.Now);
    }
}