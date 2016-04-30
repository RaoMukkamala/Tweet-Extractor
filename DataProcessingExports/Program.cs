using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProcessingExports.DataExports;
using DataProcessingExports.DataProcessing;
using Newtonsoft.Json;
using TweetDataExtractor.Json;

namespace DataProcessingExports
{
    class Program
    {
        static void Main(string[] args)
        {

            //DataCleaner.CleanData();

            //DataExporter.ExportData();

            //Tweetparsing();
            //ParseTweets();

            //RetweetStatusUpdater.UpdateRetweetStatusToDb();

            //TweetUserImporter.ImportTweetUserToDb();
            UserExport.ExportUsers();


            Console.ReadLine();

        }

        private static void Tweetparsing()
        {
            try
            {
                var path = @"C:\Users\Alivelu\Dropbox\PhD-work\solutions\DataCleaningExports_sln\retweet-example.json";

                //path = @"D:\Alivelu-data\Data\Twitter-Data\chennai-data\Extracted-From-Twitter-Chennai-Floods\output\tweets_json\Tweets_json_TweetId_File_1.txt";

                var reader = new StreamReader(path);

                var jsonString = reader.ReadToEnd();

                var tObj = JsonConvert.DeserializeObject<RootObject>(jsonString);

                Console.WriteLine($"Number of Tweets: {tObj.tweets.Count}");
            }
            catch (Exception exception)
            {

                Console.WriteLine(exception.Message);
            }


        }

        private static void ParseTweets()
        {
            try
            {

                var path = @"D:\Alivelu-data\Data\Twitter-Data\chennai-data\Extracted-From-Twitter-Chennai-Floods\output\tweets_json\Tweets_json_TweetId_File_1.txt";

                var reader = new StreamReader(path);

                string line;

                int retweetNumber = 0;

                while ((line = reader.ReadLine()) != null)
                {

                    var tweetObject = JsonConvert.DeserializeObject<TweetObject>(line);



                    if (tweetObject.retweeted_status != null)
                    {
                        Console.WriteLine($"Tweet Id:{tweetObject.id}, Retweet Status: true, Original TweetId: {tweetObject.retweeted_status.id}");

                        retweetNumber++;
                    }
                    else
                    {
                        Console.WriteLine($"Tweet Id:{tweetObject.id}");
                    }



                }


                Console.WriteLine($"Number of Tweets: {retweetNumber}");
            }
            catch (Exception exception)
            {

                Console.WriteLine(exception.Message);
            }



        }
    }
}
