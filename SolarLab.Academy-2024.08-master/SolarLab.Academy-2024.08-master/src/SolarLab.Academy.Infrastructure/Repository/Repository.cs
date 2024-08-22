using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.Infrastructure.Repository
{
    /// <inheritdoc />
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private BoardDbContext _dbContext;
        private DbSet<TEntity> Entity;
        public Repository(BoardDbContext dbContext)
        {
            _dbContext = dbContext;
            Entity = _dbContext.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAll()
        {
            return  await Entity.ToListAsync();
        }

        //TODO
    }
}
