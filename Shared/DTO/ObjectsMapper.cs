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

        public static UserToReturnDto Map(this User user) =>
            new()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };

        public static Team Map(this CreateTeamDto teamDto) =>
            new()
            {
                Name = teamDto.TeamName
            };
    }
}
