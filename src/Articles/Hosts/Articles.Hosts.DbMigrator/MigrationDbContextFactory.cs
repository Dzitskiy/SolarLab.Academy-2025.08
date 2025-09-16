using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Articles.Hosts.DbMigrator
{
    public class MigrationDbContextFactory : IDesignTimeDbContextFactory<MigrationDbContext>
    {
        public MigrationDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            var connectionString = configuration.GetConnectionString("ConnectionString");

            var contextBuilder = new DbContextOptionsBuilder<MigrationDbContext>();
            contextBuilder.UseNpgsql(connectionString);
            return new MigrationDbContext(contextBuilder.Options);
        }
    }
}
