using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Radion6DataImport
{
    class TweetIdExtractorTUT
    {

        private string _destinationFolder = @"F:\raghava-work\TUT-CobWeb\twitter-data\Extracted-TweetData\input";

        private string _sourceFilesFolder = @"F:\raghava-work\TUT-CobWeb\twitter-data\Extracted-TweetData\input";

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
                //var parts = Regex.Split(line, Utilities.GetRegExpressionForComaOutSidesQuotes());

                var parts = line.Split(',');

                if (parts.Length < 2)
                {
                    _logWriter.WriteLine($"Failed to extractId from : {line} in file = {fileName}");

                    continue;

                }

                var tweetId = parts[1];

                long outValue = 0;


                if (!long.TryParse(tweetId, out outValue))
                {
                    _logWriter.WriteLine($"Failed to transform tweetId: {tweetId} into integer from : {line} in file = {fileName}");

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
