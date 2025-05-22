using System.ComponentModel;

namespace Entities.Enums
{
    public enum TaskItemStatus
    {
        [Description("Pending")]
        Pending,
        [Description("In Progress")]
        InProgress,
        [Description("Completed")]
        Completed
    }
}
