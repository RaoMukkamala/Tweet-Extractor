using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using TweetDataExtractor.Json;
using TweetDataExtractor.OAuthProvider;
using TweetDataExtractor.SQL;

namespace TweetDataExtractor.Tweet
{
    class TweetDownloader
    {
        private readonly string _idsString;
        private readonly StreamWriter _jsonWriter;
        private readonly StreamWriter _errorWriter;
        private readonly StreamWriter _nullIdWriter;
        private readonly StreamWriter _exportWriter;
        //private readonly string _destinationDirectory;
        private Dictionary<string, Dictionary<string, string>> _jasonObj;

        private readonly TweetStats _stats;

        public TweetDownloader(string idsString, StreamWriter jsonWriter, StreamWriter errorWriter, 
            StreamWriter nullIdWriter, StreamWriter exportWriter, ref TweetStats stats)
        {
            _idsString = idsString;
            _jsonWriter = jsonWriter;
            _errorWriter = errorWriter;
            _nullIdWriter = nullIdWriter;
            _exportWriter = exportWriter;
           // _destinationDirectory = destinationDirectory;
            _stats = stats;
        }


        public void DownloadTweets()
        {
            
            // First call the Twitter API to get the tweets.

            var result = InitiatePostRequestToTwitter();
            // If there is some error, then return.
            if (!result)
            {
                _stats.UnprocessedTweetCount += 100;

                return;
            }

            foreach (var keyValPair in _jasonObj)
            {
                foreach (var kvpair in keyValPair.Value)
                {

                    if (string.IsNullOrEmpty(kvpair.Value))
                    {
                        // then write to the nullId writer and continue
                        
                        _nullIdWriter.WriteLine(kvpair.Key);

                        _nullIdWriter.Flush();

                        _stats.EmptyTweetCount++;

                        continue;
                    }

                    // write the json string to writer
                    _jsonWriter.WriteLine(kvpair.Value);

                    _jsonWriter.Flush();

                    _stats.ExtractedTweetCount++;

                   

                    

                    try
                    {
                        var tObj = JsonConvert.DeserializeObject<TweetObject>(kvpair.Value);

                        if (ConfigManager.ConfigurationManagerInstance.PersistToDatabase)
                        {
                            var tweetToDb = new TweetToDb(tObj);

                            tweetToDb.InsertTweetToDb(_errorWriter);
                            
                        }

                        // Writing the csv files for alivelu
                        WriteTweetExport(tObj);


                    }
                    catch (Exception ex)
                    {
                        _errorWriter.WriteLine("Failed to deserailize the JSON for " + kvpair.Key + " Exception: " +
                                               ex.Message);

                        _stats.TweetsWithDataErrors++;
                    }


                }



            }





        }


        private bool InitiatePostRequestToTwitter()
        {
            try
            {
                var client = new RestClient("https://api.twitter.com")
                {
                    Authenticator = TwitterTokenProvider.GetTwitterToken()
                };


                var request = new RestRequest("/1.1/statuses/lookup.json", Method.POST);


                request.AddParameter("id", _idsString);

                request.AddParameter("map", "true");

                var response = client.Execute(request);


                JsonDeserializer deserializer = new JsonDeserializer();
                
                _jasonObj = deserializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(response);

                return true;

            }
            catch (Exception exception)
            {

                var message = string.Format("Failed to get Tweets data! Error message: {0}", exception.Message);

                _errorWriter.WriteLine(message);

                _errorWriter.WriteLine(_idsString);

                _errorWriter.Flush();

                return false;

            }


            



        }

        private void WriteTweetExport(TweetObject tweetobj)
        {
            var tempDateTime = new DateTime(1900, 1, 1);
            // TweetId,TweetDate,Text,Truncated,RetweetCount, FavoriteCount,Retweeted,Favorited,TweetLang,UserId,
            // UserName, UserLocation, UserFollowerCount,UserFriendsCount,UserCreatedDate,UserFavoritesCount,UserTimeZone,UserUTCOffset,UserStatusesCount,UserLang,Hashtags");

            // TweetId,TweetDate,Text,Truncated,RetweetCount,
            _exportWriter.Write("{0},{1},{2},{3},{4},", tweetobj.id, Utilities.TryParseTwitterDateTimeString(tweetobj.created_at, tempDateTime), ProcessText(tweetobj.text), tweetobj.truncated, tweetobj.retweet_count);


            // FavoriteCount,Retweeted,Favorited,TweetLang,UserId
            _exportWriter.Write("{0},{1},{2},{3},{4},", tweetobj.favorite_count,tweetobj.retweeted,tweetobj.favorited,tweetobj.lang, tweetobj.user.id);


            // UserName, UserLocation, UserFollowerCount,UserFriendsCount,UserCreatedDate

            _exportWriter.Write("{0},{1},{2},{3},{4},", ProcessText(tweetobj.user.name), ProcessText(tweetobj.user.location),tweetobj.user.followers_count,tweetobj.user.friends_count,
                Utilities.TryParseTwitterDateTimeString(tweetobj.user.created_at, tempDateTime));

            // UserFavoritesCount,UserTimeZone,UserUTCOffset,UserStatusesCount,UserLang,Hashtags

            _exportWriter.Write("{0},{1},{2},{3},{4},{5}", tweetobj.user.favourites_count,
                ProcessText(tweetobj.user.time_zone),
                Utilities.ConvertUTCOffset(tweetobj.user.utc_offset), tweetobj.user.statuses_count,
                ProcessText(tweetobj.user.lang), Utilities.ExtractHashTags(tweetobj));

            _exportWriter.WriteLine();

            _exportWriter.Flush();

        }

        private string ProcessText(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            var newValue = value.Replace(Environment.NewLine, string.Empty);

            newValue = newValue.Replace(",", string.Empty);


            newValue = newValue.Replace("\"", string.Empty);

            
            newValue = newValue.Replace(@"\n", string.Empty);

            newValue = newValue.Replace(@"\r", string.Empty);

            newValue = newValue.Replace("\x000A", string.Empty);

            newValue = newValue.Replace("\x000D\x000A", string.Empty);

            newValue = newValue.Replace("\x000D", string.Empty);

            newValue = newValue.Replace("\x0A", string.Empty);

            newValue = newValue.Replace("\x0D", string.Empty);
            
            return string.Format("\"{0}\"", newValue);

        }

    }
}
