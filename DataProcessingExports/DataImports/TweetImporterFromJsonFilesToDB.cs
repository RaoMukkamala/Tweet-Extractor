using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TweetDataExtractor.Json;
using TweetDataExtractor.SQL;

namespace DataProcessingExports.DataImports
{
    public class TweetImporterFromJsonFilesToDB
    {
        private readonly string _folderPath;
        private readonly string _destinationFolder;

        private StreamWriter _logWriter;

        private long _tweetIndex = 0;

        private long _failedTweets = 0;

        private int _currentFileNumber = 1;

        public TweetImporterFromJsonFilesToDB(string folderPath, string destinationFolder)
        {
            _folderPath = folderPath;
            _destinationFolder = destinationFolder;
        }

        public void ImportTweetFiles()
        {
            var jsonFilesList = LoadJsonFilesList();

            Console.WriteLine($"Total Files: {jsonFilesList.Length}.");

            // create a log writer to recored errors.
            _logWriter = new StreamWriter($@"{_destinationFolder}\LogFile.txt") { AutoFlush = true };


            try
            {
                foreach (var fileName in jsonFilesList)
                {

                    ProcessJsonFile(fileName);


                    _currentFileNumber++;

                    Console.WriteLine("{0}) Processed: {1}", _currentFileNumber, fileName);

                }

                _logWriter.WriteLine($"Total Tweet imported to DB : {_tweetIndex}.");

            }
            catch (Exception exception)
            {

                _logWriter.WriteLine($"Failed at the root level error message : {exception.Message}.");

            }
            finally
            {
                _logWriter.Close();
            }




            

            

        }

        private void ProcessJsonFile(string fileName)
        {
            var reader = new StreamReader(fileName);

            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if(string.IsNullOrEmpty(line)) continue;

                try
                {
                    var tweetObject = JsonConvert.DeserializeObject<TweetObject>(line);

                    if ((tweetObject == null) || (tweetObject.id == 0))
                    {
                        _logWriter.WriteLine("==================");
                        _logWriter.WriteLine($"Failed to deserialize tweet data  : {line}.");
                        _logWriter.WriteLine("==================");

                        _failedTweets++;

                        continue;

                    }

                    if (tweetObject.entities.media != null)
                    {
                        Console.WriteLine(tweetObject.entities.media.Count);

                        Console.WriteLine(tweetObject.entities.media[0].media_url);
                    }

                    var tweetToDb = new TweetToDb(tweetObject);


                    tweetToDb.InsertTweetToDb(_logWriter);

                    _tweetIndex++;

                    if (_tweetIndex % 10000 == 0)
                    {
                        Console.WriteLine($"Processed: {_tweetIndex} tweets!");

                    }



                }
                catch (Exception exception)
                {
                    _logWriter.WriteLine($"Failed to process a line: {line}.");

                    _logWriter.WriteLine($"Error message : {exception.Message}.");

                }


            }

            _logWriter.WriteLine($"{_currentFileNumber}) Processed: {fileName}");





        }


        private string[] LoadJsonFilesList()
        {

            var filesList = Directory.GetFiles(_folderPath, "*.json", SearchOption.AllDirectories);

            return filesList;


        }

    }
}
