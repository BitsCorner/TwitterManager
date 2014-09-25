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
    public class TwitterRepository
    {

        public async Task<List<TwitterStatus>> GetLatesStatuses(string screenName, Int64 count = 20)
        {
            HttpClient client = new HttpClient(new OAuthMessageHandler(new HttpClientHandler()));
            HttpResponseMessage response = await client.GetAsync(TwitterEndpointHelper.GetStatusesURI(true,screenName,count));
            JToken statuses = await response.Content.ReadAsAsync<JToken>();
            if (response.IsSuccessStatusCode)
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<TwitterStatus>>(statuses.ToString());
            else
                return null;
        }

        public async Task<TwitterUserCursor> GetFollowers(string screenName,Int64 cursor = -1)
        {
            HttpClient client = new HttpClient(new OAuthMessageHandler(new HttpClientHandler()));
            HttpResponseMessage response = await client.GetAsync(TwitterEndpointHelper.GetFollowerUsersURI(cursor, false,true,screenName));
            JToken statuses = await response.Content.ReadAsAsync<JToken>();
            if (response.IsSuccessStatusCode)
            {
                var twitterUserCursor = Newtonsoft.Json.JsonConvert.DeserializeObject<TwitterUserCursor>(statuses.ToString());
                return twitterUserCursor;
            }
            else
                return null;
        }

        public async Task<TwitterUserCursor> GetFollowings(string screenName, Int64 cursor = -1)
        {
            HttpClient client = new HttpClient(new OAuthMessageHandler(new HttpClientHandler()));
            HttpResponseMessage response = await client.GetAsync(TwitterEndpointHelper.GetFollowingUsersURI(cursor, false, true, screenName));
            JToken statuses = await response.Content.ReadAsAsync<JToken>();

            var x_rate_limit_limit = response.Headers.Where(m => m.Key == "x-rate-limit-limit").FirstOrDefault().Value;
            var x_rate_limit_remaining = response.Headers.Where(m => m.Key == "x-rate-limit-remaining").FirstOrDefault().Value;
            var x_rate_limit_reset= response.Headers.Where(m => m.Key == "x-rate-limit-reset").FirstOrDefault().Value;
            if (response.IsSuccessStatusCode)
            {
                var twitterUserCursor = Newtonsoft.Json.JsonConvert.DeserializeObject<TwitterUserCursor>(statuses.ToString());

                var UserIDs = twitterUserCursor.users.Select(m => m.id_str).ToList();
                var Friendships = await GetTwitterFriendships(string.Join("%2C", UserIDs));
                if (Friendships != null)
                {
                    foreach (var item in twitterUserCursor.users)
                    {
                        item.connections = Friendships.FirstOrDefault(m => m.id_str == item.id_str).connections;
                    }
                }
                return twitterUserCursor;
            }
            else
                return null;
        }

        private async Task<List<TwitterUser>> GetTwitterUserDetails(string screenName, string UserIDs)
        {
            HttpClient client = new HttpClient(new OAuthMessageHandler(new HttpClientHandler()));
            HttpResponseMessage response = await client.GetAsync(TwitterEndpointHelper.GetUserLookupURI(UserIDs));
            JToken statuses = await response.Content.ReadAsAsync<JToken>();
            if (response.IsSuccessStatusCode)
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<TwitterUser>>(statuses.ToString());
            else
                return null;
        }

        private async Task<List<FriendshipLookup>> GetTwitterFriendships(string UserIDs)
        {
            HttpClient client = new HttpClient(new OAuthMessageHandler(new HttpClientHandler()));
            HttpResponseMessage response = await client.GetAsync(TwitterEndpointHelper.GetFriendshipLookupURI(UserIDs));
            JToken statuses = await response.Content.ReadAsAsync<JToken>();
            if (response.IsSuccessStatusCode)
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<FriendshipLookup>>(statuses.ToString());
            else
                return null;
        }

        public async Task<TwitterUserCursor> Unfollow(string screen_name, string userid)
        {
            HttpClient client = new HttpClient(new OAuthMessageHandler(new HttpClientHandler()));
            //var argsAsJson = "screen_name=" + screen_name;
            //StringContent content = new StringContent(argsAsJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.DeleteAsync(TwitterEndpointHelper.GetDestroyFriendshipURI() + "?user_id="+userid );
            JToken statuses = await response.Content.ReadAsAsync<JToken>();
            if (response.IsSuccessStatusCode)
            {
                return null;
            }
            else
                return null;
        }

        public async Task<TwitterUserCursor> Tweet(string Status)
        {
            HttpClient client = new HttpClient(new OAuthMessageHandler(new HttpClientHandler()));
            var argsAsJson = "status=" + Status;
            StringContent content = new StringContent(argsAsJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsJsonAsync(TwitterEndpointHelper.GetTweetURI(), content);
            JToken statuses = await response.Content.ReadAsAsync<JToken>();
            if (response.IsSuccessStatusCode)
            {
                return null;
            }
            else
                return null;
        }

    }
}
