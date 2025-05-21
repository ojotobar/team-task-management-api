using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repositories;
using TeamTaskManagerApi.Configurations;

namespace TeamTaskManagerApi.ContextFactory
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            var config = LogConfigurations.GetConfigurations(env);
            var connectionString = config.GetConnectionString("Default");

            var builder = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionString, s => s.MigrationsAssembly("TeamTaskManagerApi"));

            return new AppDbContext(builder.Options);
        }
    }
}
