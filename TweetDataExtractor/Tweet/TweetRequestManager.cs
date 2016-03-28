using System;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using TweetDataExtractor.Json;
using TweetDataExtractor.OAuthProvider;

namespace TweetDataExtractor.Tweet
{
    public class TweetRequestManager
    {

        private DateTime _fileProcessStartTime;

        




        public void ProcessTweetIdFiles()
        {
            


            // First check whether necessary folder are created 
            CheckDirectories();


            // Run an infinite loop to process all the files.
            while (true)
            {
                try
                {
                    // Critical checks to just see if you will cross the limit..

                    var remainingLimit = GetTwitterStatusLookupRemainingLimit();

                    if (remainingLimit < ConfigManager.ConfigurationManagerInstance.AllowedRemainingLimit)
                    {
                        Console.WriteLine("{0}: sleeping for {0} minites due to lesser remianing limit: {1}", DateTime.Now,
                            ConfigManager.ConfigurationManagerInstance.TimeFrame, remainingLimit);

                        Thread.Sleep(ConfigManager.ConfigurationManagerInstance.TimeFrame * 60 * 1000);

                    }


                    var filePath = GetFilePath();
                    // if no filepath then exit
                    if (string.IsNullOrEmpty(filePath))
                    {
                        break;
                    }

                    _fileProcessStartTime = DateTime.Now;

                    //Console.WriteLine("fileProcessStartTime:" + _fileProcessStartTime);

                    // Do some processing of the file...
                    var fileProcessor = new TweetIdFileProcessor(filePath);

                    fileProcessor.ProcessFile();




                    // after file processing finished, check the timing.
                    var span = DateTime.Now - _fileProcessStartTime;

                    //Console.WriteLine("After finishing the file processing: " + DateTime.Now);

                    if (span.TotalMinutes < ConfigManager.ConfigurationManagerInstance.TimeFrame)
                    {
                        var waitspan = _fileProcessStartTime.AddMinutes(ConfigManager.ConfigurationManagerInstance.TimeFrame) - DateTime.Now;

                        Console.WriteLine("{0}: Before going to sleep for (mins) : {1}", DateTime.Now, waitspan.TotalMinutes);

                        // sleep for the time till it reaches 15 mins
                        Thread.Sleep(waitspan);
                    }

                }
                catch (Exception exception)
                {

                    Console.Write(
                        "{0}: Exception occured. The thread will put to sleep for 15 mins. Error Message: {1}",
                        DateTime.Now, exception.StackTrace);

                    Thread.Sleep(ConfigManager.ConfigurationManagerInstance.TimeFrame * 60 * 1000);
                }



            }





        }

        private void CheckDirectories()
        {

            
            if (!Directory.Exists(ConfigManager.ConfigurationManagerInstance.TweetJsonFolderPath))
            {
                Directory.CreateDirectory(ConfigManager.ConfigurationManagerInstance.TweetJsonFolderPath);
            }

            if (!Directory.Exists(ConfigManager.ConfigurationManagerInstance.ExportFolderPath))
            {
                Directory.CreateDirectory(ConfigManager.ConfigurationManagerInstance.ExportFolderPath);
            }

            if (!Directory.Exists(ConfigManager.ConfigurationManagerInstance.LogsFolderPath))
            {
                Directory.CreateDirectory(ConfigManager.ConfigurationManagerInstance.LogsFolderPath);
            }




        }


        private int GetTwitterStatusLookupRemainingLimit()
        {

            try
            {
                var client = new RestClient("https://api.twitter.com")
                {
                    Authenticator = TwitterTokenProvider.GetTwitterToken()
                };

                var request = new RestRequest("/1.1/application/rate_limit_status.json", Method.GET);

                request.AddQueryParameter("resources", "statuses");

                var response = client.Execute(request);

                var tObj = JsonConvert.DeserializeObject<TweetApiLimits>(response.Content);

                return tObj.resources.statuses.StatusLookUp.remaining;

                Console.WriteLine(tObj.resources.statuses.StatusLookUp.remaining);



            }
            catch (Exception exception)
            {

                throw;
            }

        }


        private string GetFilePath()
        {
            var inputfolder = ConfigManager.ConfigurationManagerInstance.InputFolderPath;

            var dirInfo = new DirectoryInfo(inputfolder);

            var fileInfos = dirInfo.GetFiles();

            return fileInfos.Length > 0 ? fileInfos[0].FullName : string.Empty;
        }



    }
}
