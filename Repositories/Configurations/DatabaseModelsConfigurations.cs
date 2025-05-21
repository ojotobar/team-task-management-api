using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Configurations
{
    public static class DatabaseModelsConfigurations
    {
        public static ModelBuilder ConfigureUser(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(user =>
            {
                user.HasMany(u => u.CreatedTasks)
                    .WithOne(t => t.CreatedByUser)
                    .HasForeignKey(t => t.CreatedByUserId)
                    .OnDelete(DeleteBehavior.Cascade);

                user.HasMany(u => u.AssignedTasks)
                    .WithOne(t => t.AssignedToUser)
                    .HasForeignKey(t => t.AssignedToUserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            return modelBuilder;
        }

        public static ModelBuilder ConfigureTask(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>(task =>
            {
                task.HasOne(t => t.Team)
                    .WithMany(t => t.Tasks)
                    .HasForeignKey(t => t.TeamId);

                task.HasOne(t => t.CreatedByUser)
                    .WithMany(t => t.CreatedTasks)
                    .HasForeignKey(t => t.CreatedByUserId)
                    .OnDelete(DeleteBehavior.NoAction);

                task.HasOne(t => t.AssignedToUser)
                    .WithMany(t => t.AssignedTasks)
                    .HasForeignKey(t => t.AssignedToUserId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            return modelBuilder;
        }

        public static ModelBuilder ConfigureTeamUser(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamUser>(teamUser =>
            {
                teamUser.HasKey(tu => new { tu.TeamId, tu.UserId });

                teamUser.HasOne(tu => tu.Team)
                    .WithMany(tu => tu.TeamUsers)
                    .HasForeignKey(tu => tu.TeamId);

                teamUser.HasOne(tu => tu.User)
                    .WithMany(tu => tu.TeamUsers)
                    .HasForeignKey(tu => tu.UserId);
            });

            return modelBuilder;
        }
    }
}
