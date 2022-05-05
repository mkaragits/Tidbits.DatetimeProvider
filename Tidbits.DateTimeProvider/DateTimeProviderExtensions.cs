using System;
using System.Collections.Generic;
using System.Text;

namespace Tidbits.DateTimeProvider
{
    public static class DateTimeProviderExtensions
    {
        public static Random Rng = new Random();

        /// <summary>
        /// Returns a new Timespan which is the original +- a random amount between [0, uncertaintyFactor].
        /// Uses DateTimeProviderExtensions.Rng as randomness provider.
        /// </summary>
        /// <param name="tSpan">Timespan to be modified</param>
        /// <param name="uncertaintyFactor">outside limit of the randomness added or subtracted from Timespan</param>
        /// <returns>a new Timespan with random variation</returns>
        public static TimeSpan GiveOrTake(this TimeSpan tSpan, TimeSpan uncertaintyFactor)
        {
            var swingTicks = uncertaintyFactor.TotalSeconds;
            var deviation = (Rng.NextDouble() * swingTicks * 2) - swingTicks;
            return TimeSpan.FromSeconds(tSpan.TotalSeconds + deviation);
        }
    }
}
