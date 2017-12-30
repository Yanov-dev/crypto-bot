using System;

namespace crypto.bot.backend.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime FromUnixTime(long epoch) {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(epoch);
        }

        public static long ToUnixTime(this DateTime time)
        {
            return (long)(time - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }
    }
}