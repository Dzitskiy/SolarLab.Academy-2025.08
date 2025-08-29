using System.ComponentModel.DataAnnotations;

namespace Articles.Contracts.Articles;

public class CreateArticleDto
{
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public string UserName { get; set; }
}