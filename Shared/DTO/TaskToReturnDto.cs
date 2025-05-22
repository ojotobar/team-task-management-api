using Entities.Enums;

namespace Shared.DTO
{
    public record TaskToReturnDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = string.Empty;
        public UserToReturnDto? CreatedByUser { get; set; }
        public UserToReturnDto? AssignedToUser { get; set; }
        public TeamToReturnDto? Team { get; set; }
    }

    public record LeanTaskToReturnDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public record TaskStatusDto(TaskItemStatus Status);
}
