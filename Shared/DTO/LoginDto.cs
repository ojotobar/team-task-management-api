using System.ComponentModel.DataAnnotations;

namespace Shared.DTO
{
    public record LoginDto
    {
        /// <summary>
        /// User's Email Address
        /// </summary>
        public string Email { get; init; } = string.Empty;
        /// <summary>
        /// User's Password.
        /// </summary>
        public string Password { get; init; } = string.Empty;
    }
}
