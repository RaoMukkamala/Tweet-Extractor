using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetDataExtractor.Tweet;

namespace TweetExtractorConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TweetRequestManager requestManager = new TweetRequestManager();

            requestManager.ProcessTweetIdFiles();
           
            Console.WriteLine("Done");

            Console.ReadLine();
        }
    }
}
