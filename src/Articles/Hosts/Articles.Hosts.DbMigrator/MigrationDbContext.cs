using Articles.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Articles.Hosts.DbMigrator
{
    /// <summary>
    /// Контекст БД для мигратора.
    /// </summary>
    public class MigrationDbContext : ApplicationDbContext
    {
        public MigrationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
