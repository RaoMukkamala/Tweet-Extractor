using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProcessingExports.DataExports;
using DataProcessingExports.DataImports;
using DataProcessingExports.DataProcessing;
using DataProcessingExports.Downloaders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            //UserExport.ExportUsers();
            //ExportTweets();
            //ExportTweetsAsJSON();
            //SortDictionaryTest();

            //ProcessTopicModels.ComputeDominentTopicsDayWise();

            // importing tweets to DB
            // ImportJsonTweetsToDB();

            //TwitterDateTest();

            // ExportImageUrlsFromJson();

            DownloadImages();

            // ss

            Console.ReadLine();

        }

        private static void DownloadImages()
        {
            var source = @"C:\Users\Alivelu\Documents\Alivelu-Research-Work\Twitter-Data-Analysis\Chennai-Floods\image-downloads\source-files";

            var destination = @"C:\Users\Alivelu\Documents\Alivelu-Research-Work\Twitter-Data-Analysis\Chennai-Floods\image-downloads\download-images";

            var imageDownloader = new DownloadTweetImages(source, destination);

            imageDownloader.DownloadImages();


        } 


        private static void TwitterDateTest()
        {
            var dateStr = "Mon Apr 15 00:09:45 +0000 2013";

            var dateTwit =Utilities.TryParseTwitterDateTimeString(dateStr, DateTime.MinValue);

            Console.WriteLine(dateTwit.ToString("s"));
        }

        private static void ExportImageUrlsFromJson()
        {
            //var sourceJsonFilesFolder = @"D:\Alivelu-data\Data\Twitter-Data\chennai-data\Extracted-From-Twitter-#chennaiMicro\output\tweets_json";

            //var fnPrefix = "#chennaimicro-image-urls-file";



            var sourceJsonFilesFolder = @"D:\Alivelu-data\Data\Twitter-Data\chennai-data\Extracted-From-Twitter-Chennai-Floods\output\tweets_json";

            var fnPrefix = "chennai-floods-image-urls-file";



            var destinationFolder = @"D:\Alivelu-data\Data\Twitter-Data\chennai-data\Chennai-tweet-image-urls";

            var tweetImporter = new ExportImageUrlsFromJsonFiles(sourceJsonFilesFolder, destinationFolder, fnPrefix);


            tweetImporter.ExportImageUrlFromJson();




        }

        private static void ImportJsonTweetsToDB()
        {
            var sourceJsonFilesFolder = @"D:\Alivelu-data\Data\BostonMarathon\test_images";

            var destinationFolder = @"D:\Alivelu-data\Data\BostonMarathon\test_images\destination_folder";

            var tweetImporter = new TweetImporterFromJsonFilesToDB(sourceJsonFilesFolder,destinationFolder);


            tweetImporter.ImportTweetFiles();




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

        private static void ExportTweetsAsJSON()
        {

            var query = "SELECT[TweetId] tweetId, CONVERT(char(10), [TweetDate], 126) date,[Text] text FROM[dbo].[TweetData] where tweetdate > '2015-11-29' and RetweetStatusBit = 0  order by TweetDate";


            query = "SELECT[TweetId] tweetId, CONVERT(char(10), [TweetDate], 126) date,[Text] text FROM[dbo].[TweetData] where RetweetStatusBit = 0 and RetweetCount = 0 and tweetdate > '2015-11-29' order by TweetDate";

            var destinationFolder = @"C:\Users\Alivelu\Dropbox\PhD-work\Twitter-data-analysis\Chennai-Floods\tweets-for-topic-modeling\original-tweets";

            var linesLimit = 500000;

            DataExporter.ExportDataAsJson(query, destinationFolder, linesLimit);
            

            //var property = new JProperty("Raghava","Asst. Professor");

            //var property2 = new JProperty("date", DateTime.Now.ToString("s"));

            //var jObj = new JObject {property, property2};

            //var array = new JArray {jObj};

            //Console.WriteLine(array);









        }



        private static void ExportTweets()
        {

            var query = "SELECT  [TweetId] ,CONVERT( datetime, [TweetDate], 126) ,[Text] FROM[dbo].[TweetData] order by TweetDate";

            var destinationFolder = @"C:\Users\Alivelu\Dropbox\PhD-work\Twitter-data-analysis\Chennai-Floods\tweets-for-topic-modeling";

            var linesLimit = 500000;

            DataExporter.ExportData(query, destinationFolder, linesLimit);




        }

        private static void SortDictionaryTest()
        {

            Dictionary<string, int> myDict = new Dictionary<string, int>();

            myDict.Add("one", 1);
            myDict.Add("four", 4);
            myDict.Add("two", 2);
            myDict.Add("three", 3);

            foreach (var keyValuePair in myDict)
            {
                Console.WriteLine(keyValuePair.Key + ":" + keyValuePair.Value);

            }


            var sortedDict = from entry in myDict orderby entry.Value descending select entry;

            foreach (var keyValuePair in sortedDict)
            {
                Console.WriteLine(keyValuePair.Key + ":" + keyValuePair.Value);

            }


        }
    }
}
