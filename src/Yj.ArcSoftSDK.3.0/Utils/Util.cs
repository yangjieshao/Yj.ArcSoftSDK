using System;

namespace Yj.ArcSoftSDK.Utils
{
    /// <summary>
    /// </summary>
    internal static class Util
    {
        /// <summary>
        /// </summary>
        public static long ToTimestamp(this DateTime dateTime)
        {
            return (long)(dateTime - TimeStampStartTime2).TotalMilliseconds;
        }

        /// <summary>
        /// </summary>
        public static DateTime ToDateTime(this long timestamp)
        {
            return TimeStampStartTime2.AddMilliseconds(timestamp);
        }

        internal static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1);
        internal static readonly TimeSpan UtcOffset = TimeZoneInfo.Local.GetUtcOffset(UnixEpoch);

        private static readonly DateTime TimeStampStartTime2 = UnixEpoch.AddMilliseconds(UtcOffset.TotalMilliseconds);
    }
}