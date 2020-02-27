using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Macrocosm.Tool
{
    /// <summary>
    /// 日期转换相关
    /// </summary>
    public static class TimeHelper
    {
        public static DateTime UnixToLacalTime(this int span)
        {
            var date = new DateTime(621355968000000000 + (long)span * (long)10000000, DateTimeKind.Utc);
            return date.ToLocalTime();
        }
        public static long GetTimeStamp(this DateTime t)
        {
            long ts = (t.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            return ts;
        }

    }
}
