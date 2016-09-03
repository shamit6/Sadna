using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaySimple.Common
{
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
    }
}