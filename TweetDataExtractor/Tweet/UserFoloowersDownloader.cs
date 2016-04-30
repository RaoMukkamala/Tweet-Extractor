using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using TweetDataExtractor.Json;
using TweetDataExtractor.OAuthProvider;

namespace TweetDataExtractor.Tweet
{
    public class UserFoloowersDownloader
    {
        // bucket time in seconds.
        private int _bucketTimeSecs = 900;

        private DateTime _firstCallTime;

        private int _numberOfAllowedCallsInBucket = 15;

        private long resetUnixTime = 0;















        private int GetRemainingLimitForFollowesIds()
        {

            try
            {
                var client = new RestClient("https://api.twitter.com")
                {
                    Authenticator = TwitterTokenProvider.GetTwitterToken()
                };

                var request = new RestRequest("/1.1/application/rate_limit_status.json", Method.GET);

                request.AddQueryParameter("resources", "followers");

                var response = client.Execute(request);

                var apiLimitObject = JsonConvert.DeserializeObject<TweetApiLimits>(response.Content);
                
                // update the next rest time

                resetUnixTime = apiLimitObject.resources.followers.FollowersIds.reset;

                return apiLimitObject.resources.followers.FollowersIds.remaining;

            }
            catch (Exception exception)
            {

                Console.WriteLine(exception.Message);

                throw;
            }

        }

        private long GetUserId()
        {

            var query =
                "select top 1 UserId FROM [ChennaiFloods].[dbo].[TwitterUsers] where [NeedToExtractFollowers] = 1";

            try
            {
                using (var connection = new SqlConnection(ConfigManager.ConfigurationManagerInstance.DbConnString))
                {
                    var command = new SqlCommand
                    {
                        CommandText = query,
                        CommandType = CommandType.Text,
                        Connection = connection,
                        CommandTimeout = 0
                    };


                    connection.Open();

                    var userId =  command.ExecuteScalar();

                    return (long?) userId ?? 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to Get UserId from  database. Error: {ex.Message}");

                throw;
            }







        }


    }
}
