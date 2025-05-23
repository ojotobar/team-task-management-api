using Entities.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.Contracts;
using Services.Validations;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamTaskManager.Presentation.Controllers.v1;
using Xunit;

namespace TeamTaskManagerApi.Tests
{
    public class TeamServiceTests
    {
        private readonly Mock<IServiceManager> _mockService;

        public static List<object[]> InvitationIdTestCases =>
            new List<object[]>
            {
                //Valid ids
                new object[] { new List<string> { Guid.NewGuid().ToString(), "a4ec1786-5745-494e-9083-0a5f9bbc872c" }, true },
                //Invalid string ids
                new object[] { new List<string> { "", "xxxx-xxxx-xxxxxxxx-xxxx-xxxxxxxx" }, false },
                //Null
                new object[] { null!, false },
                //Empty list
                new object[] { new List<string>(), false }
            };

        public TeamServiceTests()
        {
            _mockService = new Mock<IServiceManager>();
        }

        [Theory]
        [InlineData("Team A", true)]
        [InlineData("", false)] //Invalid input
        public async Task CreateTeam_Returns_Expected_Results(string teamName, bool expectedSuccess)
        {
            //Arrange
            var teamRequest = new CreateTeamDto
            {
                TeamName = teamName
            };

            _mockService.Setup(x => x.Team.Create(teamRequest))
                .ReturnsAsync(expectedSuccess ? new OkResponse<TeamToReturnDto>(new TeamToReturnDto()) : new BadRequestResponse("Invalid inputs"));

            var authController = new TeamController(_mockService.Object);

            //Act
            var result = await authController.Create(teamRequest);

            //Assert
            if (expectedSuccess)
            {
                Assert.IsType<OkObjectResult>(result);
            }
            else
            {
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        [Theory]
        [MemberData(nameof(InvitationIdTestCases))]
        public async Task Team_Invitation_Returns_Expected_Results(List<string> userIds, bool isValid)
        {
            //Arrange
            var teamId = Guid.NewGuid();
            var payload = new TeamInvitaionDto
            {
                UserIds = userIds
            };

            var validationResult = new TeamInvitationValidator().Validate(payload);
            ApiResponseBase returns = validationResult.IsValid ? 
                new OkResponse<string>("Users invited") : new BadRequestResponse("Invalid input");
            

            _mockService.Setup(x => x.Team.InviteUsers(teamId, payload))
                .ReturnsAsync(returns);

            var teamController = new TeamController(_mockService.Object);

            //Act
            var result = await teamController.Invite(teamId, payload);

            //Assert
            Assert.Equal(isValid, validationResult.IsValid);

            if (validationResult.IsValid)
            {
                Assert.IsType<OkObjectResult>(result);
            }
            else
            {
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        [Theory]
        [InlineData("a4ec1786-5745-494e-9083-0a5f9bbc872c", "Task A", "Description A", "a4ec1786-5745-494e-9083-0a5f9bbc872c", "9999-12-31T16:28:10.548Z", true)]
        [InlineData("a4ec1786-5745-494e-9083-0a5f9bbc872c", "", "Description B", "a4ec1786-5745-494e-9083-0a5f9bbc872c", "9999-12-31T16:28:10.548Z", false)]//Invalid title
        [InlineData("a4ec1786-5745-494e-9083-0a5f9bbc872c", "Task C", "", "a4ec1786-5745-494e-9083-0a5f9bbc872c", "9999-12-31T16:28:10.548Z", false)]//Invalid description
        [InlineData("a4ec1786-5745-494e-9083-0a5f9bbc872c", "Task D", "Description D", "xxxx-xxxx-xxxxxxxx-xxxx-xxxxxxxx", "9999-12-31T16:28:10.548Z", false)]//Invalid user id
        [InlineData("a4ec1786-5745-494e-9083-0a5f9bbc872c", "Task E", "Description E", "a4ec1786-5745-494e-9083-0a5f9bbc872c", "", false)]//Invalid date
        [InlineData("xxxx-xxxx-xxxxxxxx-xxxx-xxxxxxxx", "Task E", "Description E", "a4ec1786-5745-494e-9083-0a5f9bbc872c", "9999-12-31T16:28:10.548Z", false)]//Invalid team Id
        public async Task Creating_Team_Tasks_Returns_Expected_Results(string teamIdString, string title, string descritions, string userIdString, string futureDateString, bool expectedSuccess)
        {
            //Arrange
            Guid.TryParse(teamIdString, out var teamId);
            DateTime.TryParse(futureDateString, out var dueDate);
            Guid.TryParse(userIdString, out var userId);
            var payload = new List<TaskCreateDto>
            {
                new TaskCreateDto
                {
                    TaskTitle = title,
                    Description = descritions,
                    AssignTo = userIdString,
                    DueOn = dueDate
                }
            };

            var oneOrBothAreInvalidIds = teamId.Equals(Guid.Empty) || userId.Equals(Guid.Empty);

            ApiResponseBase returns = expectedSuccess ? new OkResponse<List<LeanTaskToReturnDto>>(new List<LeanTaskToReturnDto>()) : 
                oneOrBothAreInvalidIds ? new NotFoundResponse("Not found") : new BadRequestResponse("Invalid Input");


            _mockService.Setup(x => x.Task.CreateManyAsync(teamId, payload))
                .ReturnsAsync(returns);

            var teamController = new TeamController(_mockService.Object);

            //Act
            var result = await teamController.CreateTasks(teamId, payload);

            //Assert
            if (expectedSuccess)
            {
                Assert.IsType<OkObjectResult>(result);
            }
            else if (oneOrBothAreInvalidIds)
            {
                Assert.IsType<NotFoundObjectResult>(result);
            }
            else
            {
                Assert.IsType<BadRequestObjectResult>(result);
            }
        }

        [Fact]
        public async Task Get_Team_Tasks_Returns_Expected_Results()
        {
            //Arrange
            Guid.TryParse("a4ec1786-5745-494e-9083-0a5f9bbc872c", out var teamId);
            var isNotValidTeamId = teamId.Equals(Guid.Empty);

            _mockService.Setup(x => x.Task.GetTeamTasksAsync(teamId))
                .ReturnsAsync(new OkResponse<List<TaskToReturnDto>>(new List<TaskToReturnDto>()));

            var teamController = new TeamController(_mockService.Object);

            //Act
            var actionResult = await teamController.GetTeamTasks(teamId);

            //Assert
            Assert.IsType<OkObjectResult>(actionResult);
        }
    }
}
