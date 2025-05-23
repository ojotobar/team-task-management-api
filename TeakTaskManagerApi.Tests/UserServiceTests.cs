using Entities.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.Contracts;
using Shared.DTO;
using System;
using System.Threading.Tasks;
using TeamTaskManager.Presentation.Controllers.v1;
using Xunit;

namespace TeamTaskManagerApi.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IServiceManager> _mockService;

        public UserServiceTests()
        {
            _mockService = new Mock<IServiceManager>();
        }

        [Fact]
        public async Task GetLoggedInUserDetails_Returns_Expected_Result()
        {
            //Arrange
            var userId = Guid.NewGuid();

            _mockService.Setup(x => x.User.GetLoggedInUserInfo())
                .ReturnsAsync(new OkResponse<UserToReturnDto>(new UserToReturnDto
                {
                    Id = userId.ToString(),
                    Name = "Test User"
                }));

            var userController = new UserController(_mockService.Object);

            //Act
            var result = await userController.GetLoggedInUserDetails();

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
