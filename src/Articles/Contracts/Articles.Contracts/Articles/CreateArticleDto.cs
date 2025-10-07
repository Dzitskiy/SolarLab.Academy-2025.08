using Articles.Contracts.Attributes;

namespace Articles.Contracts.Articles;

/// <summary>
/// Модель создания статьи.
/// </summary>
public class CreateArticleDto
{
    /// <summary>
    /// Заголовок.
    /// </summary>
    [AntibotValidation]
    public string Title { get; set; }
    
    /// <summary>
    /// Описание.
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Дата/время создания.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string UserName { get; set; }
}