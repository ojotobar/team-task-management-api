using Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO
{
    public record RegistrationDto
    {
        /// <summary>
        /// User's Name
        /// </summary>
        public string Name { get; init; } = string.Empty;
        /// <summary>
        /// User's Email Address
        /// </summary>
        public string Email { get; init; } = string.Empty;
        /// <summary>
        /// User's Password. Must be at least 8
        /// characters long, must contain at least, one lowercase,
        /// one uppercase, one digit and one non-alphanumeric
        /// character
        /// </summary>
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// Must match the Password entered
        /// </summary>
        public string ConfirmPassword { get; set; } = string.Empty;
        /// <summary>
        /// 1: Team Admin, 2: Member
        /// </summary>
        public Roles Role { get; set; } = Roles.Member;

    }
}
