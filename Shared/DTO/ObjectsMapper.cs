using Entities.Models;

namespace Shared.DTO
{
    public static class ObjectsMapper
    {
        public static User Map(this RegistrationDto registrationDto) =>
            new()
            {
                Name = registrationDto.Name,
                Email = registrationDto.Email,
                UserName = registrationDto.Email,
                EmailConfirmed = true
            };
    }
}
