using System.Runtime.Serialization;

namespace Articles.AppServices.Exceptions;

/// <summary>
/// Ошибка отсутствия сущности.
/// </summary>
public class NotFoundException : Exception
{
    public string Id { get; }
    
    public NotFoundException(string id)
    {
        Id = id;
    }

    protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public NotFoundException(string? message, string id) : base(message)
    {
        Id = id;
    }

    public NotFoundException(string? message, Exception? innerException, string id) : base(message, innerException)
    {
        Id = id;
    }
}