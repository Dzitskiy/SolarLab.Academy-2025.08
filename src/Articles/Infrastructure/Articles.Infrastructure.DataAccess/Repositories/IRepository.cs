using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Articles.Infrastructure.DataAccess.Repositories
{
    /// <summary>
    /// Базовый репозиторий.
    /// </summary>
    /// <typeparam name="TEntity">Тип доменной сущности.</typeparam>
    /// <typeparam name="TContext">Тип <see cref="DbContext">контекста данных</see>.</typeparam>
    public interface IRepository<TEntity, TContext> where TEntity : class where TContext : DbContext
    {
        /// <summary>
        /// Получить последовательность элементов.
        /// </summary>
        /// <returns>Объект постройки запросов к последовательности элементов.</returns>
        IQueryable<TEntity> GetAll();
        
        /// <summary>
        /// Добавить сущность.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Обновить сущность.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Получить сущность.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Доменная модель.</returns>
        Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
