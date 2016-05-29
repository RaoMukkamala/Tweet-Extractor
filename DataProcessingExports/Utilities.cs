using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessingExports
{
    public static class Utilities
    {

        // Raghava : Check the correct connection string here.
        public static string connectionString = "Server=ITU-TIME-SERVER;DataBase=ChennaiFloods;Integrated Security=SSPI";
        //public static string connectionString = "Server=ITU-TIME-SERVER;DataBase=TwitterDatabase;Integrated Security=SSPI";

        public const string TwitterDateFormatString = "ddd MMM dd HH:mm:ss +ffff yyyy";

        public static string ProcessText(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            var newValue = value.Replace(Environment.NewLine, string.Empty);

            newValue = newValue.Replace(",", string.Empty);


            newValue = newValue.Replace("\"", string.Empty);

            newValue = newValue.Replace("'", string.Empty);


            newValue = newValue.Replace(@"\n", string.Empty);

            newValue = newValue.Replace(@"\r", string.Empty);

            newValue = newValue.Replace("\x000A", string.Empty);

            newValue = newValue.Replace("\x000D\x000A", string.Empty);

            newValue = newValue.Replace("\x000D", string.Empty);

            newValue = newValue.Replace("\x0A", string.Empty);

            newValue = newValue.Replace("\x0D", string.Empty);

            return $"\"{newValue}\"";

        }


        public static string ProcessTextExcludingComma(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            var newValue = value.Replace(Environment.NewLine, string.Empty);

            //newValue = newValue.Replace(",", string.Empty);


            newValue = newValue.Replace("\"", string.Empty);

            newValue = newValue.Replace("'", string.Empty);


            newValue = newValue.Replace(@"\n", string.Empty);

            newValue = newValue.Replace(@"\r", string.Empty);

            newValue = newValue.Replace("\x000A", string.Empty);

            newValue = newValue.Replace("\x000D\x000A", string.Empty);

            newValue = newValue.Replace("\x000D", string.Empty);

            newValue = newValue.Replace("\x0A", string.Empty);

            newValue = newValue.Replace("\x0D", string.Empty);

            return $"\"{newValue}\"";

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



        // Raghava: Extension Methofd on SQlDataReader object..

        public static List<string> ColumnList(this IDataReader dataReader)
        {
            var columns = new List<string>();
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                columns.Add(dataReader.GetName(i));
            }
            return columns;
        }



        public static List<string> GetValuesList(this IDataReader dataReader)
        {
            var objArray = new object[dataReader.FieldCount];

            dataReader.GetValues(objArray);

            return objArray.Select(obj => obj?.ToString() ?? string.Empty).ToList();
        }


        public static string GetRegExpressionForComaOutSidesQuotes()
        {

            // Reg expression to split based on comma located outside double quotes
            var regExpression = ",(?=(?:[^']*'[^']*')*[^']*$)";

            return regExpression.Replace('\'', '"');

        }

        public static string EscapeSingleQuotes(string sql)
        {
            if (string.IsNullOrEmpty(sql)) return sql;

            return sql.Replace("'", "''");

        }

        public static string GetSubString(string text, int length)
        {
            return string.IsNullOrEmpty(text) ? text : (text.Length <= length ? text : text.Substring(0, length));
        }

        public static int TryParseIntString(string value, int defaultValue)
        {
            int result;

            return (int.TryParse(value, out result)) ? result : defaultValue;

        }


        public static double TryParseDoubleString(string value, double defaultValue)
        {
            double result;

            return (double.TryParse(value, out result)) ? result : defaultValue;

        }

    }
}
