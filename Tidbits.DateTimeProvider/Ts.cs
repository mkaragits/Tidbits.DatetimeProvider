using System;

namespace Tidbits.DateTimeProvider
{
    public static class Ts
    {
        public static readonly TimeSpan OneSec = TimeSpan.FromSeconds(1);
        public static readonly TimeSpan FiveSecs = TimeSpan.FromSeconds(5);
        public static readonly TimeSpan TenSecs = TimeSpan.FromSeconds(10);
        public static readonly TimeSpan ThirtySecs = TimeSpan.FromSeconds(30);
        public static readonly TimeSpan HalfMin = TimeSpan.FromSeconds(30);
        public static readonly TimeSpan OneMin = TimeSpan.FromMinutes(1);
        public static readonly TimeSpan FiveMins = TimeSpan.FromMinutes(5);
        public static readonly TimeSpan TenMins = TimeSpan.FromMinutes(10);
        public static readonly TimeSpan ThirtyMins = TimeSpan.FromMinutes(30);
        public static readonly TimeSpan HalfHour = TimeSpan.FromMinutes(30);
        public static readonly TimeSpan OneHour = TimeSpan.FromHours(1);
        public static readonly TimeSpan SixHours = TimeSpan.FromHours(6);
        public static readonly TimeSpan TwelveHours = TimeSpan.FromHours(12);
        public static readonly TimeSpan HalfDay = TimeSpan.FromHours(12);
        public static readonly TimeSpan OneDay = TimeSpan.FromDays(1);
        public static readonly TimeSpan FiveDays = TimeSpan.FromDays(5);
        public static readonly TimeSpan OneWeek = TimeSpan.FromDays(7);
        public static readonly TimeSpan TwoWeeks = TimeSpan.FromDays(14);
    }
}
