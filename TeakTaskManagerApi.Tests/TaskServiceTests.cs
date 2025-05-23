using Moq;
using Services.Contracts;

namespace TeamTaskManagerApi.Tests
{
    internal class TaskServiceTests
    {
        private readonly Mock<IServiceManager> _mockService;

        public TaskServiceTests()
        {
            _mockService = new Mock<IServiceManager>();
        }
    }
}
