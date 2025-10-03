using System.ComponentModel.DataAnnotations;

namespace Articles.Contracts.Attributes;

/// <summary>
/// Атрибут для валидации строковых полей.
/// Запрещает использование типичных фраз от спам-бота.
/// </summary>
public class AntibotValidationAttribute : ValidationAttribute
{
    /// <inheritdoc/>
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var badTitles = new[] { "Продаю мопед", "Продам мопед" };
        if (badTitles.Contains(value.ToString()))
        {
            return new ValidationResult("Нельзя продавать мопед");
        }

        return ValidationResult.Success;
    }
}