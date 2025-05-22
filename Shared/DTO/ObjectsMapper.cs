using Entities.Models;
using Shared.Extrensions;

namespace Shared.DTO
{
    public static class ObjectsMapper
    {
        public static TaskItem Map(this TaskItem task, TaskUpdateDto dto)
        {
            task.Title = dto.TaskTitle;
            task.Description = dto.Description;
            task.DueDate = dto.DueOn;
            task.AssignedToUserId = dto.AssignTo;
            return task;
        }
        public static TaskItem Map(this TaskDtoBase task, string userId, Guid teamId) =>
            new()
            {
                Title = task.TaskTitle,
                Description = task.Description,
                AssignedToUserId = task.AssignTo,
                CreatedByUserId = userId,
                DueDate = task.DueOn,
                TeamId = teamId
            };

        public static LeanTaskToReturnDto Map(this TaskItem task) =>
            new ()
            {
                Id = task.Id,
                Title = task.Title,
                Description= task.Description,
                CreatedAt = task.CreatedAt,
                DueDate = task.DueDate,
                Status = task.Status.GetDescription()
            };

        public static TaskToReturnDto MapTo(this TaskItem task)
        {
            return new TaskToReturnDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status.GetDescription(),
                CreatedAt = task.CreatedAt,
                DueDate = task.DueDate,
                Team = task.Team != null ? new TeamToReturnDto
                {
                    Id = task.Team.Id,
                    Name = task.Team.Name

                } : null,
                AssignedToUser = task.AssignedToUser != null ? new UserToReturnDto
                {
                    Id = task.AssignedToUser.Id,
                    Name = task.AssignedToUser.Name,
                    Email = task.AssignedToUser.Email
                } : null,
                CreatedByUser = task.CreatedByUser != null ? new UserToReturnDto
                {
                    Id = task.CreatedByUser.Id,
                    Name = task.CreatedByUser.Name,
                    Email = task.CreatedByUser.Email
                } : null
            };
        }

        public static List<TaskToReturnDto> Map(this List<TaskItem> tasks)
        {
            var data = new List<TaskToReturnDto>();
            tasks.ForEach(task =>
            {
                data.Add(task.MapTo());
            });

            return data;
        }

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

        public static List<UserToReturnDto> Map(this List<User>? users)
        {
            var mappedUsers = new List<UserToReturnDto>();
            if(users != null)
            {
                mappedUsers.AddRange(users.Select(x => new UserToReturnDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                }));
            }

            return mappedUsers;
        }

        public static List<UserToReturnDto> Map(this IEnumerable<User?> users)
        {
            var mappedUsers = new List<UserToReturnDto>();
            if (users != null)
            {
                foreach(var user in users)
                {
                    if(user != null)
                    {
                        mappedUsers.Add(new UserToReturnDto
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Email = user.Email,
                        });
                    }
                }
            }

            return mappedUsers;
        }

        public static Team Map(this CreateTeamDto teamDto) =>
            new()
            {
                Name = teamDto.TeamName
            };

        public static List<TeamToReturnDto> Map(this List<Team> teams)
        {
            var mappedTeams = new List<TeamToReturnDto>();
            mappedTeams.AddRange(teams.Select(x => new TeamToReturnDto
            {
                Id = x.Id,
                Name = x.Name,
            }));

            return mappedTeams;
        }

        public static List<TeamUser> Map(this Guid teamId, List<User> users)
        {
            var teamUsers = new List<TeamUser>();
            foreach (var user in users)
            {
                teamUsers.Add(new TeamUser { UserId = user.Id, TeamId = teamId });
            }

            return teamUsers;
        }
    }
}
