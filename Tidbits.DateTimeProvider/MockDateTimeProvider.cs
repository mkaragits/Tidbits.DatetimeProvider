using System;

namespace Tidbits.DateTimeProvider
{
    /// <summary>
    /// Defines a DateTime implementation that returns the mocked values for use in testing.
    /// </summary>
    public class MockDateTimeProvider : IDateTimeProvider
    {
        /// <summary>
        /// Random number generator for uncertainties. Set seed to specific value to get reproducible results.
        /// </summary>
        public Random Rng { get; set; } = new Random();

        /// <summary>
        /// Gets a System.DateTime object that is set to the current date and time on this computer, expressed as the local time.
        /// </summary>
        /// <returns>
        /// A <see cref="DateTime">System.DateTime</see> that has been previously set to return.
        /// </returns>
        public DateTime Now { get; set; }

        /// <summary>
        /// Gets the current date.
        /// </summary>
        /// <returns>
        /// A <see cref="DateTime">System.DateTime</see> that has been previously set to return.
        /// </returns>
        public DateTime Today { get; set; }

        /// <summary>
        /// Gets a <see cref="DateTime">System.DateTime</see> object that is set to the current date and time on this computer, expressed as the Coordinated Universal Time (UTC).
        /// </summary>
        /// <returns>
        /// A <see cref="DateTime">System.DateTime</see> that has been previously set to return.
        /// </returns>
        public DateTime UtcNow { get; set; }


        /// <summary>
        /// Advance Now, UtcNow and Today by a TimeSpan.
        /// </summary>
        /// <param name="timeSpan">Duration to advance time by.</param>
        public void AdvanceTimeBy(TimeSpan timeSpan)
        {
            Now += timeSpan;
            UtcNow += timeSpan;
            Today = Now - Now.TimeOfDay;
        }

        /// <summary>
        /// Advance Now, UtcNow and Today by a TimeSpan with some fuzziness.
        /// Ex: if timeSpan is 1 minute and uncertainty is 10 seconds, the result will be somewhere between 50 and 70 seconds
        /// </summary>
        /// <param name="timeSpan">Duration to advance time by.</param>
        /// <param name="uncertainty">Amount of uncertainty added will be between [-uncertainty; uncertainty]</param>
        public void AdvanceTimeBy(TimeSpan timeSpan, TimeSpan uncertainty)
        {
            AdvanceTimeBy(GiveOrTake(timeSpan, uncertainty));
        }

        /// <summary>
        /// Rewind Now, UtcNow and Today by a TimeSpan.
        /// </summary>
        /// <param name="timeSpan">Duration to rewind time by.</param>
        public void RewindTimeBy(TimeSpan timeSpan)
        {
            Now -= timeSpan;
            UtcNow -= timeSpan;
            Today = Now - Now.TimeOfDay;
        }

        /// <summary>
        /// Rewind Now, UtcNow and Today by a TimeSpan with some fuzziness.
        /// Ex: if timeSpan is 1 minute and uncertainty is 10 seconds, the result will be somewhere between 50 and 70 seconds
        /// </summary>
        /// <param name="timeSpan">Duration to rewind time by.</param>
        /// <param name="uncertainty">Amount of uncertainty added will be between [-uncertainty; uncertainty]</param>
        public void RewindTimeBy(TimeSpan timeSpan, TimeSpan uncertainty)
        {
            RewindTimeBy(GiveOrTake(timeSpan, uncertainty));
        }

        private TimeSpan GiveOrTake(TimeSpan tSpan, TimeSpan uncertaintyFactor)
        {
            var swingTicks = uncertaintyFactor.TotalSeconds;
            var deviation = (Rng.NextDouble() * swingTicks * 2) - swingTicks;
            return TimeSpan.FromSeconds(tSpan.TotalSeconds + deviation);
        }


        public void Set(DateTime startingDateTime, TimeZoneInfo timeZoneInfo = null)
        {
            switch (startingDateTime.Kind)
            {
                case DateTimeKind.Unspecified:
                case DateTimeKind.Local:
                    Now = startingDateTime;
                    UtcNow = TimeZoneInfo.ConvertTimeToUtc(startingDateTime, timeZoneInfo ?? TimeZoneInfo.Local);
                    Today = startingDateTime - startingDateTime.TimeOfDay;
                    break;
                case DateTimeKind.Utc:
                    Now = TimeZoneInfo.ConvertTimeFromUtc(startingDateTime, timeZoneInfo ?? TimeZoneInfo.Local);
                    UtcNow = startingDateTime;
                    Today = Now - Now.TimeOfDay;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(startingDateTime), "Somehow got a DateTimeKind that should not exist.");
            }
        }

        public MockDateTimeProvider(DateTime startingDateTime, TimeZoneInfo timeZoneInfo = null)
        {
            Set(startingDateTime, timeZoneInfo);
        }
    }
}
