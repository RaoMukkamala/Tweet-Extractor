using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace DataProcessingExports.Downloaders
{
    class DownloadTweetImages
    {
        private readonly string _sourceFolderPath;

        private readonly string _destinationFolder;

        private StreamWriter _logWriter;

        private StreamWriter _completedUrlWriter;

        private StreamWriter _failedUrlWriter;

        private int _currentFileNumber;

        private int _downloadedImagCount;


        private int _failedImageCount;

        public DownloadTweetImages(string sourceFolderPath, string destinationFolder)
        {
            _sourceFolderPath = sourceFolderPath;

            _destinationFolder = destinationFolder;
        }


        public void DownloadImages()
        {
            // initialise all writers
            InitilizeWriters();

            var filesList = LoadFilesList();

            Console.WriteLine($"Total Files: {filesList.Length}.");

            foreach (var fileName in filesList)
            {
                try
                {
                    ProcessJsonFile(fileName);

                    _currentFileNumber++;

                    Console.WriteLine(
                        $" {DateTime.Now.ToString("s")}: Processed file number: {_currentFileNumber}, " +
                        $"downloaded image URL count: {_downloadedImagCount}, failed image count: {_failedImageCount}");

                }
                catch (Exception exception)
                {
                    _logWriter.WriteLine($"Failed to process File: {fileName}; error message: {exception.Message}");

                }

            }


            Console.WriteLine($" {DateTime.Now.ToString("s")}:DONE Processing! Total files processed: {_currentFileNumber}, " +
                              $"downloaded image URL count: {_downloadedImagCount}, failed image count: {_failedImageCount}");

            _logWriter.Close();

            _failedUrlWriter.Close();

            _completedUrlWriter.Close();


        }

        private void ProcessJsonFile(string fileName)
        {

            var reader = new StreamReader(fileName);

            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line)) continue;

                // if it does not contain image URL pass on
                if(! line.Contains("http://")) continue;

                try
                {
                    var parts = line.Split(',');

                    // TweetId:parts[0], TweetDate: parts[1], Url:parts[2]


                    var webRequest = WebRequest.Create(parts[2]);

                    var request = (HttpWebRequest)webRequest;

                    request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:38.0) Gecko/20100101 Firefox/38.0";

                    request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

                    request.Headers.Add("Accept-Encoding", "gzip, deflate");

                    var response = request.GetResponse();

                    var stream = response.GetResponseStream();

                    var img = Image.FromStream(stream);

                    var imageName = GetFileNameFromURI(parts[2]);

                    var filePathForImage = GetFilePathForImage(parts[0], parts[1], imageName);

                    img.Save(filePathForImage);

                    _downloadedImagCount++;

                    _completedUrlWriter.WriteLine(line);


                }
                catch (Exception exception)
                {
                    _failedImageCount++;

                    _failedUrlWriter.WriteLine(line);

                    _logWriter.WriteLine($"{DateTime.Now}: Failed to process a line: {line}.");

                    _logWriter.WriteLine($"Error message : {exception.Message}.");

                }


                if ((_downloadedImagCount + _failedImageCount) % 100 == 0)
                {
                    _logWriter.WriteLine($"{DateTime.Now}: Downloaded image count: {_downloadedImagCount}, Failed images count: {_failedImageCount}.");

                    Console.WriteLine($"{DateTime.Now}: Downloaded image count: {_downloadedImagCount}, Failed images count: {_failedImageCount}.");

                }

                if ((_downloadedImagCount + _failedImageCount)%1000 == 0)
                {
                    Console.WriteLine($"{DateTime.Now}: Thread going to sleep 2 minutes");
                    // make the thread to sleep for 2 minutes
                    Thread.Sleep(120000);
                    //
                    Console.WriteLine($"{DateTime.Now}: Thread started after sleep!");
                }







            }
        }


        private string GetFilePathForImage(string tweetId, string timestamp, string imageFilename)
        {
            var tweetDate = DateTime.Parse(timestamp);

            // Check whether directory exists with date

            var folderPath = $@"{_destinationFolder}\{tweetDate.ToString("yyyy-MM-dd")}";

            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            return $@"{folderPath}\{Utilities.TransformTimestampToFilenameformat(timestamp)}_{tweetId}_{imageFilename}";

        }

        private string GetFileNameFromURI(string imageUrl)
        {
            if(string.IsNullOrEmpty(imageUrl)) return String.Empty;

            var split = imageUrl.Split('/');

            return split[split.Length - 1];
        }



        private string[] LoadFilesList()
        {
            // change the file suitably based on extension...

            var filesList = Directory.GetFiles(_sourceFolderPath, "*.csv", SearchOption.AllDirectories);

            return filesList;


        }


        private void InitilizeWriters()
        {
            var filePath = $@"{_destinationFolder}\LogFile_{Utilities.GetTimestampForFilenames()}.txt";

            _logWriter = new StreamWriter(filePath) { AutoFlush = true };


            // write header
            //_logWriter.WriteLine("TweetId,TweetDate,IsARetweet,ImageUrl");

            filePath = $@"{_destinationFolder}\Completed_Urls_{Utilities.GetTimestampForFilenames()}.csv";

            _completedUrlWriter = new StreamWriter(filePath) { AutoFlush = true };

            _completedUrlWriter.WriteLine("TweetId,TweetDate,ImageUrl");

            filePath = $@"{_destinationFolder}\Failed_Urls_{Utilities.GetTimestampForFilenames()}.csv";

            _failedUrlWriter = new StreamWriter(filePath) { AutoFlush = true };

            _failedUrlWriter.WriteLine("TweetId,TweetDate,ImageUrl");




        }



    }
}
