using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tidbits.DateTimeProvider;
using Xunit;
using FluentAssertions;
using Xunit.Abstractions;

namespace DateTimeProviderTests
{
    public class UncertaintyTests
    {
        private readonly ITestOutputHelper _output;

        public UncertaintyTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [Repeat(10)]
        public void AdvanceTime_result_isWithinParameters(int minutes)
        {
            // Arrange
            var time = DateTime.Now;
            var timespan = TimeSpan.FromMinutes(minutes);
            var sut = new MockDateTimeProvider(time);
            var lowerBound = time + Ts.OneHour - timespan;
            var upperBound = time + Ts.OneHour + timespan;

            // Act
            sut.AdvanceTimeBy(Ts.OneHour, timespan);

            // Assert
            sut.Now.Should().BeAfter(lowerBound)
                .And.BeBefore(upperBound);
            
            _output.WriteLine($"{lowerBound} < {sut.Now} < {upperBound}");
        }

        [Theory]
        [Repeat(10)]
        public void RewindTime_result_isWithinParameters(int minutes)
        {
            // Arrange
            var time = DateTime.Now;
            var timespan = TimeSpan.FromMinutes(minutes);
            var sut = new MockDateTimeProvider(time);
            var lowerBound = time - Ts.OneHour - timespan;
            var upperBound = time - Ts.OneHour + timespan;

            // Act
            sut.RewindTimeBy(Ts.OneHour, timespan);

            // Assert
            sut.Now.Should().BeAfter(lowerBound)
                .And.BeBefore(upperBound);
            _output.WriteLine($"{lowerBound} < {sut.Now} < {upperBound}");
        }

    }
}
