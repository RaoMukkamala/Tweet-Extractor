using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessingExports.DataProcessing
{
    class ProcessTopicModels
    {


        private static string sourceFilePath = @"D:\Alivelu-data\Data\Twitter-Data\chennai-data\topic-modeling\20160602-Latest\TopicDistribution-20160602.csv";

        private static string destinationPath_TopFive = @"D:\Alivelu-data\Data\Twitter-Data\chennai-data\topic-modeling\20160602-Latest\results\Daywise-Topic-distribution-Top5Only.csv";

        private static string destinationPath = @"D:\Alivelu-data\Data\Twitter-Data\chennai-data\topic-modeling\20160602-Latest\results\Daywise-Topic-distribution-all.csv";

        private static string tweetTopicFilePath = @"D:\Alivelu-data\Data\Twitter-Data\chennai-data\topic-modeling\20160602-Latest\results\TweetWise-Topic.csv";

        private static int startTopicIndex = 3;

        private static int endTopicIndex = 53;

        private static Dictionary<string, double> topicsDictionary = new Dictionary<string, double>();

        private static int _recordCount = 0;

        private static Dictionary<string, Dictionary<string, int>> topicsDistributionDictionary =
            new Dictionary<string, Dictionary<string, int>>();



        public static void ComputeDominentTopicsDayWise()
        {



            var reader = new StreamReader(sourceFilePath);

            var line = reader.ReadLine();

            var headerparts = line.Split("\t".ToCharArray());


            for (var index = startTopicIndex; index < endTopicIndex; index++)
            {
                topicsDictionary.Add($"Topic {headerparts[index]}", 0);
            }


            var tweet_topic_writer = new StreamWriter(tweetTopicFilePath) { AutoFlush = true };

            tweet_topic_writer.WriteLine($"Date,TweetId,Topic,probability");

            while ((line = reader.ReadLine()) != null)
            {
                _recordCount++;

                var parts = line.Split("\t".ToCharArray());

                var topicTuple = GetDominenetTopic(parts);

                tweet_topic_writer.WriteLine($"{parts[1]},{parts[2]},{topicTuple.Item1},{topicTuple.Item2}");

                UpdateTopicsCount(parts[1], topicTuple.Item1);

                if (_recordCount % 10000 == 0)
                {

                    Console.WriteLine($"{DateTime.Now}: Exported record number: {_recordCount}");
                }
            }

            // write dictionary as output.

            var daywise_topic_writer = new StreamWriter(destinationPath) { AutoFlush = true };

            var daywise_topic_writer_top5 = new StreamWriter(destinationPath_TopFive) { AutoFlush = true };

            daywise_topic_writer.WriteLine("Date,Topic,Count");

            foreach (var kvpair in topicsDistributionDictionary)
            {
                var dict = kvpair.Value;

                var sortedDict = from kvEntry in dict orderby kvEntry.Value descending select kvEntry;

                var sortedDictTop5 = (from kvEntry in dict orderby kvEntry.Value descending select kvEntry).Take(5);

                foreach (var keyValuePair in sortedDict)
                {
                    daywise_topic_writer.WriteLine($"{kvpair.Key},{keyValuePair.Key},{keyValuePair.Value}");
                }


                foreach (var keyValuePair in sortedDictTop5)
                {
                    daywise_topic_writer_top5.WriteLine($"{kvpair.Key},{keyValuePair.Key},{keyValuePair.Value}");
                }


            }

            tweet_topic_writer.Close();

            daywise_topic_writer.Close();

            daywise_topic_writer_top5.Close();


            Console.WriteLine($"{DateTime.Now}: Done with Exporting. Total records: {_recordCount}");
        }


        private static Tuple<string, double> GetDominenetTopic(string[] parts)
        {

            for (var index = startTopicIndex; index < endTopicIndex; index++)
            {
                topicsDictionary[$"Topic {index - 3}"] = Utilities.TryParseDoubleString(parts[index], 0);
            }

            var sortedDict = from entry in topicsDictionary orderby entry.Value descending select entry;

            var tuple = new Tuple<string, double>(sortedDict.First().Key, sortedDict.First().Value);


            return tuple;
        }

        private static void UpdateTopicsCount(string date, string topic)
        {
            // create a new dictionary if the data is not found.
            if (!topicsDistributionDictionary.ContainsKey(date))
            {
                topicsDistributionDictionary.Add(date, new Dictionary<string, int>());
            }

            // Increment count of topic if it is already exists.
            if (topicsDistributionDictionary[date].ContainsKey(topic))
            {
                topicsDistributionDictionary[date][topic]++;
            }
            else
            {
                topicsDistributionDictionary[date].Add(topic, 1);
            }

        }


    }
}
