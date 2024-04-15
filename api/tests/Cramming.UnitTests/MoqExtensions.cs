namespace Cramming.UnitTests
{
    public static class MoqExtensions
    {
        public static void VerifyLog<T>(
            this Mock<ILogger<T>> loggerMock,
            LogLevel expectedLogLevel,
            string expectedMessage,
            params KeyValuePair<string, object>[] expectedValues)
        {
            loggerMock.Verify(
                mock => mock.Log(
                    expectedLogLevel,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((state, _) => MatchesLogValues(state, expectedMessage, expectedValues)),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
                Times.Once
            );
        }

        private static bool MatchesLogValues(object state, string expectedMessage, params KeyValuePair<string, object>[] expectedValues)
        {
            const string messageKeyName = "{OriginalFormat}";

            var loggedValues = (IReadOnlyList<KeyValuePair<string, object>>)state;

            return loggedValues
                .Any(loggedValue => loggedValue.Key == messageKeyName && loggedValue.Value.ToString() == expectedMessage) &&
                 Array.TrueForAll(expectedValues, expectedValue =>
                    loggedValues.Any(loggedValue => loggedValue.Key == expectedValue.Key && loggedValue.Value == expectedValue.Value));
        }
    }
}
