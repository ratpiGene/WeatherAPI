using System;
using System.Globalization;

namespace Application_meteo_csharp
{
    class Epoch
    {
        //Get the date and time on UTC and convert to Unix time (in seconds)

        private static long epoch = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        /*Method that add the timezone of countries (in seconds) to the epoch Utc and 
        convert back to date and time string format*/
        public static DateTimeOffset GetTimeAndDateNow(long timezone)
        {
            long currentime = epoch + timezone;
            DateTimeOffset date = DateTimeOffset.FromUnixTimeSeconds(currentime);
            return date;
        }

        public static string FormatDateTime(string date)
        {
            DateTime parsedDateTime;
            string FormattedDate;
            CultureInfo provider = CultureInfo.InvariantCulture;
            if(DateTime.TryParseExact(date, "yyyy-MM-dd HH:mm:ss",CultureInfo.InvariantCulture,DateTimeStyles.None,out parsedDateTime))
            {   
                FormattedDate = parsedDateTime.ToString("ddd, dd MMMM yyyy");
                return FormattedDate;
            }
            return "Failed";
           
            
        }

    }
}