using System;
using System.Collections.Generic;
using System.Text;

namespace OrderItemsReserver.Helpers
{
    public static class DateTimeExtension
    {
        public static readonly DateTime UnixStart = new DateTime(1970, 1, 1);
        public static int ToUnixTimestamp(this DateTime dateTime)
        {
            return (int)dateTime.Subtract(UnixStart).TotalSeconds;
        }
    }
}
