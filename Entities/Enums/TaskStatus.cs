using System.ComponentModel;

namespace Entities.Enums
{
    public enum TaskStatus
    {
        [Description("Pending")]
        Pending,
        [Description("In Progress")]
        InProgress,
        [Description("Completed")]
        Completed
    }
}
