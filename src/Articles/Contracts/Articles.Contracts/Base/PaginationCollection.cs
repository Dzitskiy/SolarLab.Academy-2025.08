namespace Articles.Contracts.Base;

/// <summary>
/// Пагинированная коллекция.
/// </summary>
/// <typeparam name="T">Тип элементов коллекции.</typeparam>
public class PaginationCollection<T>
{
    /// <summary>
    /// Элементы коллекции.
    /// </summary>
    public IReadOnlyCollection<T> Items { get; set; } = [];
    
    /// <summary>
    /// Общее число элементов.
    /// </summary>
    public int Total { get; set; }
    
    /// <summary>
    /// Кол-во доступных страниц.
    /// </summary>
    public int AvailablePages { get; set; }
}