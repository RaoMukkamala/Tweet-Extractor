using Newtonsoft.Json;

namespace TweetDataExtractor.Json
{

    public class TweetApiLimits
    {
        public RateLimitContext rate_limit_context { get; set; }
        public Resources resources { get; set; }
    }


    public class RateLimitContext
    {
        public string access_token { get; set; }
    }

    public class StatusesRetweetersIds
    {
        public int limit { get; set; }
        public int remaining { get; set; }
        public int reset { get; set; }
    }

    public class StatusesRetweetsOfMe
    {
        public int limit { get; set; }
        public int remaining { get; set; }
        public int reset { get; set; }
    }

    public class StatusesHomeTimeline
    {
        public int limit { get; set; }
        public int remaining { get; set; }
        public int reset { get; set; }
    }

    public class StatusesShowId
    {
        public int limit { get; set; }
        public int remaining { get; set; }
        public int reset { get; set; }
    }

    public class StatusesUserTimeline
    {
        public int limit { get; set; }
        public int remaining { get; set; }
        public int reset { get; set; }
    }

    public class StatusesFriends
    {
        public int limit { get; set; }
        public int remaining { get; set; }
        public int reset { get; set; }
    }

    public class StatusesRetweetsId
    {
        public int limit { get; set; }
        public int remaining { get; set; }
        public int reset { get; set; }
    }

    public class StatusesMentionsTimeline
    {
        public int limit { get; set; }
        public int remaining { get; set; }
        public int reset { get; set; }
    }

    public class StatusesOembed
    {
        public int limit { get; set; }
        public int remaining { get; set; }
        public int reset { get; set; }
    }

    public class StatusesLookup
    {
        public int limit { get; set; }
        public int remaining { get; set; }
        public int reset { get; set; }
    }

    public class Statuses
    {
        [JsonProperty("/statuses/retweeters/ids")]
        public StatusesRetweetersIds ReTweeterIds { get; set; }

        [JsonProperty("/statuses/retweets_of_me")]
        public StatusesRetweetsOfMe ReTweetsOfMe { get; set; }

        [JsonProperty("/statuses/home_timeline")]
        public StatusesHomeTimeline HomeTimeLine { get; set; }

        [JsonProperty("/statuses/show/:id")]
        public StatusesShowId ShowId { get; set; }

        [JsonProperty("/statuses/user_timeline")]
        public StatusesUserTimeline UserTimeline { get; set; }
        [JsonProperty("/statuses/friends")]
        public StatusesFriends Friends { get; set; }
        [JsonProperty("/statuses/retweets/:id")]
        public StatusesRetweetsId RetweetsId { get; set; }
        [JsonProperty("/statuses/mentions_timeline")]
        public StatusesMentionsTimeline MentionsTimeline { get; set; }
        [JsonProperty("/statuses/oembed")]
        public StatusesOembed OEmbed { get; set; }

        [JsonProperty("/statuses/lookup")]
        public StatusesLookup StatusLookUp { get; set; }
    }

    public class Resources
    {
        public Statuses statuses { get; set; }
    }


}
