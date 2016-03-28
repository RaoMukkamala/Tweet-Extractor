using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TweetDataExtractor.Json;

namespace DataProcessingExports.DataProcessing
{
    class RetweetStatusUpdater
    {

        private static string _sourceFilesFolder = @"D:\Alivelu-data\Data\Twitter-Data\chennai-data\Extracted-From-Twitter-#chennaiMicro\output\tweets_json";

        private static string _destinationFolder = @"D:\Alivelu-data\Data\Twitter-Data\chennai-data\Retweet-Status-Updates";


        private static int _currentFileNumber = 1;

        private static StreamWriter _logWriter;

        private static StreamWriter _errorWriter;

        private static int _updatedTweetNumber;

        private static int _failedTweetNumber;

        private static int _tweetIndex;


        public static void UpdateRetweetStatusToDb()
        {
            var jsonFilesList = LoadFilesList();

            Console.WriteLine($"{DateTime.Now.ToString("s")}:- Total Files:= {jsonFilesList.Length}.");

            // create a log writer to recored errors.
            _logWriter = new StreamWriter($@"{_destinationFolder}\LogFile.txt") { AutoFlush = true };

            // create a log writer to recored errors.
            _errorWriter = new StreamWriter($@"{_destinationFolder}\FaileTweetJason.txt") { AutoFlush = true };

            foreach (var fileName in jsonFilesList)
            {
                Console.WriteLine(
                    $"{DateTime.Now.ToString("s")}:- Processing file number:{_currentFileNumber}) FileName:= {fileName}");

                ProcessFile(fileName);

                var msg = $"{DateTime.Now.ToString("s")}:- Completed File Processing #{_currentFileNumber}. " +
                          $"Updated Tweet Count:= {_updatedTweetNumber}, failed tweet count:={_failedTweetNumber}";

                Console.WriteLine(msg);

                _logWriter.WriteLine(msg);
            }

            _logWriter.WriteLine($"{DateTime.Now.ToString("s")}:- Processing of {jsonFilesList.Length} files successfully completed! ");

            _logWriter.Close();

            _errorWriter.Close();



            Console.WriteLine($"{DateTime.Now.ToString("s")}:- Processing of {jsonFilesList.Length} files successfully completed! ");



        }



        private static void ProcessFile(string fileName)
        {

            var reader = new StreamReader(fileName);

            string line;

            while ((line = reader.ReadLine()) != null)
            {
                try
                {
                    var tweetObject = JsonConvert.DeserializeObject<TweetObject>(line);

                    string sqlQuery;

                    if (tweetObject.retweeted_status != null)
                    {
                        sqlQuery = $"UPDATE [dbo].[TweetData] SET [RetweetStatusBit] = 1,  [OriginalTweetId] = {tweetObject.retweeted_status.id}, " +
                                   $"[OriginalTweetUserId] = {tweetObject.retweeted_status.user.id}," +
                                   $" [OriginalTweetDate] = '{(Utilities.TryParseTwitterDateTimeString(tweetObject.retweeted_status.created_at, DateTime.MaxValue)).ToString("s")}' " +
                                   $"WHERE [TweetId] = {tweetObject.id};";



                    }
                    else
                    {
                        sqlQuery = $"UPDATE [dbo].[TweetData] SET [RetweetStatusBit] = 0,  [OriginalTweetId] = NULL, [OriginalTweetUserId] = NULL," +
                                   $" [OriginalTweetDate] = NULL WHERE [TweetId] = {tweetObject.id};";

                    }

                    ExecuteSqlQuery(sqlQuery);

                    _tweetIndex++;

                    if (_tweetIndex % 1000 == 0)
                    {
                        Console.WriteLine($"{DateTime.Now.ToString("s")}:- Processing of {_tweetIndex} record completed! ");
                    }


                }
                catch (Exception exception)
                {
                    var msg =
                        $"{DateTime.Now.ToString("s")}:- Failed to parse json from filename:={fileName}. " +
                        $"{Environment.NewLine} Error Message: {exception.Message}";

                    _logWriter.WriteLine(msg);

                    _errorWriter.WriteLine(line);

                    Console.WriteLine(msg);

                    _failedTweetNumber++;
                }



            }


        }


        private static void ExecuteSqlQuery(string query)
        {
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

                    _updatedTweetNumber++;

                }
            }
            catch (Exception exception)
            {
                var msg = $"{DateTime.Now.ToString("s")}:- Failed to execute query: = {query}. {Environment.NewLine} Error Message: {exception.Message}";

                _logWriter.WriteLine(msg);

                Console.WriteLine(msg);

                _failedTweetNumber++;
            }

        }


        private static string[] LoadFilesList()
        {

            var filesList = Directory.GetFiles(_sourceFilesFolder, "*.txt", SearchOption.AllDirectories);

            return filesList;


        }
    }
}
