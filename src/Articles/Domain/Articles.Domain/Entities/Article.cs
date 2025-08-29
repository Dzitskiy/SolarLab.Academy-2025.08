using Articles.Domain.Base;

namespace Articles.Domain.Entities;

public class Article : EntityBase
{
    public string Title { get; set; }
    
    public string Description { get; set; }
}