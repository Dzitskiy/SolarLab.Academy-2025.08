namespace Articles.Contracts.Base;

/// <summary>
/// Позволяет использовать пагинацию.
/// </summary>
public interface IPagination
{
    /// <summary>
    /// Номер страницы.
    /// </summary>
    public int Page { get; set; }
    
    /// <summary>
    /// Размер выборки.
    /// </summary>
    public int Take { get; set;  }
}