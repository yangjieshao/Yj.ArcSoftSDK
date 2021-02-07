using System;

namespace Yj.ArcSoftSDK._4_0.Utils
{
    /// <summary>
    ///
    /// </summary>
    internal static class Util
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToTimestamp(this DateTime dateTime)
        {
            return (long)(dateTime - TimeStampStartTime2).TotalMilliseconds;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long timestamp)
        {
            return TimeStampStartTime2.AddMilliseconds(timestamp);
        }

        internal static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1);
        internal static readonly TimeSpan UtcOffset = TimeZoneInfo.Local.GetUtcOffset(UnixEpoch);

        private static readonly DateTime TimeStampStartTime2 = UnixEpoch.AddMilliseconds(UtcOffset.TotalMilliseconds);
    }
}