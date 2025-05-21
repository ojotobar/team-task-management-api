using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories.Configurations;

namespace Repositories
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<Team>? Teams { get; set; }
        public DbSet<TeamUser>? TeamUsers { get; set; }
        public DbSet<TaskItem>? Tasks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureUser()
                .ConfigureTask()
                .ConfigureTeamUser()
                .ApplyConfiguration(new RoleConfiguration());
        }
    }
}
