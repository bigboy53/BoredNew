using System;

namespace DKD.Framework.Extensions
{
    public class DateTimeExtension
    {
        private static readonly long UinxBase = DateTime.Parse("1970-1-1 00:00:00").Ticks;
        private const long DotNetTimeTick = 10000000;   // C#每秒所占的刻度

        ///<summary>
        /// 把C#时间转为Unix时间
        ///</summary>
        ///<param name="time">C#时间</param>
        ///<returns>转换后的Unix时间</returns>
        public static int DateTimeToUnix(DateTime time)
        {
            return (Int32)((time.ToUniversalTime().Ticks - UinxBase) / DotNetTimeTick);
        }

        ///<summary>
        /// Unix时间转化为C#时间
        ///</summary>
        ///<param name="time">Unix时间</param>
        ///<returns>转化后的C#时间</returns>
        public static DateTime UnixToDateTime(int time)
        {
            try
            {
                long t = time * DotNetTimeTick + UinxBase;
                return new DateTime(t).ToLocalTime();
            }
            catch
            {
                return DateTime.Today;
            }
        }
    }
}
