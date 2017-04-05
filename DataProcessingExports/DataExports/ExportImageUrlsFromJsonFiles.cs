using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TweetDataExtractor.Json;
using TweetDataExtractor.SQL;

namespace DataProcessingExports.DataExports
{
    class ExportImageUrlsFromJsonFiles
    {

        private readonly string _folderPath;
        private readonly string _destinationFolder;

        private StreamWriter _logWriter;

        private long _tweetIndex;

        private long _originalTweetUrlIndex;

        private int _currentFileNumber;

        private static int _fileIndex = 1;

        private  StreamWriter _imageUrlWriter;

        private StreamWriter _originaltweetImageUrlStreamWriter;

        private static string _fileNamePrefix;

        private static int _lineslimit = 10000;

        private int _failedTweets;

        public ExportImageUrlsFromJsonFiles(string folderPath, string destinationFolder, string filenameprefix)
        {
            _folderPath = folderPath;
            _destinationFolder = destinationFolder;

            _fileNamePrefix = filenameprefix;
        }


        public void ExportImageUrlFromJson()
        {

            var jsonFilesList = LoadJsonFilesList();

            Console.WriteLine($"Total Files: {jsonFilesList.Length}.");

            // create a log writer to recored errors.
            _logWriter = new StreamWriter($@"{_destinationFolder}\LogFile.txt") { AutoFlush = true };

            _originaltweetImageUrlStreamWriter = new StreamWriter($@"{_destinationFolder}\{_fileNamePrefix}_image-urls-from-original-tweets.csv") { AutoFlush = true };

            // initialise writer to write tweet image urls.
            InitialiseWriter();


            try
            {
                foreach (var fileName in jsonFilesList)
                {

                    ProcessJsonFile(fileName);


                    _currentFileNumber++;

                    Console.WriteLine(
                        $" {DateTime.Now.ToString("s")}: Processed file number: {_currentFileNumber}, current image URL count: {_tweetIndex}, current original tweet URL count: {_originalTweetUrlIndex}");

                }

                _logWriter.WriteLine($"{DateTime.Now.ToString("s")}: Total Tweet image URLs exported : {_tweetIndex}.");
                Console.WriteLine($"{DateTime.Now.ToString("s")}: Total Tweet image URLs exported : {_tweetIndex}, total original tweet URL count: {_originalTweetUrlIndex}.");


                _logWriter.WriteLine($"{DateTime.Now.ToString("s")}: Number of failed tweets : {_failedTweets}.");
                Console.WriteLine($"{DateTime.Now.ToString("s")}: Number of failed tweets : {_failedTweets}.");



            }
            catch (Exception exception)
            {

                _logWriter.WriteLine($"{DateTime.Now.ToString("s")}: Failed at the root level error message : {exception.Message}.");

            }
            finally
            {
                _logWriter.Close();

                _imageUrlWriter.Close();

                _originaltweetImageUrlStreamWriter.Close();
            }





        }

        private void ProcessJsonFile(string fileName)
        {
            var reader = new StreamReader(fileName);

            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line)) continue;

                try
                {
                    var tweetObject = JsonConvert.DeserializeObject<TweetObject>(line);

                    if ((tweetObject == null) || (tweetObject.id == 0))
                    {
                        _logWriter.WriteLine("==================");
                        _logWriter.WriteLine($"{DateTime.Now.ToString("s")}: Failed to deserialize tweet data  : {line}.");
                        _logWriter.WriteLine("==================");

                        _failedTweets++;

                        continue;

                    }

                    if (tweetObject.entities.media != null)
                    {

                        var tweetDate = Utilities.TryParseTwitterDateTimeString(tweetObject.created_at,
                            DateTime.MinValue);

                        var isARetweet = tweetObject.retweeted_status == null ? 0 : 1;

                        foreach (var medium in tweetObject.entities.media)
                        {
                            _imageUrlWriter.WriteLine($"{tweetObject.id_str},{tweetDate.ToString("s")},{isARetweet},{medium.media_url}");

                            if (isARetweet != 0) continue;

                            _originaltweetImageUrlStreamWriter.WriteLine($"{tweetObject.id_str},{tweetDate.ToString("s")},{medium.media_url}");

                            _originalTweetUrlIndex++;
                        }

                        _tweetIndex++;

                        if (_tweetIndex % _lineslimit == 0)
                        {
                            InitialiseWriter();

                            Console.WriteLine($"{DateTime.Now}: Exported Image URL number: {_tweetIndex}");
                        }


                    }



                }
                catch (Exception exception)
                {
                    _logWriter.WriteLine($"{DateTime.Now}: Failed to process a line: {line}.");

                    _logWriter.WriteLine($"Error message : {exception.Message}.");

                }


            }

            _logWriter.WriteLine($"{_currentFileNumber}) Processed: {fileName}");
        }


        private string[] LoadJsonFilesList()
        {
            // change the file suitably based on extension...

            var filesList = Directory.GetFiles(_folderPath, "*.txt", SearchOption.AllDirectories);

            return filesList;


        }


        private  void InitialiseWriter()
        {
            // first claose the writer is it is laredy open.
            _imageUrlWriter?.Close();

            var filePath = $@"{_destinationFolder}\{_fileNamePrefix}_{_fileIndex}.csv";

            _imageUrlWriter = new StreamWriter(filePath) { AutoFlush = true };

            // write header
            _imageUrlWriter.WriteLine("TweetId,TweetDate,IsARetweet,ImageUrl");

            _fileIndex++;

        }



    }
}
