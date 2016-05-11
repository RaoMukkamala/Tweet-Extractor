using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using TweetDataExtractor;
using TweetDataExtractor.Json;
using TweetDataExtractor.Misc;
using TweetDataExtractor.OAuthProvider;
using TweetDataExtractor.Tweet;
using TweetSharp;

namespace TweetExtractorTestApp
{
    public partial class Form1 : Form
    {
        private string _consumerKey = "f5SQkibZnpomMyfAiNF4H2w60";

        private string _consumerSecret = "jdKlnzBNi916Zq6rZhAScVvTxEnuATnORRWCcB7dnzBsHIrKS3";

        private string _accessToken = "1704540338-zl0QvTnp59JTXCKSGAyyx23qtSUxyX4lNOCLOr1";

        private string _accessTokenSecret = "qTtJt2ZOtCE1Y8hUK27wxprPmzs0gYXbrdyzSSf3GZfOB";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            try
            {

                // In v1.1, all API calls require authentication
                var service = new TwitterService("f5SQkibZnpomMyfAiNF4H2w60", "jdKlnzBNi916Zq6rZhAScVvTxEnuATnORRWCcB7dnzBsHIrKS3");

                service.AuthenticateWith("1704540338-zl0QvTnp59JTXCKSGAyyx23qtSUxyX4lNOCLOr1", "qTtJt2ZOtCE1Y8hUK27wxprPmzs0gYXbrdyzSSf3GZfOB");


                var options = new GetTweetOptions();

                options.Id = 261547135852478464;

                var twitterStatus = service.GetTweet(options);

                Console.WriteLine(twitterStatus.RawSource);

                var listOptions = new ListListMembersOptions();

                
                //service.(new GetTweetOptions().)

                //return;

                var tweets = service.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions());

                foreach (var tweet in tweets)
                {
                    Console.WriteLine("{0} says '{1}'", tweet.User.ScreenName, tweet.Text);
                }


            }
            catch (Exception exception)
            {

                throw;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {


            try
            {
               /*
                var encodedKey = "";

                var encodedSecret = "";

                var TWITTER_API_ENDPOINT = "";

                //get the application bearer token:
                var bearerCredentials = string.Format("{0}:{1}", encodedKey, encodedSecret);
                var encodedBearerCredentials = EncodeTo64(bearerCredentials);

                var client = new RestClient(TWITTER_API_ENDPOINT);
                var request = new RestRequest("oauth2/token", Method.POST);
                request.AddHeader("Authorization", string.Format("Basic {0}", encodedBearerCredentials));
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
                request.AddHeader("Host", "api.twitter.com");
                request.AddHeader("User-Agent", "FanManager");

                request.AddParameter("grant_type", "client_credentials");
                var response = client.Post<Dictionary<string, string>>(request);
                //bearer token comes back successfully

                //get rate limit status
                var request = new RestRequest("1.1/application/rate_limit_status.json");
                request.AddHeader("Authorization", string.Format("Bearer {0}", bearerToken));
                var client = new RestClient(TWITTER_API_ENDPOINT);
                var response = client.Get<RateLimitResponse>(request);
                * 
                */

                var ProtectedChars = "0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F, 0x50, 0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58, 0x59, 0x5A, 0x61, 0x62, 0x63, 0x64, 0x65, 0x66, 0x67, 0x68, 0x69, 0x6A, 0x6B, 0x6C, 0x6D, 0x6E, 0x6F, 0x70, 0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 0x77, 0x78, 0x79, 0x7A".Replace(" 0x", "%").Split(',');
                
                var client = new RestClient("https://api.twitter.com");

                client.Authenticator = OAuth1Authenticator.ForProtectedResource(
                    _consumerKey, _consumerSecret, _accessToken, _accessTokenSecret
                );

                var status = "A sample tweet using RestSharp API!";

                string newStatus = string.Empty;

                foreach (char c in status)
                {
                    var charBytes = Encoding.UTF8.GetBytes(c.ToString());
                    var tempText = string.Empty;

                    for (int i = 0; i < charBytes.Count(); i++)
                    {
                        byte b = charBytes[i];
                        string hex = "%" + b.ToString("X2");
                        tempText += hex;
                    }

                    if (ProtectedChars.Any(x => x == tempText))
                    {
                        newStatus += c;
                    }
                    else
                    {
                        newStatus += string.Format("{0:X2}", tempText);
                    }
                }

                var request = new RestRequest("/1.1/statuses/update.json", Method.POST);

                request = new RestRequest("/1.1/statuses/lookup.json", Method.GET);

                var idsString =
                    "261547135852478464,261547156538789888,261547107675152385,261547107566104576,261547108027482113,261547108375617536,261547109872959488,261547113383608320,261547122300702721";

                request.AddQueryParameter("id", idsString);

                request.AddQueryParameter("map", "true");

                
                //request.AddParameter(new Parameter { Name = "status", Type = ParameterType.GetOrPost, Value = newStatus });



                var response = client.Execute(request);

                //var jsonResponse = response.Content;



                JsonDeserializer deserializer = new JsonDeserializer();

                var jasonObj = deserializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(response);

                Console.WriteLine(response.Content);

                foreach (var keyValPair in jasonObj)
                {
                    foreach (var kvpair in keyValPair.Value)
                    {

                        if (string.IsNullOrEmpty(kvpair.Value))
                        {
                            Console.WriteLine(kvpair.Key + ": Null value");

                            continue;
                        }

                       
                        
                        Console.WriteLine(kvpair.Key);
                        
                        Console.WriteLine(kvpair.Value);


                        
                        var path = string.Format("Tweet_{0}.txt", kvpair.Key);



                        StreamWriter sw = new StreamWriter(path);

                        sw.WriteLine(kvpair.Value);

                        sw.Flush();

                        sw.Close();

                        try
                        {
                            var tObj = JsonConvert.DeserializeObject<TweetObject>(kvpair.Value);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Failed to deserailize the JSON for "+ kvpair.Key + " Exception: " + ex.Message);
                        }
                        

                    }

                    

                }




            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {


            try
            {
                var client = new RestClient("https://api.twitter.com");

                client.Authenticator = OAuth1Authenticator.ForProtectedResource(
                    _consumerKey, _consumerSecret, _accessToken, _accessTokenSecret
                );

                var request = new RestRequest("/1.1/application/rate_limit_status.json", Method.GET);

                //request.AddQueryParameter("resources", "statuses");
                //followers
                request.AddQueryParameter("resources", "followers,users");

                var response = client.Execute(request);

                var tObj = JsonConvert.DeserializeObject< TweetApiLimits >(response.Content);

                Console.WriteLine(response.Content);

                Console.WriteLine(tObj.resources.statuses.StatusLookUp.remaining);



            }
            catch (Exception exception)
            {
                
                throw;
            }



        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                var inputFilePath = @"H:\skydrive-rrm-outlook\research-work\Twitter-data-trials\hurricane-sandy-tweets\hurricane-sandy-tweets.tsv";

                var destinationFolder = @"H:\skydrive-rrm-outlook\research-work\Twitter-data-trials\data-trials\output";

                var splitter = new FileSplitter(inputFilePath, destinationFolder);

                //splitter.splitData();

                MessageBox.Show("done");

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.StackTrace);

                Console.WriteLine(exception.Message);
              
                
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            var filepath = "sample.txt";

            var sw = File.CreateText(filepath);

            sw.Close();

            var info = new FileInfo(filepath);

            Console.WriteLine("filesize: " + info.Length );



            
            
            
            StreamWriter writer = new StreamWriter(@"data\Sample.txt");

            writer.WriteLine("hello Raghava");

            writer.Close();


            var datestr = "Thu Oct 25 20:26:01 +0000 2012";

            var date = DateTime.ParseExact(datestr, Utilities.TwitterDateFormatString, new CultureInfo("en-us"));

            Console.WriteLine(date);



            Console.WriteLine(Utilities.ConvertUTCOffset("-15000"));


            float offset = -1800;

            var offseth = offset/3600;

            Console.WriteLine(offseth + " h");

            var sample = "-16200";

            

            var sampleInt = int.Parse(sample);

            Console.WriteLine(sampleInt);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TweetRequestManager requestManager = new TweetRequestManager();

            requestManager.ProcessTweetIdFiles();

            MessageBox.Show("Done");



        }

        private void button7_Click(object sender, EventArgs e)
        {

            var client = new RestClient("https://api.twitter.com");

            client.Authenticator = OAuth1Authenticator.ForProtectedResource(
                _consumerKey, _consumerSecret, _accessToken, _accessTokenSecret
            );

            
           var  request = new RestRequest("/1.1/statuses/lookup.json", Method.POST);

            var idsString =
                "261547135852478464,261547156538789888,261547107675152385,261547107566104576,261547108027482113,261547108375617536,261547109872959488,261547113383608320,261547122300702721";

            request.AddParameter("id", idsString);

            request.AddParameter("map", "true");


            //request.AddParameter(new Parameter { Name = "status", Type = ParameterType.GetOrPost, Value = newStatus });



            var response = client.Execute(request);

            //var jsonResponse = response.Content;



            JsonDeserializer deserializer = new JsonDeserializer();

            var jasonObj = deserializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(response);

            Console.WriteLine(response.Content);

            foreach (var keyValPair in jasonObj)
            {
                foreach (var kvpair in keyValPair.Value)
                {

                    if (string.IsNullOrEmpty(kvpair.Value))
                    {
                        Console.WriteLine(kvpair.Key + ": Null value");

                        continue;
                    }



                    Console.WriteLine(kvpair.Key);

                    Console.WriteLine(kvpair.Value);



                    var path = string.Format("Tweet_{0}.txt", kvpair.Key);



                    StreamWriter sw = new StreamWriter(path);

                    sw.WriteLine(kvpair.Value);

                    sw.Flush();

                    sw.Close();

                    try
                    {
                        var tObj = JsonConvert.DeserializeObject<TweetObject>(kvpair.Value);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed to deserailize the JSON for " + kvpair.Key + " Exception: " + ex.Message);
                    }


                }



            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //{"created_at":"Thu Oct 25 20:26:03 +0000 2012","id":261564298487279617,"id_str":"261564298487279617","text":"@sandybeales THANK YOU SO MUCH SANDY","source":"<a href=\"http://twitter.com/#!/download/ipad\" rel=\"nofollow\">Twitter for iPad</a>","truncated":false,"in_reply_to_status_id":null,"in_reply_to_status_id_str":null,"in_reply_to_user_id":426951343,"in_reply_to_user_id_str":"426951343","in_reply_to_screen_name":"sandybeales","user":{"id":625068802,"id_str":"625068802","name":"sara","screen_name":"sandy_beales","location":"","description":"hi","url":null,"entities":{"description":{"urls":[]}},"protected":false,"followers_count":1112,"friends_count":103,"listed_count":0,"created_at":"Mon Jul 02 22:50:42 +0000 2012","favourites_count":244,"utc_offset":null,"time_zone":null,"geo_enabled":false,"verified":false,"statuses_count":6115,"lang":"en","contributors_enabled":false,"is_translator":false,"is_translation_enabled":false,"profile_background_color":"9AE4E8","profile_background_image_url":"http://abs.twimg.com/images/themes/theme16/bg.gif","profile_background_image_url_https":"https://abs.twimg.com/images/themes/theme16/bg.gif","profile_background_tile":false,"profile_image_url":"http://pbs.twimg.com/profile_images/527246158440460288/k9FeKK-w_normal.jpeg","profile_image_url_https":"https://pbs.twimg.com/profile_images/527246158440460288/k9FeKK-w_normal.jpeg","profile_banner_url":"https://pbs.twimg.com/profile_banners/625068802/1383500048","profile_link_color":"0084B4","profile_sidebar_border_color":"BDDCAD","profile_sidebar_fill_color":"DDFFCC","profile_text_color":"333333","profile_use_background_image":true,"default_profile":false,"default_profile_image":false,"following":false,"follow_request_sent":false,"notifications":false},"geo":null,"coordinates":null,"place":null,"contributors":null,"retweet_count":0,"favorite_count":0,"entities":{"hashtags":[],"symbols":[],"user_mentions":[{"screen_name":"sandybeales","name":"Sandy Beales","id":426951343,"id_str":"426951343","indices":[0,12]}],"urls":[]},"favorited":false,"retweeted":false,"lang":"en"}
            //{"created_at":"Thu Oct 25 20:26:01 +0000 2012","id":261564290312572928,"id_str":"261564290312572928","text":"hurricane winds and rain better not screw up the Cowboys game on sunday!!","source":"<a href=\"http://twitter.com\" rel=\"nofollow\">Twitter Web Client</a>","truncated":false,"in_reply_to_status_id":null,"in_reply_to_status_id_str":null,"in_reply_to_user_id":null,"in_reply_to_user_id_str":null,"in_reply_to_screen_name":null,"user":{"id":251911060,"id_str":"251911060","name":"James Haley","screen_name":"jhaleyIII","location":"Atlantic, VA","description":"","url":null,"entities":{"description":{"urls":[]}},"protected":false,"followers_count":355,"friends_count":388,"listed_count":1,"created_at":"Mon Feb 14 02:53:29 +0000 2011","favourites_count":2305,"utc_offset":-14400,"time_zone":"Eastern Time (US & Canada)","geo_enabled":false,"verified":false,"statuses_count":11514,"lang":"en","contributors_enabled":false,"is_translator":false,"is_translation_enabled":false,"profile_background_color":"F00C0C","profile_background_image_url":"http://pbs.twimg.com/profile_background_images/399247911/Cape_Fear_Image.jpg","profile_background_image_url_https":"https://pbs.twimg.com/profile_background_images/399247911/Cape_Fear_Image.jpg","profile_background_tile":true,"profile_image_url":"http://pbs.twimg.com/profile_images/378800000236523419/b75642a0d9c1f4f7265f984fbdbac61f_normal.jpeg","profile_image_url_https":"https://pbs.twimg.com/profile_images/378800000236523419/b75642a0d9c1f4f7265f984fbdbac61f_normal.jpeg","profile_banner_url":"https://pbs.twimg.com/profile_banners/251911060/1352909456","profile_link_color":"0084B4","profile_sidebar_border_color":"140DDB","profile_sidebar_fill_color":"DDE7EB","profile_text_color":"333333","profile_use_background_image":true,"default_profile":false,"default_profile_image":false,"following":false,"follow_request_sent":false,"notifications":false},"geo":null,"coordinates":null,"place":null,"contributors":null,"retweet_count":1,"favorite_count":0,"entities":{"hashtags":[],"symbols":[],"user_mentions":[],"urls":[]},"favorited":false,"retweeted":false,"lang":"en"}



            var reader = new StreamReader(@"H:\skydrive-rrm-outlook\research-work\Twitter-data-trials\Extracted-Tweets\trouble-shooting\sample-json.txt");

            var writer =
                new StreamWriter(
                    @"H:\skydrive-rrm-outlook\research-work\Twitter-data-trials\Extracted-Tweets\trouble-shooting\sample-export.txt");

            while (reader.Peek() > 0)
            {

                var json = reader.ReadLine();

                try
                {
                    var tObj = JsonConvert.DeserializeObject<TweetObject>(json);

                    
                 

                    // Writing the csv files for alivelu
                    WriteTweetExport(tObj, writer);


                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to deserailize the JSON for Exception: " + ex.Message);

                   
                }






            }

            writer.Close();



        }

        private void WriteTweetExport(TweetObject tweetobj, StreamWriter _exportWriter)
        {
            var tempDateTime = new DateTime(1900, 1, 1);
            // TweetId,TweetDate,Text,Truncated,RetweetCount, FavoriteCount,Retweeted,Favorited,TweetLang,UserId,
            // UserName, UserLocation, UserFollowerCount,UserFriendsCount,UserCreatedDate,UserFavoritesCount,UserTimeZone,UserUTCOffset,UserStatusesCount,UserLang");

            // TweetId,TweetDate,Text,Truncated,RetweetCount,
            _exportWriter.Write("{0},{1},{2},{3},{4},", tweetobj.id, Utilities.TryParseTwitterDateTimeString(tweetobj.created_at, tempDateTime), ProcessText(tweetobj.text), tweetobj.truncated, tweetobj.retweet_count);

            _exportWriter.Flush();

            // FavoriteCount,Retweeted,Favorited,TweetLang,UserId
            _exportWriter.Write("{0},{1},{2},{3},{4},", tweetobj.favorite_count, tweetobj.retweeted, tweetobj.favorited, tweetobj.lang, tweetobj.user.id);

            _exportWriter.Flush();

            // UserName, UserLocation, UserFollowerCount,UserFriendsCount,UserCreatedDate

            _exportWriter.Write("{0},{1},{2},{3},{4},", tweetobj.user.name, ProcessText(tweetobj.user.location), tweetobj.user.followers_count, tweetobj.user.friends_count,
                Utilities.TryParseTwitterDateTimeString(tweetobj.user.created_at, tempDateTime));

            _exportWriter.Flush();

            // UserFavoritesCount,UserTimeZone,UserUTCOffset,UserStatusesCount,UserLang

            _exportWriter.Write("{0},{1},{2},{3},{4}", tweetobj.user.favourites_count, ProcessText(tweetobj.user.time_zone),
                Utilities.ConvertUTCOffset(tweetobj.user.utc_offset), tweetobj.user.statuses_count, ProcessText(tweetobj.user.lang));

            _exportWriter.Flush();

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

        private void button9_Click(object sender, EventArgs e)
        {

            try
            {
                var originalPath = @"C:\Users\Raghava\Dropbox\cbs-research\Solutions\itu\alivelu-tweets-sandy\Original-cleaned-tweets-2.csv";

                var codedPath = @"C:\Users\Raghava\Dropbox\cbs-research\Solutions\itu\alivelu-tweets-sandy\Coded-Tweets-Alivelu.csv";

                var targetPath = @"C:\Users\Raghava\Dropbox\cbs-research\Solutions\itu\alivelu-tweets-sandy\Coded-Tweets-Alivelu-updated.csv";

                TweetLocationUpdates updates = new TweetLocationUpdates(originalPath, codedPath, targetPath);

                updates.UpdateTweetDataWithLocation();

                MessageBox.Show("done");

            }
            catch (Exception exception)
            {

                MessageBox.Show((exception.Message));
            }


        }

        private void button10_Click(object sender, EventArgs e)
        {

            // UserId: 1059896820, 475525677, 3274840891
            var userId = "475525677";

            try
            {
                var client = new RestClient("https://api.twitter.com");

                

                client.Authenticator = OAuth1Authenticator.ForProtectedResource(
                    _consumerKey, _consumerSecret, _accessToken, _accessTokenSecret
                );

                client.Authenticator = TwitterTokenProvider.GetTwitterToken();

                var request = new RestRequest("/1.1/followers/ids.json", Method.GET);

                request.AddQueryParameter("user_id", userId);

                request.AddQueryParameter("count", "5000");

                request.AddQueryParameter("cursor", "-1");



                var response = client.Execute(request);

                Console.WriteLine(response.Content);

               var tObj = JsonConvert.DeserializeObject<UserFollowersRootObject>(response.Content);

                Console.WriteLine(tObj.ids.Count);



            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }


        }

        private void button11_Click(object sender, EventArgs e)
        {

            object dtObj = (object) DateTime.Now;

            MessageBox.Show(dtObj.ToString());

        }

    }
}
