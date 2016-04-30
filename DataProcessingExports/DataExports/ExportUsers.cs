using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessingExports.DataExports
{
    class UserExport
    {


        //private static string _sqlQuery = @"SELECT UserId,UserName, [UserFollowerCount], [UserStatusesCount] FROM TwitterUsers WHERE 
        //        UserId IN 
	       //     (SELECT DISTINCT [OriginalTweetUserId] OriginalTweetUsers FROM [ChennaiFloods].[dbo].[TweetData] where [OriginalTweetId] is not null) 
	       //   OR UserId in	(select distinct [UserId] RetweetedUsers FROM [ChennaiFloods].[dbo].[TweetData] where [OriginalTweetId] is not null)";


        private static string _sqlQuery =
            @"select [OriginalTweetUserId],UserId, COUNT([TweetId]) RetweetCount FROM [ChennaiFloods].[dbo].[TweetData]
where [OriginalTweetId] is not null GROUP BY [OriginalTweetUserId],UserId order by RetweetCount desc";


        private static string _destinationFolder = @"D:\Alivelu-data\Data\Chenna-Floods\For-SNA\";


        private static int _lineslimit = 1000000;

        private static int _fileIndex = 1;

        private static int _recordCount = 0;

        private static StreamWriter _writer;

        //private static string _fileNamePrefix = "TweetedReTweetedUsers";
        private static string _fileNamePrefix = "Edges-Tweet-ReTweet-Users";

        public static void ExportUsers()
        {

            Console.WriteLine($"{DateTime.Now}: Starting exporting records.");

            InitialiseWriter();

            try
            {
                using (var connection = new SqlConnection(Utilities.connectionString))
                {
                    var command = new SqlCommand
                    {
                        CommandText = _sqlQuery,
                        CommandType = CommandType.Text,
                        Connection = connection,
                        CommandTimeout = 0
                    };


                    connection.Open();

                    var dataReader = command.ExecuteReader();

                    var columnList = string.Join(",", dataReader.ColumnList());

                    _writer.WriteLine(columnList);


                    while (dataReader.Read())
                    {

                        var valuesStr = string.Join(",", dataReader.GetValuesList());

                        _writer.WriteLine(valuesStr);

                        _recordCount++;

                        if (_recordCount % _lineslimit == 0)
                        {
                            InitialiseWriter();

                            _writer.WriteLine(columnList);

                            Console.WriteLine($"{DateTime.Now}: Exported record number: {_recordCount}");
                        }


                    }


                    Console.WriteLine($"{DateTime.Now}: Done exporting records.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to export data from database. Error: {ex.Message}");
            }




        }



        private static void InitialiseWriter()
        {
            // first claose the writer is it is laredy open.
            _writer?.Close();

            var filePath = $@"{_destinationFolder}\{_fileNamePrefix}_{_fileIndex}.csv";

            _writer = new StreamWriter(filePath) { AutoFlush = true };

            _fileIndex++;






        }


    }
}
