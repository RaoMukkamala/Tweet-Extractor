using System;
using System.IO;

namespace TweetDataExtractor.Tweet
{
    class TweetIdFileProcessor
    {
        private readonly string _filePath;

        private StreamReader _fileReader;

        private string _fileNameWithoutExtension;

        private StreamWriter _errorWriter;

        private StreamWriter _jsonWriter;

        private StreamWriter _nullTweetIdsWriter;

        private StreamWriter _exportWriter;

        private TweetStats _stats = new TweetStats();

        private StreamWriter _statsWriter;

        
        public TweetIdFileProcessor(string filePath)
        {
            _filePath = filePath;

            _fileReader = new StreamReader(_filePath);
        }


        public void ProcessFile()
        {
            // 1. create a directory for the input file.
            Console.WriteLine("{0}: Processing file : {1}", DateTime.Now, _filePath);

            GetoutputDirectoryPathInfo();

            // 2. Create an error file writer.

            _errorWriter =
                new StreamWriter(string.Format(@"{0}\Error_log_{1}.txt", ConfigManager.ConfigurationManagerInstance.LogsFolderPath, _fileNameWithoutExtension));

            _jsonWriter = new StreamWriter(string.Format(@"{0}\Tweets_json_{1}.txt", ConfigManager.ConfigurationManagerInstance.TweetJsonFolderPath, _fileNameWithoutExtension));

            _nullTweetIdsWriter = new StreamWriter(string.Format(@"{0}\Empty_tweetIds_{1}.txt", ConfigManager.ConfigurationManagerInstance.LogsFolderPath, _fileNameWithoutExtension));

            _statsWriter = new StreamWriter(string.Format(@"{0}\Tweets_Stats_{1}.txt", ConfigManager.ConfigurationManagerInstance.LogsFolderPath, _fileNameWithoutExtension));

            _exportWriter = new StreamWriter(string.Format(@"{0}\Tweets_export_{1}.txt", ConfigManager.ConfigurationManagerInstance.ExportFolderPath, _fileNameWithoutExtension));

            WriteExportHeader();

            try
            {
                // 3. prepare 100 Tweet ids string

                var tweetIds = GetTweetIdsString();

                while (!string.IsNullOrEmpty(tweetIds))
                {

                    // 4. Pass it to TweetExtractor class: destination folder path, error file writer, jasonWriter, ids string
                    //Console.WriteLine(tweetIds);


                    var tweetDownloader = new TweetDownloader(tweetIds, _jsonWriter, _errorWriter, _nullTweetIdsWriter,
                        _exportWriter,
                        ref _stats);

                    tweetDownloader.DownloadTweets();

                    // extract the tweetids strings again for next round
                    tweetIds = GetTweetIdsString();


                }

                // When we have no tweetids,
                _fileReader.Close();

                var fileInfo = new FileInfo(_filePath);

                var destinationFileName = string.Format(@"{0}\{1}",
                    ConfigManager.ConfigurationManagerInstance.CompletedFolderPath, fileInfo.Name);


                if (File.Exists(destinationFileName))
                {
                    File.Delete(destinationFileName);
                }


                File.Move(_filePath, destinationFileName);


                // 5. When file is processed, move the file to completed folder.

                _errorWriter.Close();

                _jsonWriter.Close();

                _nullTweetIdsWriter.Close();

                //_unprocessedTweetIdsWriter.Close();

                WriteStats();
            }
            catch (Exception exception)
            {

                _statsWriter.WriteLine("Error occured when processing the file. Error message: " + exception.Message);
            }


        }

        private string GetoutputDirectoryPathInfo()
        {

            var fileInfo = new FileInfo(_filePath);

            _fileNameWithoutExtension = fileInfo.Name.Replace(fileInfo.Extension, "");

            var dirPath = string.Format(@"{0}\{1}", ConfigManager.ConfigurationManagerInstance.OutputFolderPath,
                _fileNameWithoutExtension);

            //var dirInfo = Directory.CreateDirectory(dirPath);

            return dirPath;

        }

        private void WriteStats()
        {

            



            _statsWriter.WriteLine("Extracted Tweets: " + _stats.ExtractedTweetCount);

            _statsWriter.WriteLine("EmptyTweetCount: " + _stats.EmptyTweetCount);

            _statsWriter.WriteLine("UnprocessedTweetCount: " + _stats.UnprocessedTweetCount);

            _statsWriter.WriteLine("TweetsWithDataErrors: " + _stats.TweetsWithDataErrors);
            
            _statsWriter.Flush();

            _statsWriter.Close();


        }

        private string GetTweetIdsString()
        {
            var idsString = string.Empty;

            int currentline = 0;

            while (_fileReader.Peek() > 0)
            {
                idsString = string.Format("{0},{1}", idsString, _fileReader.ReadLine());

                currentline++;

                if (currentline == 100)
                {
                    break;
                }
            }

            if (idsString.StartsWith(","))
            {
                idsString = idsString.Remove(0, 1);

            }

            // finally return
            return idsString;
        }

        private void WriteExportHeader()
        {
            

           // TweetId	TweetDate	Text	Truncated	RetweetCount	FavoriteCount	Retweeted	Favorited	TweetLang	UserId	UserName	UserLocation	UserFollowerCount	UserFriendsCount	UserCreatedDate	UserFavoritesCount	UserTimeZone	UserUTCOffset	UserStatusesCount	UserLang
            //261564176915365888	2012-10-25 20:25:34.000	'Sandy' castigó Santiago de Cuba con derrumbes de viviendas y árboles y con cortes eléctricos - 20minutos.es http://t.co/HVQU9cZC vía @20m	0	0	0	0	0	es	306108015	DOLORES LOPEZ 	Vilanova i laGeltrú	182	371	2011-05-27 09:13:21.000	744	Amsterdam	2.00 h	2181	es

            _exportWriter.WriteLine("TweetId,TweetDate,Text,Truncated,RetweetCount, FavoriteCount,Retweeted,Favorited,TweetLang,UserId,UserName, UserLocation, UserFollowerCount,UserFriendsCount,UserCreatedDate,UserFavoritesCount,UserTimeZone,UserUTCOffset,UserStatusesCount,UserLang,Hashtags");
        }


    }
}
