using Entities.Enums;
using Entities.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.Contracts;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamTaskManager.Presentation.Controllers.v1;
using Xunit;

namespace TeamTaskManagerApi.Tests
{
    public class TaskServiceTests
    {
        private readonly Mock<IServiceManager> _mockService;

        public TaskServiceTests()
        {
            _mockService = new Mock<IServiceManager>();
        }

        [Theory]
        [InlineData("a4ec1786-5745-494e-9083-0a5f9bbc872c", "Task A", "Description A", "a4ec1786-5745-494e-9083-0a5f9bbc872c", "9999-12-31T16:28:10.548Z", true)]
        [InlineData("a4ec1786-5745-494e-9083-0a5f9bbc872c", "", "Description B", "a4ec1786-5745-494e-9083-0a5f9bbc872c", "9999-12-31T16:28:10.548Z", false)]//Invalid title
        [InlineData("a4ec1786-5745-494e-9083-0a5f9bbc872c", "Task C", "", "a4ec1786-5745-494e-9083-0a5f9bbc872c", "9999-12-31T16:28:10.548Z", false)]//Invalid description
        [InlineData("a4ec1786-5745-494e-9083-0a5f9bbc872c", "Task D", "Description D", "xxxx-xxxx-xxxxxxxx-xxxx-xxxxxxxx", "9999-12-31T16:28:10.548Z", false)]//Invalid user id
        [InlineData("a4ec1786-5745-494e-9083-0a5f9bbc872c", "Task E", "Description E", "a4ec1786-5745-494e-9083-0a5f9bbc872c", "", false)]//Invalid date
        [InlineData("xxxx-xxxx-xxxxxxxx-xxxx-xxxxxxxx", "Task E", "Description E", "a4ec1786-5745-494e-9083-0a5f9bbc872c", "9999-12-31T16:28:10.548Z", false)]//Invalid task Id
        public async Task Updating_Team_Tasks_Returns_Expected_Results(string taskIdString, string title, string descritions, string userIdString, string futureDateString, bool expectedSuccess)
        {
            //Arrange
            Guid.TryParse(taskIdString, out var taskId);
            DateTime.TryParse(futureDateString, out var dueDate);
            Guid.TryParse(userIdString, out var userId);
            var payload = new TaskUpdateDto
            {
                TaskTitle = title,
                Description = descritions,
                AssignTo = userIdString,
                DueOn = dueDate
            };

            var oneOrBothAreInvalidIds = taskId.Equals(Guid.Empty) || userId.Equals(Guid.Empty);

            ApiResponseBase returns = expectedSuccess ? new OkResponse<LeanTaskToReturnDto>(new LeanTaskToReturnDto()) :
                oneOrBothAreInvalidIds ? new NotFoundResponse("Not found") : new BadRequestResponse("Invalid Input");


            _mockService.Setup(x => x.Task.Update(taskId, payload))
                .ReturnsAsync(returns);

            var teamController = new TaskController(_mockService.Object);

            //Act
            var result = await teamController.Update(taskId, payload);

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

        [Theory]
        [InlineData("a4ec1786-5745-494e-9083-0a5f9bbc872c", true)]
        [InlineData("xxxx-xxxx-xxxxxxxx-xxxx-xxxxxxxx", false)]//Invalid id
        public async Task Delete_Team_Tasks_Returns_Expected_Results(string taskIdString, bool expectedSuccess)
        {
            //Arrange
            Guid.TryParse(taskIdString, out var taskId);
            var notAValidTaskId = taskId.Equals(Guid.Empty);

            ApiResponseBase expectedResponse = notAValidTaskId ?
                new NotFoundResponse("Task not found") :
                new OkResponse<string>("Task deleted");

            _mockService.Setup(x => x.Task.Deprecate(taskId))
                .ReturnsAsync(expectedResponse);

            var taskController = new TaskController(_mockService.Object);

            //Act
            var actionResult = await taskController.Deprecate(taskId);

            //Assert
            if (expectedSuccess)
            {
                Assert.IsType<OkObjectResult>(actionResult);
            }
            else
            {
                Assert.IsType<NotFoundObjectResult>(actionResult);
            }
        }

        [Theory]
        [InlineData("a4ec1786-5745-494e-9083-0a5f9bbc872c", 1, true)]
        [InlineData("xxxx-xxxx-xxxxxxxx-xxxx-xxxxxxxx", 2, false)]//Invalid id
        [InlineData("a4ec1786-5745-494e-9083-0a5f9bbc872c", 3, false)]//Invalid status
        public async Task Update_Team_Tasks_Status_Returns_Expected_Results(string taskIdString, int statusInt, bool expectedSuccess)
        {
            //Arrange
            Guid.TryParse(taskIdString, out var taskId);
            
            var validStatus = Enum.IsDefined(typeof(TaskItemStatus), statusInt);
            var notAValidTaskId = taskId.Equals(Guid.Empty);
            var payload = new TaskStatusDto((TaskItemStatus)statusInt);

            ApiResponseBase expectedResponse = expectedSuccess ?
                new OkResponse<LeanTaskToReturnDto>(new LeanTaskToReturnDto()) : notAValidTaskId ?
                new NotFoundResponse("Task not found") : 
                new BadRequestResponse("Task deleted");

            _mockService.Setup(x => x.Task.UpdateStatus(taskId, payload))
                .ReturnsAsync(expectedResponse);

            var taskController = new TaskController(_mockService.Object);

            //Act
            var actionResult = await taskController.UpdateStatus(taskId, payload);

            //Assert
            if (expectedSuccess)
            {
                Assert.IsType<OkObjectResult>(actionResult);
            }
            else if(notAValidTaskId)
            {
                Assert.IsType<NotFoundObjectResult>(actionResult);
            }
            else
            {
                Assert.IsType<BadRequestObjectResult>(actionResult);
            }
        }
    }
}
