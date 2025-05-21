using System.ComponentModel.DataAnnotations;

namespace Shared.DTO
{
    public record LoginDto
    {
        /// <summary>
        /// User's Email Address
        /// </summary>
        [Required(ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; init; } = string.Empty;
        /// <summary>
        /// User's Password.
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; init; } = string.Empty;
    }
}
