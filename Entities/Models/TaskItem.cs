﻿using Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public sealed class TaskItem : EntityBase
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        [Required]
        public TaskItemStatus Status { get; set; } = TaskItemStatus.Pending;

        public string CreatedByUserId { get; set; } = string.Empty;
        //Navigation Property
        public User? CreatedByUser { get; set; }

        public string AssignedToUserId { get; set; } = string.Empty;
        //Navigation Property
        public User? AssignedToUser { get; set; }

        public Guid TeamId { get; set; }
        //Navigation Property
        public Team? Team { get; set; }
    }
}
