namespace Shared.DTO
{
    public abstract record TaskDtoBase
    {
        /// <summary>
        /// Task's title
        /// </summary>
        public string TaskTitle { get; init; } = string.Empty;
        /// <summary>
        /// Task's description
        /// </summary>
        public string Description { get; init; } = string.Empty;
        /// <summary>
        /// The id of the user the task should be assigned to
        /// </summary>
        public string AssignTo { get; init; } = string.Empty;
        /// <summary>
        /// Date Time the Task will be due for completion
        /// </summary>
        public DateTime DueOn { get; init; }
    }
}
