using System;
using System.Globalization;
using FluentAssertions;
using Tidbits.DateTimeProvider;
using Xunit;

namespace DateTimeProviderTests
{
    public class MockDateTimeProviderTests
    {
        private readonly TimeZoneInfo _cet = TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time");

        [Fact]
        public void Ctor_shouldInitializeUtcAndToday_whenUnspecifiedKindDateTimeGiven()
        {
            // Arrange
            var localTime = DateTime.Parse("2022-02-22 22:22");
            var utcTime = TimeZoneInfo.ConvertTimeToUtc(localTime, _cet);

            // Act
            var sut = new MockDateTimeProvider(localTime, _cet);

            // Assert
            localTime.Kind.Should().Be(DateTimeKind.Unspecified);
            sut.Now.Should().Be(localTime);
            sut.UtcNow.Should().Be(utcTime);
            sut.Today.Should().Be(localTime - localTime.TimeOfDay);
        }

        [Fact]
        public void Ctor_shouldInitializeUtcAndToday_whenLocalKindDateTimeGiven()
        {
            // Arrange
            var localTime = DateTime.Parse("2022-02-22 22:22Z");
            var tzInfo = TimeZoneInfo.Local;
            var utcTime = TimeZoneInfo.ConvertTimeToUtc(localTime, tzInfo);

            // Act
            var sut = new MockDateTimeProvider(localTime, tzInfo);

            // Assert
            localTime.Kind.Should().Be(DateTimeKind.Local);
            sut.Now.Should().Be(localTime);
            sut.UtcNow.Should().Be(utcTime);
            sut.Today.Should().Be(localTime - localTime.TimeOfDay);
        }

        [Fact]
        public void Ctor_shouldInitializeLocalAndToday_whenUtcKindDateTimeGiven()
        {
            // Arrange
            var utcTime = DateTime.Parse("2022-02-22T22:22:00Z", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal);
            var localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, _cet);

            // Act
            var sut = new MockDateTimeProvider(utcTime, _cet);

            // Assert
            utcTime.Kind.Should().Be(DateTimeKind.Utc);
            sut.Now.Should().Be(localTime);
            sut.UtcNow.Should().Be(utcTime);
            sut.Today.Should().Be(localTime - localTime.TimeOfDay);
        }

        [Fact]
        public void Ctor_shouldUseLocalTimezone_whenOnlyLocalTimeGiven()
        {
            // Arrange
            var localTime = DateTime.Parse("2022-02-22 23:22Z");
            var tzInfo = TimeZoneInfo.Local;
            var utcTime = TimeZoneInfo.ConvertTimeToUtc(localTime, tzInfo);

            // Act
            var sut = new MockDateTimeProvider(localTime);

            // Assert
            localTime.Kind.Should().Be(DateTimeKind.Local);
            sut.Now.Should().Be(localTime);
            sut.UtcNow.Should().Be(utcTime);
            sut.Today.Should().Be(localTime - localTime.TimeOfDay);
        }

        [Fact]
        public void Ctor_shouldUseLocalTimezone_whenOnlyUnspecifiedTimeGiven()
        {
            // Arrange
            var localTime = DateTime.Parse("2022-02-22 23:22");
            var tzInfo = TimeZoneInfo.Local;
            var utcTime = TimeZoneInfo.ConvertTimeToUtc(localTime, tzInfo);

            // Act
            var sut = new MockDateTimeProvider(localTime);

            // Assert
            localTime.Kind.Should().Be(DateTimeKind.Unspecified);
            sut.Now.Should().Be(localTime);
            sut.UtcNow.Should().Be(utcTime);
            sut.Today.Should().Be(localTime - localTime.TimeOfDay);
        }

        [Fact]
        public void Ctor_shouldUseUtcTimezone_whenOnlyUtcTimeGiven()
        {
            // Arrange
            var utcTime = DateTime.Parse("2022-02-22 23:22Z", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal);
            var tzInfo = TimeZoneInfo.Local;
            var localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzInfo);

            // Act
            var sut = new MockDateTimeProvider(utcTime);

            // Assert
            utcTime.Kind.Should().Be(DateTimeKind.Utc);
            sut.Now.Should().Be(localTime);
            sut.UtcNow.Should().Be(utcTime);
            sut.Today.Should().Be(localTime - localTime.TimeOfDay);
        }
    }
}