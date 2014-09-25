using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterHelper.Entity
{
    public class TwitterUser
    {
        public string name { get; set; }
        public string screen_name { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public string created_at { get; set; }
        public bool follow_request_sent { get; set; }
        public bool Protected { get; set; }
        public Int64 followers_count { get; set; }
        public Int64? utc_offset { get; set; }
        public Int64 favourites_count { get; set; }
        public string url { get; set; }
        public string profile_image_url_https { get; set; }
        public string id_str { get; set; }
        public string lang { get; set; }
        public string statuses_count { get; set; }
        public bool following { get; set; }
        public Int64 friends_count { get; set; }
        public string time_zone { get; set; }
        public TwitterStatus status { get; set; }
        public List<string> connections { get; set; }
        public bool IsFollowingBack
        {
            get {
                if (connections != null)
                    return (connections.Contains("followed_by") ? true : false);
                else
                    return false;
                }
        }
    }
}
 
