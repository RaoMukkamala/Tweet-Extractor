using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Newtonsoft.Json;
using TweetDataExtractor.Json;

namespace DataProcessingExports.DataProcessing
{
    class TweetUserImporter
    {
        private static string _sourceFilesFolder = @"D:\Alivelu-data\Data\Twitter-Data\chennai-data\Extracted-From-Twitter-#chennaiMicro\output\tweets_json";
        //private static string _sourceFilesFolder = @"D:\Alivelu-data\Data\Twitter-Data\chennai-data\Extracted-From-Twitter-Chennai-Floods\output\tweets_json";

        private static string _destinationFolder = @"D:\Alivelu-data\Data\Twitter-Data\chennai-data\logs";


        private static int _currentFileNumber = 1;

        private static StreamWriter _logWriter;

        private static StreamWriter _errorWriter;

        private static int _failedTweetNumber;

        private static int _tweetUserCount;




        public static void ImportTweetUserToDb()
        {
            var jsonFilesList = LoadFilesList();

            Console.WriteLine($"{DateTime.Now.ToString("s")}:- Total Files:= {jsonFilesList.Length}.");

            // create a log writer to recored errors.
            _logWriter = new StreamWriter($@"{_destinationFolder}\ImportUserLogFile.txt") { AutoFlush = true };

            // create a log writer to recored errors.
            _errorWriter = new StreamWriter($@"{_destinationFolder}\ImportUserFailedTweetJason.txt") { AutoFlush = true };

            foreach (var fileName in jsonFilesList)
            {
                Console.WriteLine(
                    $"{DateTime.Now.ToString("s")}:- Processing file number:{_currentFileNumber}) FileName:= {fileName}");

                ProcessFile(fileName);

                var msg = $"{DateTime.Now.ToString("s")}:- Completed File Processing #{_currentFileNumber}. " +
                          $"Imported user Count:= {_tweetUserCount}, failed user count:={_failedTweetNumber}";

                Console.WriteLine(msg);

                _logWriter.WriteLine(msg);

                _currentFileNumber++;
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

                    // Save the user.
                    PersistUser(tweetObject.user);
                   

                    if (tweetObject.retweeted_status != null)
                    {
                        // Save the original tweet user also
                        PersistUser(tweetObject.retweeted_status.user);

                    }


                    

                    if (_tweetUserCount % 1000 == 0)
                    {
                        Console.WriteLine($"{DateTime.Now.ToString("s")}:- Importing twitter user #{_tweetUserCount} ! ");
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

        private static void PersistUser(User tweetUser)
        {
            if (tweetUser == null)
            {
                return;
            }



            // First delete the user if already exists.
            var query = $"DELETE [dbo].[TwitterUsers] WHERE [UserId] = {tweetUser.id};";

            query += $@"INSERT INTO [dbo].[TwitterUsers]
           ([UserId]
           ,[UserName]
           ,[ScreenName]
           ,[UserLocation]
           ,[Description]
           ,[UserFollowerCount]
           ,[UserFriendsCount]
           ,[UserListedCount]
           ,[UserFavoritesCount]
           ,[UserCreatedDate]
           ,[UserTimeZone]
           ,[UserUTCOffset]
           ,[UserStatusesCount]
           ,[UserLang])
       VALUES
           ({tweetUser.id}
           ,'{Utilities.EscapeSingleQuotes(tweetUser.name)}'
           ,'{Utilities.EscapeSingleQuotes(tweetUser.screen_name)}'
           ,'{Utilities.EscapeSingleQuotes(Utilities.GetSubString(tweetUser.location, 249))}'
           ,'{Utilities.EscapeSingleQuotes(Utilities.GetSubString(tweetUser.description, 249))}'
           ,{tweetUser.followers_count}
           ,{tweetUser.friends_count}
           ,{tweetUser.listed_count}
           ,{tweetUser.friends_count}
           ,'{Utilities.TryParseTwitterDateTimeString(tweetUser.created_at, DateTime.MinValue)}'
           ,'{Utilities.EscapeSingleQuotes(tweetUser.time_zone)}'
           ,'{tweetUser.utc_offset}'
           ,{tweetUser.statuses_count}
           ,'{tweetUser.lang}'
            );";

            ExecuteSqlQuery(query);


            _tweetUserCount++;

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
