using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetDataExtractor
{
    class ConfigManager
    {
        private static readonly object SyncRoot = new Object();

        private static volatile ConfigManager _configManagerInstance;

        public  string ConsumerKey;

        public  string ConsumerSecret;

        public  string AccessToken;

        public  string AccessTokenSecret;

        public string InputFolderPath;

        public string OutputFolderPath;

        public string TweetJsonFolderPath;

        public string ExportFolderPath;

        public string LogsFolderPath;

        public string CompletedFolderPath;

        public int TimeFrame;

        public int AllowedRemainingLimit;

        public string DbConnString;

        public bool PersistToDatabase;


        private ConfigManager()
        {
            try
            {
                ConsumerKey = ConfigurationManager.AppSettings["ConsumerKey"];

                ConsumerSecret = ConfigurationManager.AppSettings["ConsumerKeySecret"];

                AccessToken = ConfigurationManager.AppSettings["AccessToken"];

                AccessTokenSecret = ConfigurationManager.AppSettings["AccessTokenSecret"];

                InputFolderPath = ConfigurationManager.AppSettings["InputFolderPath"];


                OutputFolderPath = ConfigurationManager.AppSettings["OutputFolderPath"];


                TweetJsonFolderPath = string.Format(@"{0}\tweets_json", OutputFolderPath);

                ExportFolderPath = string.Format(@"{0}\exports_csv", OutputFolderPath);

                LogsFolderPath = string.Format(@"{0}\logs", OutputFolderPath);


                CompletedFolderPath = ConfigurationManager.AppSettings["CompletedFolderPath"];

                TimeFrame = Utilities.TryParseIntString(ConfigurationManager.AppSettings["TimeFrame"], 15);

                AllowedRemainingLimit = Utilities.TryParseIntString(ConfigurationManager.AppSettings["AllowedRemainingLimit"], 160);

                DbConnString = ConfigurationManager.AppSettings["DbConnString"];

                PersistToDatabase = Utilities.TryParseBooleanValues(ConfigurationManager.AppSettings["PersistToDatabase"]);


            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to load configuration! check the settings in app.config. Error Message: " + ex.Message);

            }

        }


        public static ConfigManager ConfigurationManagerInstance
        {
            get
            {
                if (_configManagerInstance == null)
                {
                    {
                        lock (SyncRoot)
                        {
                            if (_configManagerInstance == null)
                                _configManagerInstance = new ConfigManager();
                        }
                    }
  
                }
                return _configManagerInstance;

            }
        }





    }
}
