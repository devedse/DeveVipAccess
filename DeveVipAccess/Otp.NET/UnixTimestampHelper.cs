using System;

namespace OtpNet
{
    public static class UnixTimestampHelper
    {
        /// <summary>
        /// The number of ticks as Measured at Midnight Jan 1st 1970;
        /// </summary>
        private const long unixEpochTicks = 621355968000000000L;

        /// <summary>
        /// A divisor for converting ticks to seconds
        /// </summary>
        private const long ticksToSeconds = 10000000L;

        public static long ConvertToUnixTimeStamp(DateTime timestamp)
        {
            var unixTimestamp = (timestamp.Ticks - unixEpochTicks) / ticksToSeconds;
            return unixTimestamp;
        }

        /// <summary>
        /// Takes a timestamp and calculates a time step
        /// </summary>
        public static long CalculateTimeStepFromTimestamp(DateTime timestamp, int step)
        {
            var unixTimestamp = ConvertToUnixTimeStamp(timestamp);
            var window = unixTimestamp / step;
            return window;
        }

        public static int RemainingSecondsForSpecificTime(DateTime timestamp, int step)
        {
            var unixTimestamp = ConvertToUnixTimeStamp(timestamp);
            return step - (int)(unixTimestamp % step);
        }
    }
}
