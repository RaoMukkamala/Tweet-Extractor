using System;
using System.Globalization;
using System.Linq;
using TweetDataExtractor.Json;

namespace TweetDataExtractor
{
    public static class Utilities
    {

        public const string TwitterDateFormatString = "ddd MMM dd HH:mm:ss +ffff yyyy";

        public static int TryParseIntString(string value, int defaultValue)
        {
            int result;

            return (int.TryParse(value, out result)) ? result : defaultValue;

        }



        public static DateTime TryParseTwitterDateTimeString(string value, DateTime defaultValue)
        {
            try
            {
                return DateTime.ParseExact(value, TwitterDateFormatString, new CultureInfo("en-us"));
            }
            catch
            {
                return defaultValue;
            }



        }






        public static DateTime TryParseDateTimeString(string value, DateTime defaultValue)
        {
            DateTime result;

            return (DateTime.TryParse(value, out result)) ? result : defaultValue;
        }


        public static bool TryParseBooleanValues(string value)
        {
            bool result;

            return (Boolean.TryParse(value, out result)) && result;
        }


        public static int ConvertToBitValue(string value)
        {
            if (string.IsNullOrEmpty(value)) return 0;

            return value.ToLower() == "true" ?  1 :  0;

        }

        public static short TryParseSmallIntString(string value, short defaultValue)
        {
            short result;

            return (short.TryParse(value, out result)) ? result : defaultValue;

        }


        public static string ConvertUTCOffset(string value)
        {

            try
            {
                if (string.IsNullOrEmpty(value))
                {
                    return "No Value";
                }

                var val = (float)TryParseIntString(value, 0);

                return (val / 3600).ToString("0.00", new CultureInfo("en-us")) + " h";

            }
            catch
            {
                return string.Empty;

            }
        }

        public static string ExtractHashTags(TweetObject tweetobj)
        {
            var hashtagsText = string.Empty;

            if (tweetobj.entities?.hashtags != null)
            {
                hashtagsText = tweetobj.entities.hashtags.Where(hashtag => !string.IsNullOrEmpty(hashtag.text)).Aggregate(hashtagsText, (current, hashtag) => string.Format("{0},{1}", current, hashtag.text));
            }

            if (hashtagsText.StartsWith(","))
            {
                hashtagsText = hashtagsText.Remove(0, 1);
            }



            return $"\"{hashtagsText}\"";

        }


        public static DateTime FromUnixTimeToUtcDateTime(this long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }

        public static long ToUnixTimeFromUtcDateTime(this DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date.ToUniversalTime() - epoch).TotalSeconds);
        }

    }
}
