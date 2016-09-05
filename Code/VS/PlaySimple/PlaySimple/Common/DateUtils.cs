using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaySimple.Common
{
    // TODO add to doc
    public class DateUtils
    {
        public static int GetAge(DateTime dateTime)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dateTime.Year;

            if (dateTime > today.AddYears(-age))
                age--;

            return age;
        }

        public static IList<DateTime> PossibleDateOrders(DateTime dateTime)
        {
            IList<DateTime> possibleHour = new List<DateTime>()
            {
                new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 16, 0, 0),
                new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 18, 0, 0),
                new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 20, 0, 0),
            };

            return possibleHour;
        }
    }
}