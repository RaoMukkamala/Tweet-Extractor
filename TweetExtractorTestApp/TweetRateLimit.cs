using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TweetExtractorTestApp
{
    //public class RateLimitContext
    //{
    //    public string access_token { get; set; }
    //}

    //public class ListsList
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class ListsMemberships
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class ListsSubscribersShow
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class ListsMembers
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class ListsSubscriptions
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class ListsShow
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class ListsOwnerships
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class ListsSubscribers
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class ListsMembersShow
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class ListsStatuses
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class Lists
    //{
    //    [JsonProperty("/lists/list")]
    //    public ListsList ListsList { get; set; }

    //    [JsonProperty("/lists/memberships")]
    //    public ListsMemberships ListsMemberships { get; set; }

    //    [JsonProperty("/lists/subscribers/show")]
    //    public ListsSubscribersShow ListsSubscribersShow { get; set; }

    //    [JsonProperty("/lists/members")]
    //    public ListsMembers ListsMembers { get; set; }

    //    [JsonProperty("/lists/subscriptions")]
    //    public ListsSubscriptions ListsSubscriptions { get; set; }

    //    [JsonProperty("/lists/show")]
    //    public ListsShow ListsShow { get; set; }

    //    [JsonProperty("/lists/ownerships")]
    //    public ListsOwnerships ListsOwnerships { get; set; }

    //    [JsonProperty("/lists/subscribers")]
    //    public ListsSubscribers ListsSubscribers { get; set; }

    //    [JsonProperty("/lists/members/show")]
    //    public ListsMembersShow ListsMembersShow { get; set; }
    //    [JsonProperty("/lists/statuses")]
    //    public ListsStatuses ListsStatuses { get; set; }
    //}

    //public class ApplicationRateLimitStatus
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class TweetApplication
    //{
    //    [JsonProperty("/application/rate_limit_status")]
    //    public ApplicationRateLimitStatus ApplicationRateLimitStatus { get; set; }
    //}

    //public class MutesUsersList
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class MutesUsersIds
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class Mutes
    //{
    //    [JsonProperty("/mutes/users/list")]
    //    public MutesUsersList MutesUsersList { get; set; }
    //    [JsonProperty("/mutes/users/ids")]
    //    public MutesUsersIds MutesUsersIds { get; set; }
    //}

    //public class FriendshipsOutgoing
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class FriendshipsList
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class FriendshipsNoRetweetsIds
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class FriendshipsLookup
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class FriendshipsIncoming
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class FriendshipsShow
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class Friendships
    //{
    //    [JsonProperty("/friendships/outgoing")]
    //    public FriendshipsOutgoing FriendshipsOutgoing { get; set; }
    //    [JsonProperty("/friendships/list")]
    //    public FriendshipsList FriendshipsList { get; set; }
    //    [JsonProperty("/friendships/no_retweets/ids")]
    //    public FriendshipsNoRetweetsIds FriendshipsNoRetweetsIds { get; set; }
    //    [JsonProperty("/friendships/lookup")]
    //    public FriendshipsLookup FriendshipsLookup { get; set; }
    //    [JsonProperty("/friendships/incoming")]
    //    public FriendshipsIncoming FriendshipsIncoming { get; set; }
    //    [JsonProperty("/friendships/show")]
    //    public FriendshipsShow FriendshipsShow { get; set; }
    //}

    //public class AuthCsrfToken
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class Auth
    //{
    //    [JsonProperty("/auth/csrf_token")]
    //    public AuthCsrfToken AuthCsrfToken { get; set; }
    //}

    //public class BlocksList
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class BlocksIds
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class Blocks
    //{
    //    [JsonProperty("/blocks/list")]
    //    public BlocksList BlocksList { get; set; }
    //    [JsonProperty("/blocks/ids")]
    //    public BlocksIds BlocksIds { get; set; }
    //}

    //public class GeoSimilarPlaces
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class GeoIdPlaceId
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class GeoReverseGeocode
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class GeoSearch
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class Geo
    //{
    //    [JsonProperty("/geo/similar_places")]
    //    public GeoSimilarPlaces GeoSimilarPlaces { get; set; }
    //    [JsonProperty("/geo/id/:place_id")]
    //    public GeoIdPlaceId GeoIdPlaceId { get; set; }
    //    [JsonProperty("/geo/reverse_geocode")]
    //    public GeoReverseGeocode GeoReverseGeocode { get; set; }
    //    [JsonProperty("/geo/search")]
    //    public GeoSearch GeoSearch { get; set; }
    //}

    //public class UsersReportSpam
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class UsersShowId
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class UsersSearch
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class UsersSuggestionsSlug
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class UsersDerivedInfo
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class UsersProfileBanner
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class UsersSuggestionsSlugMembers
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class UsersLookup
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class UsersSuggestions
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class Users
    //{
    //    [JsonProperty("/users/report_spam")]
    //    public UsersReportSpam ReportSpam { get; set; }
    //    [JsonProperty("/users/show/:id")]
    //    public UsersShowId UsersShowId { get; set; }
    //    [JsonProperty("/users/search")]
    //    public UsersSearch UsersSearch { get; set; }
    //    [JsonProperty("/users/suggestions/:slug")]
    //    public UsersSuggestionsSlug SuggestionsSlug { get; set; }
    //    [JsonProperty("/users/derived_info")]
    //    public UsersDerivedInfo UsersDerivedInfo { get; set; }
    //    [JsonProperty("/users/profile_banner")]
    //    public UsersProfileBanner UsersProfileBanner { get; set; }
    //    [JsonProperty("/users/suggestions/:slug/members")]
    //    public UsersSuggestionsSlugMembers UsersSuggestionsSlugMembers { get; set; }
    //    [JsonProperty("/users/lookup")]
    //    public UsersLookup UsersLookup { get; set; }
    //    [JsonProperty("/users/suggestions")]
    //    public UsersSuggestions UsersSuggestions { get; set; }
    //}

    //public class FollowersIds
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class FollowersList
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class Followers
    //{
    //    [JsonProperty("/followers/ids")]
    //    public FollowersIds FollowersIds { get; set; }
    //    [JsonProperty("/followers/list")]
    //    public FollowersList FollowersList
    //    { get; set; }
    //}

    //public class CollectionsList
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class CollectionsEntries
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class CollectionsShow
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class Collections
    //{
    //    [JsonProperty("/collections/list")]
    //    public CollectionsList CollectionsList { get; set; }
    //    [JsonProperty("/collections/entries")]
    //    public CollectionsEntries CollectionsEntries { get; set; }
    //    [JsonProperty("/collections/show")]
    //    public CollectionsShow CollectionsShow { get; set; }
    //}

    //public class StatusesRetweetersIds
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class StatusesRetweetsOfMe
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class StatusesHomeTimeline
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class StatusesShowId
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class StatusesUserTimeline
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class StatusesFriends
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class StatusesRetweetsId
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class StatusesMentionsTimeline
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class StatusesOembed
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class StatusesLookup
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class Statuses
    //{
    //    [JsonProperty("/statuses/retweeters/ids")]
    //    public StatusesRetweetersIds ReTweeterIds { get; set; }

    //    [JsonProperty("/statuses/retweets_of_me")]
    //    public StatusesRetweetsOfMe ReTweetsOfMe { get; set; }

    //    [JsonProperty("/statuses/home_timeline")]
    //    public StatusesHomeTimeline HomeTimeLine { get; set; }

    //    [JsonProperty("/statuses/show/:id")]
    //    public StatusesShowId ShowId { get; set; }

    //    [JsonProperty("/statuses/user_timeline")]
    //    public StatusesUserTimeline UserTimeline { get; set; }
    //    [JsonProperty("/statuses/friends")]
    //    public StatusesFriends Friends { get; set; }
    //    [JsonProperty("/statuses/retweets/:id")]
    //    public StatusesRetweetsId RetweetsId { get; set; }
    //    [JsonProperty("/statuses/mentions_timeline")]
    //    public StatusesMentionsTimeline MentionsTimeline { get; set; }
    //    [JsonProperty("/statuses/oembed")]
    //    public StatusesOembed OEmbed { get; set; }

    //    [JsonProperty("/statuses/lookup")]
    //    public StatusesLookup StatusLookUp { get; set; }
    //}

    //public class ContactsUploadedBy
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class ContactsUsers
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class ContactsAddressbook
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class ContactsUsersAndUploadedBy
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class ContactsDeleteStatus
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class Contacts
    //{
    //    [JsonProperty("/contacts/uploaded_by")]
    //    public ContactsUploadedBy ContactsUploadedBy { get; set; }
    //    [JsonProperty("/contacts/users")]
    //    public ContactsUsers ContactsUsers { get; set; }
    //    [JsonProperty("/contacts/addressbook")]
    //    public ContactsAddressbook ContactsAddressbook { get; set; }
    //    [JsonProperty("/contacts/users_and_uploaded_by")]
    //    public ContactsUsersAndUploadedBy ContactsUsersAndUploadedBy { get; set; }
    //    [JsonProperty("/contacts/delete/status")]
    //    public ContactsDeleteStatus ContactsDeleteStatus { get; set; }
    //}

    //public class MomentsPermissions
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class Moments
    //{
    //    [JsonProperty("/moments/permissions")]
    //    public MomentsPermissions MomentsPermissions { get; set; }
    //}

    //public class HelpTos
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class HelpConfiguration
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class HelpSettings
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class HelpPrivacy
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class HelpLanguages
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class Help
    //{
    //    [JsonProperty("/help/tos")]
    //    public HelpTos HelpTos { get; set; }
    //    [JsonProperty("/help/configuration")]
    //    public HelpConfiguration HelpConfiguration { get; set; }
    //    [JsonProperty("/help/settings")]
    //    public HelpSettings HelpSettings { get; set; }
    //    [JsonProperty("/help/privacy")]
    //    public HelpPrivacy HelpPrivacy { get; set; }
    //    [JsonProperty("/help/languages")]
    //    public HelpLanguages HelpLanguages { get; set; }
    //}

    //public class FeedbackShowId
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class FeedbackEvents
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class Feedback
    //{
    //    [JsonProperty("/feedback/show/:id")]
    //    public FeedbackShowId FeedbackShowId { get; set; }
    //    [JsonProperty("/feedback/events")]
    //    public FeedbackEvents FeedbackEvents { get; set; }
    //}

    //public class BusinessExperienceKeywords
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class BusinessExperience
    //{
    //    [JsonProperty("/business_experience/keywords")]
    //    public BusinessExperienceKeywords BusinessExperienceKeywords { get; set; }
    //}

    //public class FriendsFollowingIds
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class FriendsFollowingList
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class FriendsList
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class FriendsIds
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class Friends
    //{
    //    [JsonProperty("/friends/following/ids")]
    //    public FriendsFollowingIds FriendsFollowingIds { get; set; }
    //    [JsonProperty("/friends/following/list")]
    //    public FriendsFollowingList FriendsFollowingList { get; set; }
    //    [JsonProperty("/friends/list")]
    //    public FriendsList FriendsList { get; set; }
    //    [JsonProperty("/friends/ids")]
    //    public FriendsIds FriendsIds { get; set; }
    //}

    //public class DirectMessagesSent
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class DirectMessages2
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class DirectMessagesSentAndReceived
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class DirectMessagesShow
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class DirectMessages
    //{
    //    [JsonProperty("/direct_messages/sent")]
    //    public DirectMessagesSent DirectMessagesSent { get; set; }
    //    [JsonProperty("/direct_messages")]
    //    public DirectMessages2 DirectMessages2 { get; set; }
    //    [JsonProperty("/direct_messages/sent_and_received")]
    //    public DirectMessagesSentAndReceived DirectMessagesSentAndReceived { get; set; }
    //    [JsonProperty("/direct_messages/show")]
    //    public DirectMessagesShow DirectMessagesShow { get; set; }
    //}

    //public class MediaUpload
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class Media
    //{
    //    [JsonProperty("/media/upload")]
    //    public MediaUpload MediaUpload { get; set; }
    //}

    //public class AccountLoginVerificationEnrollment
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class AccountUpdateProfile
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class AccountVerifyCredentials
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class AccountSettings
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class Account
    //{
    //    [JsonProperty("/account/login_verification_enrollment")]
    //    public AccountLoginVerificationEnrollment AccountLoginVerificationEnrollment { get; set; }
    //    [JsonProperty("/account/update_profile")]
    //    public AccountUpdateProfile AccountUpdateProfile { get; set; }
    //    [JsonProperty("/account/verify_credentials")]
    //    public AccountVerifyCredentials AccountVerifyCredentials { get; set; }
    //    [JsonProperty("/account/settings")]
    //    public AccountSettings AccountSettings { get; set; }
    //}

    //public class FavoritesList
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class Favorites
    //{
    //    [JsonProperty("/favorites/list")]
    //    public FavoritesList FavoritesList { get; set; }
    //}

    //public class DeviceToken
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class Device
    //{
    //    [JsonProperty("/device/token")]
    //    public DeviceToken DeviceToken { get; set; }
    //}

    //public class SavedSearchesDestroyId
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class SavedSearchesShowId
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class SavedSearchesList
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class SavedSearches
    //{
    //    [JsonProperty("/saved_searches/destroy/:id")]
    //    public SavedSearchesDestroyId SavedSearchesDestroyId { get; set; }
    //    [JsonProperty("/saved_searches/show/:id")]
    //    public SavedSearchesShowId SavedSearchesShowId { get; set; }
    //    [JsonProperty("/saved_searches/list")]
    //    public SavedSearchesList SavedSearchesList { get; set; }
    //}

    //public class SearchTweets
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class Search
    //{
    //    [JsonProperty("/search/tweets")]
    //    public SearchTweets SearchTweets { get; set; }
    //}

    //public class TrendsClosest
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class TrendsAvailable
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class TrendsPlace
    //{
    //    public int limit { get; set; }
    //    public int remaining { get; set; }
    //    public long reset { get; set; }
    //}

    //public class Trends
    //{
    //    [JsonProperty("/trends/closest")]
    //    public TrendsClosest TrendsClosest { get; set; }
    //    [JsonProperty("/trends/available")]
    //    public TrendsAvailable TrendsAvailable { get; set; }
    //    [JsonProperty("/trends/place")]
    //    public TrendsPlace TrendsPlace { get; set; }
    //}

    //public class Resources
    //{
    //    public Lists lists { get; set; }
    //    public TweetApplication application { get; set; }
    //    public Mutes mutes { get; set; }
    //    public Friendships friendships { get; set; }
    //    public Auth auth { get; set; }
    //    public Blocks blocks { get; set; }
    //    public Geo geo { get; set; }
    //    public Users users { get; set; }
    //    public Followers followers { get; set; }
    //    public Collections collections { get; set; }
    //    public Statuses statuses { get; set; }
    //    public Contacts contacts { get; set; }
    //    public Moments moments { get; set; }
    //    public Help help { get; set; }
    //    public Feedback feedback { get; set; }
    //    public BusinessExperience business_experience { get; set; }
    //    public Friends friends { get; set; }
    //    public DirectMessages direct_messages { get; set; }
    //    public Media media { get; set; }
    //    public Account account { get; set; }
    //    public Favorites favorites { get; set; }
    //    public Device device { get; set; }
    //    public SavedSearches saved_searches { get; set; }
    //    public Search search { get; set; }
    //    public Trends trends { get; set; }
    //}

    //public class TweetApiLimits
    //{
    //    public RateLimitContext rate_limit_context { get; set; }
    //    public Resources resources { get; set; }
    //}


}
