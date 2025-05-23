using Moq;
using Services.Contracts;

namespace TeamTaskManagerApi.Tests
{
    public class TeamServiceTests
    {
        private readonly Mock<IServiceManager> _mockService;

        public TeamServiceTests()
        {
            _mockService = new Mock<IServiceManager>();
        }
    }
}
