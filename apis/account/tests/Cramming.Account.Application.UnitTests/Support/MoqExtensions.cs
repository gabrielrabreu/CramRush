using Microsoft.Extensions.Logging;
using Moq;

namespace Cramming.Account.Application.UnitTests.Support
{
    public static class MoqExtensions
    {
        public static void VerifyLogInformation<T>(this Mock<ILogger<T>> loggerMock, string expectedLog)
        {
            loggerMock.Verify(
                x => x.Log(LogLevel.Information,
                           It.IsAny<EventId>(),
                           It.Is<It.IsAnyType>((v, t) => v.ToString() == expectedLog),
                           It.IsAny<Exception>(),
                           (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
                Times.Once
            );
        }
    }
}
