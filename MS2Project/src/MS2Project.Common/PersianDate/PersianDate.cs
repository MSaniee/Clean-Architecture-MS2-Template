using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace $ext_safeprojectname$.Common.PersianDate
{
    public static class PersianDate
    {
        /// <summary>
        /// اول و آخر ماه شمسی جاری را به میلادی برمیگرداند
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> GetStartAndEndMonth(this DateTime date)
        {
            var pc = new PersianCalendar();
            var year = pc.GetYear(date);
            var month = pc.GetMonth(date);

            var days = pc.GetDaysInMonth(year, month);

            yield return pc.ToDateTime(year, month, 1, 0, 0, 0, 0);
            yield return pc.ToDateTime(year, month, days, 0, 0, 0, 0);
        }

        /// <summary>
        /// نمایش کامل تاریخ با عنوان روزهای هفته
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToPersianDayOfWeekString(this DateTime dateTime)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            string year = persianCalendar.GetYear(dateTime).ToString();
            string month = persianCalendar.GetMonth(dateTime).ToString();
            string dayofmonth = persianCalendar.GetDayOfMonth(dateTime).ToString();
            string week = persianCalendar.GetDayOfWeek(dateTime).ToString();
            switch (week)
            {
                case "Saturday":
                    week = "شنبه";
                    break;

                case "Sunday":
                    week = "یکشنبه";
                    break;

                case "Monday":
                    week = "دوشنبه";
                    break;

                case "Tuesday":
                    week = "سه‌شنبه";
                    break;

                case "Wednesday":
                    week = "چهارشنبه";
                    break;

                case "Thursday":
                    week = "پنج‌شنبه";
                    break;

                case "Friday":
                    week = "جمعه";
                    break;
            }
            switch (month)
            {
                case "1":
                    month = "فروردین";
                    break;

                case "2":
                    month = "اردیبهشت";
                    break;

                case "3":
                    month = "خرداد";
                    break;

                case "4":
                    month = "تیر";
                    break;

                case "5":
                    month = "مرداد";
                    break;

                case "6":
                    month = "شهریور";
                    break;

                case "7":
                    month = "مهر";
                    break;

                case "8":
                    month = "آبان";
                    break;

                case "9":
                    month = "آذر";
                    break;

                case "10":
                    month = "دی";
                    break;

                case "11":
                    month = "بهمن";
                    break;

                case "12":
                    month = "اسفند";
                    break;
            }
            string today = week + " " + dayofmonth + " " + month + " " + year;
            return today;
        }

        //////////////////////////////////DateTimeOffset/////////////////////
        public static string ToPersianDayOfWeekString(this DateTimeOffset dateTimeOffset)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            string year = persianCalendar.GetYear(dateTimeOffset.DateTime).ToString();
            string month = persianCalendar.GetMonth(dateTimeOffset.DateTime).ToString();
            string dayofmonth = persianCalendar.GetDayOfMonth(dateTimeOffset.DateTime).ToString();
            string week = persianCalendar.GetDayOfWeek(dateTimeOffset.DateTime).ToString();
            switch (week)
            {
                case "Saturday":
                    week = "شنبه";
                    break;

                case "Sunday":
                    week = "یکشنبه";
                    break;

                case "Monday":
                    week = "دوشنبه";
                    break;

                case "Tuesday":
                    week = "سه‌شنبه";
                    break;

                case "Wednesday":
                    week = "چهارشنبه";
                    break;

                case "Thursday":
                    week = "پنج‌شنبه";
                    break;

                case "Friday":
                    week = "جمعه";
                    break;
            }
            switch (month)
            {
                case "1":
                    month = "فروردین";
                    break;

                case "2":
                    month = "اردیبهشت";
                    break;

                case "3":
                    month = "خرداد";
                    break;

                case "4":
                    month = "تیر";
                    break;

                case "5":
                    month = "مرداد";
                    break;

                case "6":
                    month = "شهریور";
                    break;

                case "7":
                    month = "مهر";
                    break;

                case "8":
                    month = "آبان";
                    break;

                case "9":
                    month = "آذر";
                    break;

                case "10":
                    month = "دی";
                    break;

                case "11":
                    month = "بهمن";
                    break;

                case "12":
                    month = "اسفند";
                    break;
            }
            string today = week + " " + dayofmonth + " " + month + " " + year;
            return today;
        }

        /// <summary>
        /// نمایش کامل تاریخ به شمسی
        /// </summary>
        /// <param name="dt">تاریخ</param>
        /// <param name="longstring">نمایش ساعت ؟</param>
        /// <returns></returns>
        public static string ToPersianDateString(this DateTime dt,
            bool includeHourMinute = false,
            bool rtlTemplate = false,
            string separator = "/",
            bool includeSecond = false)
        {
            var year = dt.Year;
            var month = dt.Month;
            var day = dt.Day;
            var persianCalendar = new PersianCalendar();
            var pYear = persianCalendar.GetYear(new DateTime(year, month, day, new GregorianCalendar()));
            var pMonth = persianCalendar.GetMonth(new DateTime(year, month, day, new GregorianCalendar()));
            var pDay = persianCalendar.GetDayOfMonth(new DateTime(year, month, day, new GregorianCalendar()));

            var dateTime = $"{pYear}{separator}{pMonth.ToString("00", CultureInfo.InvariantCulture)}{separator}{pDay.ToString("00", CultureInfo.InvariantCulture)}";

            if (includeHourMinute)
            {
                var time = $"{dt.Hour.ToString("00")}:{dt.Minute.ToString("00")}";

                if (includeSecond)
                    time += $":{dt.Second.ToString("00")}";

                //برای قالب‌هایی که راست به چپ هستند درست نمایش داده می‌شود
                if (rtlTemplate)
                    dateTime = time + " " + dateTime;
                else
                    dateTime += " " + time;
            }

            return dateTime;
        }

        public static DateTime PersianDateToGregorianDate(string pDate)
        {
            var dateParts = pDate.Split(new[] { '/' }).Select(d => int.Parse(d)).ToArray();
            var hour = 0;
            var min = 0;
            var seconds = 0;
            return new DateTime(dateParts[0], dateParts[1], dateParts[2],
                                hour, min, seconds, new PersianCalendar());
        }
    }
}