using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public sealed class Team : EntityBase
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
