using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessingExports.DataProcessing
{
    class DataCleaner
    {

        private static int recordCount = 0;


        public static void CleanData()
        {

            Console.WriteLine($"{DateTime.Now}: Starting updating records.");




            try
            {
                using (var connection = new SqlConnection(Utilities.connectionString))
                {
                    var command = new SqlCommand
                    {
                        CommandText = "select * FROM [dbo].[TweetData] order by [TweetDate]",
                        CommandType = CommandType.Text,
                        Connection = connection,
                        CommandTimeout = 0
                    };


                    connection.Open();

                    var dataReader = command.ExecuteReader();


                    while (dataReader.Read())
                    {
                        var tweetId = dataReader.GetInt64(0);

                        var text = dataReader.IsDBNull(2) ? string.Empty : dataReader.GetString(2);

                        UpdateText(tweetId, text);

                        recordCount++;

                        if (recordCount % 1000 == 0)
                        {

                            Console.WriteLine($"{DateTime.Now}: Updated record number: {recordCount}");
                        }


                    }


                    Console.WriteLine($"{DateTime.Now}: Done updating records.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update data to database. Error: {ex.Message}");
            }

        }


        private static void UpdateText(long tweetId, string text)
        {

            text = Utilities.ProcessText(text);

            var query = $"Update [dbo].[TweetData] SET [Text] = '{text}' where TweetId = {tweetId} ";

            try
            {
                using (var connection = new SqlConnection(Utilities.connectionString))
                {
                    var command = new SqlCommand
                    {
                        CommandText = query,
                        CommandType = CommandType.Text,
                        Connection = connection,
                        CommandTimeout = 0
                    };


                    connection.Open();

                    command.ExecuteNonQuery();




                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update text to  database. Error: {ex.Message}");
            }

        }




    }
}
