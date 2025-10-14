using Microsoft.EntityFrameworkCore;

namespace Articles.Infrastructure.DataAccess.Repositories
{
    /// <inheritdoc />
    public class Repository<TEntity, TContext> : IRepository<TEntity, TContext> where TEntity : class where TContext : DbContext
    {
        protected TContext DbContext;
        protected DbSet<TEntity> DbSet;

        /// <summary>
        /// Инициализирует экземпляр <see cref="Repository{TEntity, TContext}"/>.
        /// </summary>
        public Repository(TContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<TEntity>();
        }

        /// <inheritdoc />
        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await DbSet.AddAsync(entity, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);

            if (entity != null) 
            {
                DbSet.Remove(entity);
                await DbContext.SaveChangesAsync(cancellationToken);
            }
        }

        /// <inheritdoc />
        public IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        /// <inheritdoc />
        public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await DbSet.FindAsync([id], cancellationToken);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            DbSet.Update(entity);
            await DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
