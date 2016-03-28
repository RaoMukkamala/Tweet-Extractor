using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetExtractorTestApp
{
    class TweetLocationUpdates
    {
        private readonly string _sourcePath;
        private readonly string _codedFilePath;
        private readonly string _targetPath;

        private Dictionary<int,string> origiDictionary = new Dictionary<int, string>(); 

        public TweetLocationUpdates(string sourcePath, string codedFilePath, string targetPath)
        {
            _sourcePath = sourcePath;
            _codedFilePath = codedFilePath;
            _targetPath = targetPath;
        }

        public void UpdateTweetDataWithLocation()
        {
            LoadOriginalTweetsData();

            var writer = new StreamWriter(_targetPath);

            var codedreader = new StreamReader(_codedFilePath);

            var headerLine = codedreader.ReadLine();

            headerLine = "longitude,latitude,timestamp," + headerLine;

            writer.WriteLine(headerLine);

            while (codedreader.Peek() >= 0)
            {
                var coderLine = codedreader.ReadLine();

                var id = GetId(coderLine);

                if (id == -1)
                    continue;

                var prefixString = origiDictionary.ContainsKey(id) ? origiDictionary[id] : ",,";

                var updatedLine = prefixString + "," + coderLine;

                writer.WriteLine(updatedLine);

                writer.Flush();

            }

            writer.Close();






        }

        private int GetId(string line)
        {
            var id = -1;


            try
            {
                var idstr = line.Split(',')[0];

                id= int.Parse(idstr);

            }
            catch (Exception exception)
            {
                Console.WriteLine("Input Line: " + line);

                Console.WriteLine(exception.Message);


            }

            return id;
        }

        private void LoadOriginalTweetsData()
        {
            //Alivelu-coded-tweet-format:
            // 17,10/25/12,"DC Area, be prepared for Hurricane Sandy coming up est. this Sunday @gz7 @yoanisey",Secondary,Information Messages,Other,,,

            // Original Tweets: 
            // 2,10/25/12,They named the hurricane that will be coming up the coast this weekend Frankenstorm.. #wtf,-74.54916984,40.61827357,25/10/12 21:18,2,0101000020E6100000F8AF419925A352C0CA909D96234F4440,0,IRRELEVANT,EN,madelynjoyy24,FEMALE,25/10/12 21:18,-3,1,,

            var lineNumber = 1;

            var reader = new StreamReader(_sourcePath);

            reader.ReadLine();

            string line;

            while ( (line = reader.ReadLine()) != null)
            {


                try
                {
                    var strings = line.Split(',');

                    var id = int.Parse(strings[0]);

                    var values = new[] { strings[3], strings[4], strings[5] };

                    var @join = string.Join(",", values);

                    //Console.WriteLine(@join);

                    Console.WriteLine(lineNumber);

                    lineNumber++;

                    if (!origiDictionary.ContainsKey(id))
                    {
                        origiDictionary.Add(id, @join);
                    }

                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);

                    Console.WriteLine(exception.StackTrace);

                    
                }                
            }





        }
    }
}
