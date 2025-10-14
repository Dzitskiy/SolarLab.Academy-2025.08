namespace Articles.Contracts.Articles;

public class ArticleDto
{
    public Guid Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public string UserName { get; set; }
}