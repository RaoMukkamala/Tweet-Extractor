using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataProcessingExports.DataImports
{
    class TweetIdExtractor
    {

        private string _destinationFolder = @"D:\Alivelu-data\Data\Twitter-Data\chennai-data\#chennaiMicro-TweetIds";

        private string _sourceFilesFolder = @"C:\Users\Alivelu\Dropbox\PhD-work\reasearch-work\Chennai floods\#chennaiMicro-Tweets";

        private int _currentFileNumber = 1;

        private StreamWriter _logWriter;

        private long _tweetIndex = 0;





        public void ExtractTweetId()
        {
            var csvFilesList = LoadCSVFilesList();

            Console.WriteLine($"Total Files: {csvFilesList.Length}.");

            // create a log writer to recored errors.
            _logWriter = new StreamWriter($@"{_destinationFolder}\LogFile.txt") { AutoFlush = true };


            foreach (var fileName in csvFilesList)
            {


                WriteTweetIdsFile(fileName);

                _currentFileNumber++;

                Console.WriteLine("{0}) Processed: {1}", _currentFileNumber, fileName);

            }

            _logWriter.WriteLine($"Total extracted Tweet Ids : {_tweetIndex}.");

            _logWriter.Close();


        }

        private void WriteTweetIdsFile(string fileName)
        {

            var outputFileName = $@"{_destinationFolder}\TweetId_File_{_currentFileNumber}.txt";

            var writer = new StreamWriter(outputFileName) { AutoFlush = true };

            var reader = new StreamReader(fileName);

            var line = reader.ReadLine();

            while ((line = reader.ReadLine()) != null)
            {
                var parts = Regex.Split(line, Utilities.GetRegExpressionForComaOutSidesQuotes());

                var urlparts = parts[4].Replace("\"", string.Empty).Split('/');

                var tweetId = urlparts[urlparts.Length - 1];

                long outValue = 0;


                if (!long.TryParse(tweetId, out outValue))
                {
                    _logWriter.WriteLine($"Failed to extractId from : {line} in file = {fileName}");

                    continue;
                }


                writer.WriteLine(tweetId);

                _tweetIndex++;
            }

            writer.Close();

        }


        private string[] LoadCSVFilesList()
        {

            var filesList = Directory.GetFiles(_sourceFilesFolder, "*.csv", SearchOption.AllDirectories);

            return filesList;


        }



    }
}
