namespace Entities.Models
{
    public class AppUser
    {
        public ICollection<TaskItem> CreatedTasks { get; set; } = new List<TaskItem>();
        public ICollection<TaskItem> AssignedTasks { get; set; } = new List<TaskItem>();
        public ICollection<TeamUser> TeamUsers { get; set; } = new List<TeamUser>();
    }
}
