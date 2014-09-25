using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitterHelper.Entity;
using TwitterHelper.OAuth;

namespace TwitterHelper
{
    public static class TwitterEndpointHelper
    {

        private static string _twitterBaseAddress = "https://api.twitter.com/1.1/";
        //private static string _FollowerIDs = "followers/ids.json?cursor=-1&screen_name=aramkoukia&count=5000";
        //private static string _FollowingIDs = "friends/ids.json?cursor=-1&screen_name=aramkoukia&count=5000";
        //private static string _UserLookupByName = "users/lookup.json?screen_name="; // A comma separated list of user IDs, up to 100 are allowed in a single request. You are strongly encouraged to use a POST for larger requests.

        public static string GetFollowerUsersURI(Int64 cursor, bool skipStatus, bool includeUserEntities, string screenName)
        {
            return _twitterBaseAddress + 
                   "followers/list.json?" + 
                   string.Format("cursor={0}&skip_status={1}&include_user_entities={2}&screen_name={3};",cursor,skipStatus,includeUserEntities,screenName);
        }
        
        public static string GetStatusesURI(bool includeRts, string screenName, Int64 Count)
        {
            return _twitterBaseAddress + 
                   "statuses/user_timeline.json?" +
                   string.Format("include_rts={0}&screen_name={1}&count={2}",includeRts,screenName,Count);
        }

        public static string GetFollowingUsersURI(Int64 cursor, bool skipStatus, bool includeUserEntities, string screenName)
        {
            return _twitterBaseAddress +
                   "friends/list.json?" +
                   string.Format("cursor={0}&skip_status={1}&include_user_entities={2}&screen_name={3};", cursor, skipStatus, includeUserEntities, screenName);
        }

        public static string GetUserLookupURI(string UserIDs)
        {
            return _twitterBaseAddress +
                   "users/lookup.json?" +
                   string.Format("user_id={0}", UserIDs);
        }

        public static string GetFriendshipLookupURI(string UserIDs)
        {
            return _twitterBaseAddress +
                   "friendships/lookup.json?" +
                   string.Format("user_id={0}", UserIDs);
        }

        public static string GetDestroyFriendshipURI()
        {
            return _twitterBaseAddress +
                   "friendships/destroy.json";
        }

        public static string GetFriendsIDsURI(Int64 cursor, string screenName, int count)
        {
            return _twitterBaseAddress +
                   "friends/ids.json?" +
                   string.Format("?cursor={0}&screen_name={1}&count={2}", cursor, screenName, count);
        }

        public static string GetTweetURI()
        {
            return _twitterBaseAddress +
                   "statuses/update.json";
        }

        
    }
}
