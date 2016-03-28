using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetDataExtractor.Misc
{
    public class FileSplitter
    {
        private readonly string _sourceFile;

        private readonly string _destinationFolder;

        private const int MaxRecordCount = 15000;

        private int _currentRecordNumber = 0;

        private int _fileIndex = 0;

        private FileInfo _fileInfo = null;

        private DirectoryInfo _destinationDir;

        private StreamReader mainFileReader;

        private StreamWriter _streamWriter;
        public FileSplitter(string sourceFile, string destinationFolder)
        {
            _sourceFile = sourceFile;

            _destinationFolder = destinationFolder;

           _fileInfo = new FileInfo(_sourceFile);

            _destinationDir = new DirectoryInfo(_destinationFolder);

            mainFileReader = new StreamReader(_sourceFile);

            // initialize the stream writer..
            _streamWriter = new StreamWriter(GetFullFileName());



        }

        public void splitData()
        {
            // Read the file line by line...
            while (mainFileReader.Peek() > 0)
            {
                var dataline = mainFileReader.ReadLine();

                var dataArray = dataline.Split('\t');

                WriteTweetId(dataArray[0]);
            }

            // finally close, stream readers and writers
            _streamWriter.Flush();

            _streamWriter.Close();

            mainFileReader.Close();





        }

        private void WriteTweetId(string tweetId)
        {
            _streamWriter.WriteLine(tweetId);

            _streamWriter.Flush();

            _currentRecordNumber++;

            if (_currentRecordNumber == MaxRecordCount)
            {
                // Close the file and reset counter.

                _streamWriter.Close();

                _fileIndex++;

                _currentRecordNumber = 0;

                _streamWriter = new StreamWriter(GetFullFileName());
            }


        }


        private string GetFullFileName()
        {
            return string.Format(@"{0}\File_{1}.txt", _destinationFolder, _fileIndex);


        }
    }
}
