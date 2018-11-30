using System;
using System.Web;
using System.IO;
using System.Collections.Specialized;

namespace WebApplication1.Utils
{
    public class ServiceUtil
    {
        public static HttpRequest Request // ! Удалить
        {
            get
            {
                return HttpContext.Current.Request;
            }
        }

        public static NameValueCollection Form // ! предпочтение композиции наследованию. ПОместить в какой-то класс, ссылку на него написать в ClientController  и других
        {
            get
            {
                return HttpContext.Current.Request.Form;
            }
        }

        public static DataContext Context { get { return new DataContext(); } }

        public static byte[] GetBytesFromStream(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static long GetLong(string value)
        {
            return Convert.ToInt64(value);
        }

        public static DateTime UnixTimestampToDateTime(long unixTime)
        {
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            long unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
            return new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc);
        }

        public static long DateTimeToUnixTimestamp(DateTime dateTime)
        {
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            long unixTimeStampInTicks = (dateTime.ToUniversalTime() - unixStart).Ticks;
            return unixTimeStampInTicks / TimeSpan.TicksPerSecond;
        }

        public static bool IsJSONString(string str)
        {
            return str.StartsWith("{") && str.EndsWith("}");
        }
    }
}