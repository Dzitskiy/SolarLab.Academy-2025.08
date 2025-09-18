using Articles.AppServices.Contexts.Articles.Services;
using Articles.Contracts.Articles;
using Articles.Contracts.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Articles.Hosts.Api.Controllers;

/// <summary>
/// Контроллер для работы со статьями.
/// </summary>
/// <param name="articleService"></param>
[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
public class ArticlesController(IArticleService articleService) : ControllerBase
{
    /// <summary>
    /// Получает статьи по фильтру.
    /// </summary>
    /// <param name="filter">Фильтр.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Коллекция моделей статей.</returns>
    [HttpGet("by-filter")]
    [ProducesResponseType(typeof(IReadOnlyCollection<ArticleDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetArticlesByFilter([FromQuery] ArticleFilterDto filter, CancellationToken cancellationToken)
    {
        var articles = await articleService.GetByFilterAsync(filter, cancellationToken);
        if (articles.Items.Count == 0)
        {
            return NotFound();
        }
        return Ok(articles);
    }

    /// <summary>
    /// Получить статью по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор статьи.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Модель статьи.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ArticleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetArticleById(Guid id, CancellationToken cancellationToken)
    {
        var article = await articleService.GetByIdAsync(id, cancellationToken);
        if (article == null)
        {
            return NotFound();
        }

        return Ok(article);
    }

    /// <summary>
    /// Создаёт статью по модели.
    /// </summary>
    /// <param name="article">Модель создания статьи.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Идентификатор созданной статьи.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateArticle(CreateArticleDto article, CancellationToken cancellationToken)
    {
        var id = await articleService.CreateAsync(article, cancellationToken);
        return CreatedAtAction(nameof(GetArticleById), id.ToString());
    }
    
    /// <summary>
    /// Обновляет статью по модели.
    /// </summary>
    /// <param name="id">Идентификатор существующей статьи.</param>
    /// <param name="request">Модель обновления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Модель обновлённой статьи.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ArticleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateArticle(Guid id, UpdateArticleDto request, CancellationToken cancellationToken)
    {
        var articleDto = await articleService.UpdateAsync(id, request, cancellationToken);
        return Ok(articleDto);
    }
    
    /// <summary>
    /// Удаляет статью.
    /// </summary>
    /// <param name="id">Идентификатор статьи.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ArticleDto), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteArticle(Guid id, CancellationToken cancellationToken)
    {
        await articleService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}