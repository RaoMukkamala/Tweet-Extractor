using System.Configuration;
using RestSharp.Authenticators;

namespace TweetDataExtractor.OAuthProvider
{
    public class TwitterTokenProvider
    {



        public static OAuth1Authenticator GetTwitterToken()
        {

            return OAuth1Authenticator.ForProtectedResource(ConfigManager.ConfigurationManagerInstance.ConsumerKey,
                ConfigManager.ConfigurationManagerInstance.ConsumerSecret,
                ConfigManager.ConfigurationManagerInstance.AccessToken,
                ConfigManager.ConfigurationManagerInstance.AccessTokenSecret
                );



        }



    }
}
