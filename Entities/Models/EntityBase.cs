namespace Entities.Models
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsDeprecated { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
