using Articles.Domain.Base;

namespace Articles.Domain.Entities;

public class User : EntityBase
{
    public string Name { get; set; }
    
    public ICollection<Article> Articles { get; set; }
}